using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public class CategoryDAL : ICategoryDAL
    {
        private IDatabaseHelper _IDatabaseHelper;
        public CategoryDAL(IDatabaseHelper databaseHelper)
        {
            _IDatabaseHelper = databaseHelper;
        }

        public List<CategoryModel> GetAll()
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_category_all");
                if (!string.IsNullOrEmpty(msgError)) {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<CategoryModel>().ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        } 
        public CategoryModel GetDataById(int id)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_category_get_data_by_id",
                    "@category_Id", id);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<CategoryModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Create(CategoryModel categoryModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_category_create",
                    "@category_Name", categoryModel.CategoryName,
                    "@category_Image", categoryModel.CategoryImage,
                    "@category_DadCategoryId", categoryModel.DadCategoryId);
                if (result != null && !string.IsNullOrEmpty(result.ToString())) { 
                    throw new Exception(result.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_category_delete",
                    "@category_Id",id);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(result.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(CategoryModel categoryModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_category_update",
                    "@category_Id", categoryModel.CategoryId,
                    "@category_Name", categoryModel.CategoryName,
                    "@category_Image", categoryModel.CategoryImage,
                    "@category_DadCategoryId", categoryModel.DadCategoryId,
                    "@category_Deleted", categoryModel.Deleted);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(result.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryModel> Search(string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_category_search",
                    "@category_Name", name);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<CategoryModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryModel> Pagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_category_pagination",
                    "@category_pageNumber", pageNumber,
                    "@category_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<CategoryModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_category_deleted_pagination",
                    "@category_pageNumber", pageNumber,
                    "@category_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<CategoryModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_category_search_pagination",
                    "@category_pageNumber", pageNumber,
                    "@category_pageSize", pageSize,
                    "@category_Name", name);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<CategoryModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
