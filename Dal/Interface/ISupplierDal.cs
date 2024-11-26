using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interface
{
    public interface ISupplierDal
    {
        List<Supplier> GetAll();
        Supplier GetById(int supplierId);
        void Add(Supplier supplier);
        void Update(Supplier supplier);
        void Delete(int supplierId);
        List<Supplier> Search(string query);
        List<Supplier> GetBlockedSuppliers();

        List<Supplier> GetAllSorted(string sortBy, bool ascending);
    }
}
