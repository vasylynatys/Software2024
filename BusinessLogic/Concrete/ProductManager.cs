using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interface;
using Dal.Interface;
using DTO;

namespace BusinessLogic.Concrete
{
    public class ProductManager : IProductManager
    {
        private readonly IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public List<Product> GetAllProducts()
        {
            return _productDal.GetAll();
        }
        public Product GetProductById(int id)
        {
            return _productDal.GetById(id);
        }
        public void AddProduct(Product product)
        {
            _productDal.Add(product);
        }
        public void DeleteProduct(int productId) 
        {
            _productDal.Delete(productId);
        }
        public List<Product> GetProductsBySupplierId(int supplierId)
        {
            return _productDal.GetBySupplierId(supplierId);
        }
        public List<Product> GetAllSorted(string sortBy, bool ascending)
        {
            return _productDal.GetAllSorted(sortBy, ascending);
        }
    }
}
