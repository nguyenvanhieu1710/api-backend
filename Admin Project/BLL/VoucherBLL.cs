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
    public class VoucherBLL : IVoucherBLL
    {
        private IVoucherDAL _IVoucherDAL;
        public VoucherBLL(IVoucherDAL InterfaceVoucherDAL)
        {
            _IVoucherDAL = InterfaceVoucherDAL;
        }

        public VoucherModel GetDataById(int id)
        {
            return _IVoucherDAL.GetDataById(id);
        }

        public List<VoucherModel> GetAll()
        {
            return _IVoucherDAL.GetAll();
        }

        public bool Create(VoucherModel voucherModel)
        {
            return _IVoucherDAL.Create(voucherModel);
        }

        public bool Update(VoucherModel voucherModel)
        {
            return _IVoucherDAL.Update(voucherModel);
        }
        public bool Delete(int id)
        {
            return _IVoucherDAL.Delete(id);
        }

        public List<VoucherModel> Search(string name)
        {
            return _IVoucherDAL.Search(name);
        }
        public List<VoucherModel> Pagination(int pageNumber, int pageSize)
        {
            return _IVoucherDAL.Pagination(pageNumber, pageSize);
        }

        public List<VoucherModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            return _IVoucherDAL.GetDataDeletedPagination(pageNumber, pageSize);
        }
        public List<VoucherModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _IVoucherDAL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
