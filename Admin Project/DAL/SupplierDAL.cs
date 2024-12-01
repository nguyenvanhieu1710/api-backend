using DAL.Helper;
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
    public class SupplierDAL : ISupplierDAL
    {
        private IDatabaseHelper _IDatabaseHelper;
        public SupplierDAL(IDatabaseHelper dbhelper)
        {
            _IDatabaseHelper = dbhelper;
        }

        public List<SupplierModel> GetAll()
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_supplier_all");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<SupplierModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SupplierModel GetDataById(int id)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_supplier_get_data_by_id",
                    "@supplier_Id", id);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<SupplierModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create(SupplierModel supplierModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_supplier_create",
                    "@supplier_Name", supplierModel.SupplierName,
                    "@supplier_PhoneNumber", supplierModel.PhoneNumber,
                    "@supplier_Address", supplierModel.Address);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(Convert.ToString(result));
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
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_supplier_delete",
                    "@supplier_Id", id);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(Convert.ToString(result));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(SupplierModel supplierModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_supplier_update",
                    "@supplier_Id", supplierModel.SupplierId,
                    "@supplier_Name", supplierModel.SupplierName,
                    "@supplier_PhoneNumber", supplierModel.PhoneNumber,
                    "@supplier_Address", supplierModel.Address,
                    "@supplier_Deleted", supplierModel.Deleted ? 1 : 0);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(Convert.ToString(result));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SupplierModel> Search(string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_supplier_search",
                    "@supplier_Name", name);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<SupplierModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SupplierModel> Pagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_supplier_pagination",
                    "@supplier_pageNumber", pageNumber,
                    "@supplier_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<SupplierModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SupplierModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_supplier_deleted_pagination",
                    "@supplier_pageNumber", pageNumber,
                    "@supplier_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<SupplierModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SupplierModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_supplier_search_pagination",
                    "@supplier_Name", name,
                    "@supplier_pageNumber", pageNumber,
                    "@supplier_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<SupplierModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
