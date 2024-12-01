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
    public class CategoryBLL : ICategoryBLL
    {
        private ICategoryDAL _ICategoryDAL;
        public CategoryBLL(ICategoryDAL icategoryDAL)
        {
            _ICategoryDAL = icategoryDAL;
        }

        public CategoryModel GetDataById(int id)
        {
            return _ICategoryDAL.GetDataById(id);
        }
        public List<CategoryModel> GetAll()
        {
            return _ICategoryDAL.GetAll();
        }

        public bool Create(CategoryModel categoryModel)
        {
            return _ICategoryDAL.Create(categoryModel);
        }

        public bool Update(CategoryModel categoryModel)
        {
            return _ICategoryDAL.Update(categoryModel);
        }

        public bool Delete(int id)
        {
            return _ICategoryDAL.Delete(id);
        }
        public List<CategoryModel> Search(string name)
        {
            //List<CategoryModel> categoryList = GetAll();
            //List<CategoryModel> categoryListAfterSearch = categoryList.Where(
            //    categoryModel =>  categoryModel.CategoryImage.Contains(name)).ToList();
            //return categoryListAfterSearch;
            return _ICategoryDAL.Search(name);
        }

        public List<CategoryModel> Pagination(int pageNumber, int pageSize)
        {
            return _ICategoryDAL.Pagination(pageNumber, pageSize);
        }

        public List<CategoryModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            return _ICategoryDAL.GetDataDeletedPagination(pageNumber, pageSize);
        }

        public List<CategoryModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _ICategoryDAL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
