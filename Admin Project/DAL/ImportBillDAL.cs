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

                if (result.Rows.Count == 0)
                {
                    return null;
                }
                var groupedData = result.AsEnumerable()
                                        .GroupBy(row => row["ImportBillId"])
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
                // Thực thi Stored Procedure để lấy dữ liệu
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(
                    out msgError,
                    "sp_importBill_get_data_by_id",
                    "@importBill_Id", id
                );

                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }

                if (result.Rows.Count == 0)
                {
                    return null;
                }

                // Nhóm dữ liệu theo ImportBillId
                var groupedData = result.AsEnumerable()
                                        .GroupBy(row => row["ImportBillId"])
                                        .FirstOrDefault();

                if (groupedData == null) return null;

                // Tạo đối tượng ImportBillModel từ dữ liệu nhóm
                var importBill = new ImportBillModel
                {
                    ImportBillId = Convert.ToInt32(groupedData.Key),
                    SupplierId = groupedData.First()["SupplierId"] != DBNull.Value ? Convert.ToInt32(groupedData.First()["SupplierId"]) : 0,
                    StaffId = groupedData.First()["StaffId"] != DBNull.Value ? Convert.ToInt32(groupedData.First()["StaffId"]) : 0,
                    InputDay = groupedData.First()["InputDay"] != DBNull.Value ? Convert.ToDateTime(groupedData.First()["InputDay"]) : DateTime.MinValue,
                    Deleted = groupedData.First()["Deleted"] != DBNull.Value && Convert.ToBoolean(groupedData.First()["Deleted"]),
                    listjson_importBillDetail = new List<ImportBillDetailModel>()
                };

                // Thêm chi tiết hóa đơn vào danh sách
                foreach (var row in groupedData)
                {
                    if (row["ImportBillDetailId"] != DBNull.Value)
                    {
                        var importBillDetail = new ImportBillDetailModel
                        {
                            ImportBillDetailId = Convert.ToInt32(row["ImportBillDetailId"]),
                            ImportBillId = importBill.ImportBillId,
                            ProductId = row["ProductId"] != DBNull.Value ? Convert.ToInt32(row["ProductId"]) : 0,
                            ImportPrice = row["ImportPrice"] != DBNull.Value ? Convert.ToDecimal(row["ImportPrice"]) : 0,
                            ImportQuantity = row["ImportQuantity"] != DBNull.Value ? Convert.ToInt32(row["ImportQuantity"]) : 0
                        };

                        importBill.listjson_importBillDetail.Add(importBillDetail);
                    }
                }

                return importBill;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving import bill data: {ex.Message}", ex);
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

                if (result.Rows.Count == 0)
                {
                    return null;
                }
                var groupedData = result.AsEnumerable()
                                        .GroupBy(row => row["ImportBillId"])
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
                    "@importBill_Deleted", importBillModel.Deleted,
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
