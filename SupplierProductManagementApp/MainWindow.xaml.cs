using SupplierProductManagementApp;
using System.Windows;

namespace SupplierProductManagementApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ManageSuppliersButton_Click(object sender, RoutedEventArgs e)
        {
            var suppliersWindow = new SuppliersWindow();
            suppliersWindow.Show();
            this.Close();
        }
        
        private void ManageProductsButton_Click(object sender, RoutedEventArgs e)
        {
            var productsWindow = new ProductsWindow();
            productsWindow.Show();
            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
