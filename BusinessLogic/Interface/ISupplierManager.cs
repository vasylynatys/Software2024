using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interface
{
    public interface ISupplierManager
    {
        List<Supplier> GetAllSuppliers();
        void AddSupplier(Supplier supplier);
        Supplier GetSupplierById(int id);
        List<Supplier> SearchSupplier(string query);
        List<Supplier> GetBlockedSuppliers();
        List<Supplier> GetSortedSupplier(string sortBy, bool ascending);
        void DeleteSupplier(int supplierId);
        void UpdateSupplier(Supplier supplier);


    }
}
