using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryBLL
    {
        CategoryModel GetDataById(int id);
        List<CategoryModel> GetAll();
        bool Create(CategoryModel categoryModel);
        bool Update(CategoryModel categoryModel);
        bool Delete(int id);
        List<CategoryModel> Search(string name);
        List<CategoryModel> Pagination(int pageNumber, int pageSize);
        List<CategoryModel> GetDataDeletedPagination(int pageNumber, int pageSize);
        List<CategoryModel> SearchAndPagination(int pageNumber, int pageSize, string name);
    }
}
