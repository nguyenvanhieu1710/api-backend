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
    public class ImportBillDAL : IImportBillDAL
    {
        private IDatabaseHelper _IDatabaseHelper;
        public ImportBillDAL(IDatabaseHelper databaseHelper)
        {
            _IDatabaseHelper = databaseHelper;
        }
        public bool Create(ImportBillModel importBillModel)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_importBill_create",
                    "@importBill_SupplierId", importBillModel.SupplierId,
                    "@importBill_StaffId", importBillModel.StaffId,
                    "@importBill_InputDay", importBillModel.InputDay,
                    "@listjson_importBillDetail", importBillModel.listjson_importBillDetail != null ? MessageConvert.SerializeObject(importBillModel.listjson_importBillDetail) : null);
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
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_importBill_delete",
                    "@importBill_Id", id);
                if (!string.IsNullOrEmpty(result))
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

        public List<ImportBillModel> GetAll()
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_importBill_all");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<ImportBillModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ImportBillModel GetDataById(int id)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_importBill_get_data_by_id",
                    "@importBill_Id", id);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<ImportBillModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ImportBillModel> Pagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_importBill_pagination",
                    "@importBill_pageNumber", pageNumber,
                    "@importBill_pageSize", pageSize);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<ImportBillModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ImportBillModel> Search(string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_importBill_search",
                    "@supplierName", name);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<ImportBillModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ImportBillModel> SearchAndPagination(string name, int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_importBill_search_pagination",
                    "@product_Name", name,
                    "@importBill_pageNumber", pageNumber,
                    "@importBill_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<ImportBillModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(ImportBillModel importBillModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_importBill_update",
                    "@importBill_Id", importBillModel.ImportBillId,
                    "@importBill_SupplierId", importBillModel.SupplierId,
                    "@importBill_StaffId", importBillModel.StaffId,
                    "@importBill_InputDay", importBillModel.InputDay,
                    "@listjson_importBillDetail", importBillModel.listjson_importBillDetail != null ? MessageConvert.SerializeObject(importBillModel.listjson_importBillDetail) : null);
                if (!string.IsNullOrEmpty(result))
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
    }
}
