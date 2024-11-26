using BusinessLogic.Interface;
using Dal.Interface;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Concrete
{
    public class SupplierManager : ISupplierManager
    {
        private readonly ISupplierDal _supplierDal;

        public SupplierManager(ISupplierDal supplierDal)
        {
            _supplierDal = supplierDal;
        }
        public List<Supplier> GetAllSuppliers()
        {
            return _supplierDal.GetAll();
        }
        public void AddSupplier(Supplier supplier)
        {
            _supplierDal.Add(supplier);
        }
        public Supplier GetSupplierById(int id)
        {
            return _supplierDal.GetById(id);
        }
        public List<Supplier> SearchSupplier(string query)
        {
            return _supplierDal.Search(query);
        }
        public List<Supplier> GetBlockedSuppliers()
        {
            return _supplierDal.GetBlockedSuppliers();
        }
        public List<Supplier> GetSortedSupplier(string sortBy, bool ascending)
        {
            return _supplierDal.GetAllSorted(sortBy, ascending);
        }
        public void DeleteSupplier(int supplierid)
        {
            _supplierDal.Delete(supplierid);
        }
        public void UpdateSupplier(Supplier supplier)
        {
            _supplierDal.Update(supplier);
        }
    }
}
