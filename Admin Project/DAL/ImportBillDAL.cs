using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
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

                var importBills = new List<ImportBillModel>();

                if (result.Rows.Count > 0)
                {
                    var groupedData = result.AsEnumerable()
                                            .GroupBy(row => row["ImportBillId"]) // Nhóm theo ImportBillId
                                            .ToList();

                    foreach (var group in groupedData)
                    {
                        var importBill = new ImportBillModel
                        {
                            ImportBillId = Convert.ToInt32(group.Key),
                            SupplierId = Convert.ToInt32(group.First()["SupplierId"]),
                            StaffId = Convert.ToInt32(group.First()["StaffId"]),
                            InputDay = Convert.ToDateTime(group.First()["InputDay"]),
                            Deleted = Convert.ToBoolean(group.First()["Deleted"]),
                            listjson_importBillDetail = new List<ImportBillDetailModel>()
                        };

                        // Thêm chi tiết hóa đơn vào danh sách chi tiết của hóa đơn
                        foreach (var row in group)
                        {
                            if (row["ImportBillDetailId"] != DBNull.Value)
                            {
                                var importBillDetail = new ImportBillDetailModel
                                {
                                    ImportBillDetailId = Convert.ToInt32(row["ImportBillDetailId"]),
                                    ImportBillId = importBill.ImportBillId,
                                    ProductId = Convert.ToInt32(row["ProductId"]),
                                    ImportPrice = Convert.ToDecimal(row["ImportPrice"]),
                                    ImportQuantity = Convert.ToInt32(row["ImportQuantity"])
                                };

                                importBill.listjson_importBillDetail.Add(importBillDetail);
                            }
                        }

                        importBills.Add(importBill);
                    }
                }

                return importBills;
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

                var importBills = new List<ImportBillModel>();

                if (result.Rows.Count > 0)
                {
                    var groupedData = result.AsEnumerable()
                                            .GroupBy(row => row["ImportBillId"]) // Nhóm theo ImportBillId
                                            .ToList();

                    foreach (var group in groupedData)
                    {
                        var importBill = new ImportBillModel
                        {
                            ImportBillId = Convert.ToInt32(group.Key),
                            SupplierId = Convert.ToInt32(group.First()["SupplierId"]),
                            StaffId = Convert.ToInt32(group.First()["StaffId"]),
                            InputDay = Convert.ToDateTime(group.First()["InputDay"]),
                            Deleted = Convert.ToBoolean(group.First()["Deleted"]),
                            listjson_importBillDetail = new List<ImportBillDetailModel>()
                        };

                        // Thêm chi tiết hóa đơn vào danh sách chi tiết của hóa đơn
                        foreach (var row in group)
                        {
                            if (row["ImportBillDetailId"] != DBNull.Value)
                            {
                                var importBillDetail = new ImportBillDetailModel
                                {
                                    ImportBillDetailId = Convert.ToInt32(row["ImportBillDetailId"]),
                                    ImportBillId = importBill.ImportBillId,
                                    ProductId = Convert.ToInt32(row["ProductId"]),
                                    ImportPrice = Convert.ToDecimal(row["ImportPrice"]),
                                    ImportQuantity = Convert.ToInt32(row["ImportQuantity"])
                                };

                                importBill.listjson_importBillDetail.Add(importBillDetail);
                            }
                        }

                        importBills.Add(importBill);
                    }
                }

                return importBills;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ImportBillModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_importBill_deleted_pagination",
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
