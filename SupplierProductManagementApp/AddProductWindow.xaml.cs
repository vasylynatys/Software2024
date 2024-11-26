using BusinessLogic.Interface;
using DTO;
using System.Windows;

namespace SupplierProductManagementApp
{
    public partial class AddProductWindow : Window
    {
        private readonly IProductManager _productManager;

        public AddProductWindow(IProductManager productManager)
        {
            InitializeComponent();
            _productManager = productManager;
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            string productName = ProductNameTextBox.Text;
            string productPriceText = ProductPriceTextBox.Text;
            string supplierId = SupplierIdTextBox.Text;

            if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(productPriceText))
            {
                ErrorMessage.Text = "Product name and price cannot be empty!";
                return;
            }

            if (!decimal.TryParse(productPriceText, out decimal productPrice))
            {
                ErrorMessage.Text = "Invalid price format!";
                return;
            }

            var newProduct = new Product
            {
                Name = productName,
                Price = productPrice,
                SupplierId = supplierId
            };

            _productManager.AddProduct(newProduct);
            MessageBox.Show("Product added successfully!");
            this.Close();
        }
    }
}
