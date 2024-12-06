using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductDAL : IProductDAL
    {
        private IDatabaseHelper _IDatabaseHelper;
        public ProductDAL(IDatabaseHelper IDatabaseHelper)
        {
            _IDatabaseHelper = IDatabaseHelper;
        }
        public bool Create(ProductModel productModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_product_create",
                    "@product_Name", productModel.ProductName, "@product_Quantity", productModel.Quantity,
                    "@product_Price", productModel.Price, "@product_Description", productModel.Description,
                    "@product_Brand", productModel.Brand, "@product_Image", productModel.ProductImage,
                    "@product_Star", productModel.Star, "@product_ProductDetail", productModel.ProductDetail,
                    "@product_CategoryId", productModel.CategoryId);
                if (result != null && !string.IsNullOrEmpty(result))
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

        public bool Delete(int id)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_product_delete",
                    "@product_Id", id);
                if (result != null && !string.IsNullOrEmpty(result))
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

        public List<ProductModel> GetAll()
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_product_all");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<ProductModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductModel GetDataById(int id)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_product_get_data_by_id",
                    "@product_Id", id);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<ProductModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductModel> Search(string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_product_search",
                    "@product_Name", name);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<ProductModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(ProductModel productModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_product_update",
                    "@product_Id", productModel.ProductId,
                    "@product_Name", productModel.ProductName, 
                    "@product_Quantity", productModel.Quantity,
                    "@product_Price", productModel.Price, 
                    "@product_Description", productModel.Description,
                    "@product_Brand", productModel.Brand, 
                    "@product_Image", productModel.ProductImage,
                    "@product_Star", productModel.Star, 
                    "@product_ProductDetail", productModel.ProductDetail,
                    "@product_CategoryId", productModel.CategoryId,
                    "@product_Deleted", productModel.Deleted);
                if (result != null && !string.IsNullOrEmpty(result))
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

        public List<ProductModel> Pagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_product_pagination",
                    "@product_pageNumber", pageNumber,
                    "@product_pageSize", pageSize);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<ProductModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProductModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_product_deleted_pagination",
                    "@product_pageNumber", pageNumber,
                    "@product_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<ProductModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_product_search_pagination",
                    "@product_Name", name,
                    "@product_pageNumber", pageNumber,
                    "@product_pageSize", pageSize);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<ProductModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
