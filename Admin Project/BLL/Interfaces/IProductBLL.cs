using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductBLL
    {
        ProductModel GetDataById(int id);
        List<ProductModel> GetAll();
        List<ProductModel> GetBestSellingProduct();
        bool Create(ProductModel productModel);
        bool Update(ProductModel productModel);
        bool Delete(int id);
        List<ProductModel> Search(string name);
        List<ProductModel> Pagination(int pageNumber, int pageSize);
        List<ProductModel> GetDataDeletedPagination(int pageNumber, int pageSize);
        List<ProductModel> SearchAndPagination(int pageNumber, int pageSize, string name);
    }
}
