using BusinessLogic.Interface;
using DTO;
using System.Windows;

namespace SupplierProductManagementApp
{
    public partial class ProductsWindow : Window
    {
        private readonly IProductManager _productManager;

        public ProductsWindow(IProductManager productManager)
        {
            InitializeComponent();
            _productManager = productManager;
            LoadProducts();
        }

        // Завантажити продукти в DataGrid
        private void LoadProducts()
        {
            ProductsDataGrid.ItemsSource = _productManager.GetAllProducts();
        }

        // Обробка кнопки для додавання нового продукту
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProductWindow(_productManager);
            addProductWindow.ShowDialog();
            LoadProducts();
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is Product selectedProduct)
            {
                _productManager.DeleteProduct(selectedProduct.ProductId);
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Please select a product to delete.");
            }
        }
    }
}
