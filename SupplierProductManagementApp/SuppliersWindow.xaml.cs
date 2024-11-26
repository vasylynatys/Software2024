using BusinessLogic.Interface;
using DTO;
using System.Windows;

namespace SupplierProductManagementApp
{
    public partial class SuppliersWindow : Window
    {
        private readonly ISupplierManager _supplierManager;

        public SuppliersWindow(ISupplierManager supplierManager)
        {
            InitializeComponent();
            _supplierManager = supplierManager;
            LoadSuppliers();
        }

        private void LoadSuppliers()
        {
            SuppliersDataGrid.ItemsSource = _supplierManager.GetAllSuppliers();
        }

        private void AddSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            var addSupplierWindow = new AddSupplierWindow(_supplierManager);
            addSupplierWindow.ShowDialog();
            LoadSuppliers();
        }

        private void BlockSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            if (SuppliersDataGrid.SelectedItem is Supplier selectedSupplier)
            {
                selectedSupplier.IsBlocked = true;
                _supplierManager.UpdateSupplier(selectedSupplier);
                LoadSuppliers();
            }
            else
            {
                MessageBox.Show("Please select a supplier to block.");
            }
        }

        private void DeleteSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            if (SuppliersDataGrid.SelectedItem is Supplier selectedSupplier)
            {
                _supplierManager.DeleteSupplier(selectedSupplier.SupplierId);
                LoadSuppliers();
            }
            else
            {
                MessageBox.Show("Please select a supplier to delete.");
            }
        }
    }
}
