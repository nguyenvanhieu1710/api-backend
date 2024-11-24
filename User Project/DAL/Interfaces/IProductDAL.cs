using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProductDAL
    {
        ProductModel GetDataById(int id);
        List<ProductModel> GetAll();
        List<ProductModel> GetBestSellingProduct();
        List<ProductModel> Search(string name);
        List<ProductModel> Pagination(int pageNumber, int pageSize);
        List<ProductModel> SearchAndPagination(string name, int pageNumber, int pageSize);
    }
}
