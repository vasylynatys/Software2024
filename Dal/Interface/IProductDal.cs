using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interface
{
    public interface IProductDal
    {
        List<Product> GetAll();
        Product GetById(int productId);
        void Add(Product product);
        void Update(Product product);
        void Delete(int productId);
        List<Product> GetBySupplierId(int supplierId);
        List<Product> GetAllSorted(string sortBy, bool ascending);
    }
}
