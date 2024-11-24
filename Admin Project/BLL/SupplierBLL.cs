using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SupplierBLL : ISupplierBLL
    {
        private ISupplierDAL _ISupplierDAL;
        public SupplierBLL(ISupplierDAL InterfaceSupplierDAL)
        {
            _ISupplierDAL = InterfaceSupplierDAL;
        }

        public SupplierModel GetDataById(int id)
        {
            return _ISupplierDAL.GetDataById(id);
        }

        public List<SupplierModel> GetAll()
        {
            return _ISupplierDAL.GetAll();
        }

        public bool Create(SupplierModel supplierModel)
        {
            return _ISupplierDAL.Create(supplierModel);
        }

        public bool Update(SupplierModel supplierModel)
        {
            return _ISupplierDAL.Update(supplierModel);
        }
        public bool Delete(int id)
        {
            return _ISupplierDAL.Delete(id);
        }

        public List<SupplierModel> Search(string name)
        {
            return _ISupplierDAL.Search(name);
        }
        public List<SupplierModel> Pagination(int pageNumber, int pageSize)
        {
            return _ISupplierDAL.Pagination(pageNumber, pageSize);
        }

        public List<SupplierModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _ISupplierDAL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
