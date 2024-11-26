using BusinessLogic.Interface;
using DTO;
using System.Windows;

namespace SupplierProductManagementApp
{
    public partial class AddSupplierWindow : Window
    {
        private readonly ISupplierManager _supplierManager;

        public AddSupplierWindow(ISupplierManager supplierManager)
        {
            InitializeComponent();
            _supplierManager = supplierManager;
        }

        private void AddSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            string supplierName = SupplierNameTextBox.Text;
            bool isBlocked = BlockedCheckBox.IsChecked.GetValueOrDefault(false);

            if (string.IsNullOrEmpty(supplierName))
            {
                ErrorMessage.Text = "Supplier name cannot be empty!";
                return;
            }

            var newSupplier = new Supplier
            {
                Name = supplierName,
                IsBlocked = isBlocked
            };

            _supplierManager.AddSupplier(newSupplier);
            MessageBox.Show("Supplier added successfully!");
            this.Close();
        }
    }
}
