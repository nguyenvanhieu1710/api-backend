using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductBLL : IProductBLL
    {
        private IProductDAL _IProductDAL;
        public ProductBLL(IProductDAL InterfaceProductDAL)
        {
            _IProductDAL = InterfaceProductDAL;
        }

        public ProductModel GetDataById(int id)
        {
            return _IProductDAL.GetDataById(id);
        }

        public List<ProductModel> GetAll()
        {
            return _IProductDAL.GetAll();
        }

        public bool Create(ProductModel voucherModel)
        {
            return _IProductDAL.Create(voucherModel);
        }

        public bool Update(ProductModel voucherModel)
        {
            return _IProductDAL.Update(voucherModel);
        }
        public bool Delete(int id)
        {
            return _IProductDAL.Delete(id);
        }

        public List<ProductModel> Search(string name)
        {
            return _IProductDAL.Search(name);
        }
        public List<ProductModel> Pagination(int pageNumber, int pageSize)
        {
            return _IProductDAL.Pagination(pageNumber, pageSize);
        }
        public List<ProductModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            return _IProductDAL.GetDataDeletedPagination(pageNumber, pageSize);
        }
        public List<ProductModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _IProductDAL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
