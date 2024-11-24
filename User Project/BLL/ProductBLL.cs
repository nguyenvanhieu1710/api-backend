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

        public List<ProductModel> GetBestSellingProduct()
        {
            return _IProductDAL.GetBestSellingProduct();
        }

        public List<ProductModel> Search(string name)
        {
            return _IProductDAL.Search(name);
        }
        public List<ProductModel> Pagination(int pageNumber, int pageSize)
        {
            return _IProductDAL.Pagination(pageNumber, pageSize);
        }

        public List<ProductModel> SearchAndPagination(string name, int pageNumber, int pageSize)
        {
            return _IProductDAL.SearchAndPagination(name, pageNumber, pageSize);
        }
    }
}
