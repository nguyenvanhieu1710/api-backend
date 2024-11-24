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
    public class VoucherDAL : IVoucherDAL
    {
        private IDatabaseHelper _IDatabaseHelper;
        public VoucherDAL(IDatabaseHelper dbhelper)
        {
            _IDatabaseHelper = dbhelper;
        }

        public List<VoucherModel> GetAll()
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_voucher_all");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<VoucherModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public VoucherModel GetDataById(int id)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_voucher_get_data_by_id",
                    "@voucher_Id", id);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<VoucherModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create(VoucherModel voucherModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_voucher_create",
                    "@voucher_Name", voucherModel.VoucherName,
                    "@voucher_Price", voucherModel.Price,
                    "@voucher_MinimumPrice", voucherModel.MinimumPrice,
                    "@voucher_Quantity", voucherModel.Quantity,
                    "@voucher_StartDay", voucherModel.StartDay,
                    "@voucher_EndDate", voucherModel.EndDate);
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
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_voucher_delete",
                    "@voucher_Id", id);
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

        public bool Update(VoucherModel voucherModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_voucher_update",
                    "@voucher_Id", voucherModel.VoucherId,
                    "@voucher_Name", voucherModel.VoucherName,
                    "@voucher_Price", voucherModel.Price,
                    "@voucher_MinimumPrice", voucherModel.MinimumPrice,
                    "@voucher_Quantity", voucherModel.Quantity,
                    "@voucher_StartDay", voucherModel.StartDay,
                    "@voucher_EndDate", voucherModel.EndDate,
                    "@voucher_Deleted", voucherModel.Deleted);
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

        public List<VoucherModel> Search(string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_voucher_search",
                    "@voucher_Name", name);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<VoucherModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VoucherModel> Pagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_voucher_pagination",
                    "@voucher_pageNumber", pageNumber,
                    "@voucher_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<VoucherModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VoucherModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_voucher_search_pagination",
                    "@voucher_Name", name,
                    "@voucher_pageNumber", pageNumber,
                    "@voucher_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<VoucherModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
