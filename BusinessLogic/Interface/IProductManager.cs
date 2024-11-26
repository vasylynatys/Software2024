using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BusinessLogic.Interface
{
    public interface IProductManager
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void DeleteProduct(int productId);
        List<Product> GetProductsBySupplierId(int supplierId);
        List<Product> GetAllSorted(string sortBy, bool ascending);

    }
}
