using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderDAL : IOrderDAL
    {
        private IDatabaseHelper _IDatabaseHelper;
        public OrderDAL(IDatabaseHelper databaseHelper)
        {
            _IDatabaseHelper = databaseHelper;
        }
        public bool Create(OrdersModel orderModel)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_orders_create",
                    "@orders_UserId", orderModel.UserId,
                    "@orders_StaffId", orderModel.StaffId,
                    "@orders_OrderStatus", orderModel.OrderStatus,
                    "@orders_DayBuy", orderModel.DayBuy,
                    "@orders_DeliveryAddress", orderModel.DeliveryAddress,
                    "@orders_Evaluate", orderModel.Evaluate,
                    "@listjson_orderDetail", orderModel.listjson_orderDetail != null ? MessageConvert.SerializeObject(orderModel.listjson_orderDetail) : null);
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
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_orders_delete",
                    "@orders_Id", id);
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

        public List<OrdersModel> GetAll()
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_orders_all");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }

                var orders = new List<OrdersModel>();

                if (result.Rows.Count == 0)
                {
                    return null;
                }
                // Nhóm dữ liệu theo OrderId
                var groupedData = result.AsEnumerable()
                                        .GroupBy(row => row["OrderId"])
                                        .ToList();

                foreach (var group in groupedData)
                {
                    // Tạo đối tượng OrdersModel từ dữ liệu nhóm
                    var order = new OrdersModel
                    {
                        OrderId = Convert.ToInt32(group.Key),
                        UserId = Convert.ToInt32(group.First()["UserId"]),
                        StaffId = Convert.ToInt32(group.First()["StaffId"]),
                        OrderStatus = group.First()["OrderStatus"].ToString(),
                        DayBuy = Convert.ToDateTime(group.First()["DayBuy"]),
                        DeliveryAddress = group.First()["DeliveryAddress"].ToString(),
                        Evaluate = Convert.ToInt32(group.First()["Evaluate"]),
                        Deleted = Convert.ToBoolean(group.First()["Deleted"]),
                        listjson_orderDetail = new List<OrderDetailModel>()
                    };

                    // Thêm chi tiết đơn hàng vào danh sách
                    foreach (var row in group)
                    {
                        if (row["OrderDetailId"] != DBNull.Value)
                        {
                            var orderDetail = new OrderDetailModel
                            {
                                OrderDetailId = Convert.ToInt32(row["OrderDetailId"]),
                                OrderId = order.OrderId,
                                ProductId = Convert.ToInt32(row["ProductId"]),
                                Quantity = Convert.ToInt32(row["Quantity"]),
                                Price = Convert.ToDecimal(row["Price"]),
                                DiscountAmount = Convert.ToDecimal(row["DiscountAmount"]),
                                VoucherId = Convert.ToInt32(row["VoucherId"])
                            };

                            order.listjson_orderDetail.Add(orderDetail);
                        }
                    }

                    orders.Add(order);
                }

                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OrdersModel GetDataById(int id)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_orders_get_data_by_id",
                    "@orders_Id", id);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }

                var orders = new List<OrdersModel>();

                if (result.Rows.Count == 0)
                {
                    return null;
                }

                // Nhóm dữ liệu theo OrderId
                var groupedData = result.AsEnumerable()
                                        .GroupBy(row => row["OrderId"])
                                        .FirstOrDefault();

                if (groupedData == null) return null;

                // Tạo đối tượng OrdersModel từ dữ liệu nhóm
                var order = new OrdersModel
                {
                    OrderId = Convert.ToInt32(groupedData.Key),
                    UserId = groupedData.First()["UserId"] != DBNull.Value ? Convert.ToInt32(groupedData.First()["UserId"]) : 0,
                    StaffId = groupedData.First()["StaffId"] != DBNull.Value ? Convert.ToInt32(groupedData.First()["StaffId"]) : 0,
                    OrderStatus = groupedData.First()["OrderStatus"]?.ToString(),
                    DayBuy = groupedData.First()["DayBuy"] != DBNull.Value ? Convert.ToDateTime(groupedData.First()["DayBuy"]) : DateTime.MinValue,
                    DeliveryAddress = groupedData.First()["DeliveryAddress"]?.ToString(),
                    Evaluate = groupedData.First()["Evaluate"] != DBNull.Value ? Convert.ToInt32(groupedData.First()["Evaluate"]) : 0,
                    Deleted = groupedData.First()["Deleted"] != DBNull.Value && Convert.ToBoolean(groupedData.First()["Deleted"]),
                    listjson_orderDetail = new List<OrderDetailModel>()
                };

                // Thêm chi tiết đơn hàng vào danh sách
                foreach (var row in groupedData)
                {
                    if (row["OrderDetailId"] != DBNull.Value)
                    {
                        var orderDetail = new OrderDetailModel
                        {
                            OrderDetailId = Convert.ToInt32(row["OrderDetailId"]),
                            OrderId = order.OrderId,
                            ProductId = row["ProductId"] != DBNull.Value ? Convert.ToInt32(row["ProductId"]) : 0,
                            Quantity = row["Quantity"] != DBNull.Value ? Convert.ToInt32(row["Quantity"]) : 0,
                            Price = row["Price"] != DBNull.Value ? Convert.ToDecimal(row["Price"]) : 0,
                            DiscountAmount = row["DiscountAmount"] != DBNull.Value ? Convert.ToDecimal(row["DiscountAmount"]) : 0,
                            VoucherId = row["VoucherId"] != DBNull.Value ? Convert.ToInt32(row["VoucherId"]) : 0
                        };

                        order.listjson_orderDetail.Add(orderDetail);
                    }
                }

                return order;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrdersModel> GetDataByUserIdAndPagination(int userId, int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_orders_get_data_by_userid_and_pagination",
                    "@userId", userId,
                    "@orders_pageNumber", pageNumber,
                    "@orders_pageSize", pageSize);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                var orders = new List<OrdersModel>();

                if (result.Rows.Count == 0)
                {
                    return null;
                }
                // Nhóm dữ liệu theo OrderId
                var groupedData = result.AsEnumerable()
                                        .GroupBy(row => row["OrderId"])
                                        .ToList();

                foreach (var group in groupedData)
                {
                    // Tạo đối tượng OrdersModel từ dữ liệu nhóm
                    var order = new OrdersModel
                    {
                        OrderId = Convert.ToInt32(group.Key),
                        UserId = Convert.ToInt32(group.First()["UserId"]),
                        StaffId = Convert.ToInt32(group.First()["StaffId"]),
                        OrderStatus = group.First()["OrderStatus"].ToString(),
                        DayBuy = Convert.ToDateTime(group.First()["DayBuy"]),
                        DeliveryAddress = group.First()["DeliveryAddress"].ToString(),
                        Evaluate = Convert.ToInt32(group.First()["Evaluate"]),
                        Deleted = Convert.ToBoolean(group.First()["Deleted"]),
                        listjson_orderDetail = new List<OrderDetailModel>()
                    };

                    // Thêm chi tiết đơn hàng vào danh sách
                    foreach (var row in group)
                    {
                        if (row["OrderDetailId"] != DBNull.Value)
                        {
                            var orderDetail = new OrderDetailModel
                            {
                                OrderDetailId = Convert.ToInt32(row["OrderDetailId"]),
                                OrderId = order.OrderId,
                                ProductId = Convert.ToInt32(row["ProductId"]),
                                Quantity = Convert.ToInt32(row["Quantity"]),
                                Price = Convert.ToDecimal(row["Price"]),
                                DiscountAmount = Convert.ToDecimal(row["DiscountAmount"]),
                                VoucherId = Convert.ToInt32(row["VoucherId"])
                            };

                            order.listjson_orderDetail.Add(orderDetail);
                        }
                    }

                    orders.Add(order);
                }

                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrdersModel> Pagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_orders_pagination",
                    "@orders_pageNumber", pageNumber,
                    "@orders_pageSize", pageSize);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                var orders = new List<OrdersModel>();

                if (result.Rows.Count == 0)
                {
                    return null;
                }
                // Nhóm dữ liệu theo OrderId
                var groupedData = result.AsEnumerable()
                                        .GroupBy(row => row["OrderId"])
                                        .ToList();

                foreach (var group in groupedData)
                {
                    // Tạo đối tượng OrdersModel từ dữ liệu nhóm
                    var order = new OrdersModel
                    {
                        OrderId = Convert.ToInt32(group.Key),
                        UserId = Convert.ToInt32(group.First()["UserId"]),
                        StaffId = Convert.ToInt32(group.First()["StaffId"]),
                        OrderStatus = group.First()["OrderStatus"].ToString(),
                        DayBuy = Convert.ToDateTime(group.First()["DayBuy"]),
                        DeliveryAddress = group.First()["DeliveryAddress"].ToString(),
                        Evaluate = Convert.ToInt32(group.First()["Evaluate"]),
                        Deleted = Convert.ToBoolean(group.First()["Deleted"]),
                        listjson_orderDetail = new List<OrderDetailModel>()
                    };

                    // Thêm chi tiết đơn hàng vào danh sách
                    foreach (var row in group)
                    {
                        if (row["OrderDetailId"] != DBNull.Value)
                        {
                            var orderDetail = new OrderDetailModel
                            {
                                OrderDetailId = Convert.ToInt32(row["OrderDetailId"]),
                                OrderId = order.OrderId,
                                ProductId = Convert.ToInt32(row["ProductId"]),
                                Quantity = Convert.ToInt32(row["Quantity"]),
                                Price = Convert.ToDecimal(row["Price"]),
                                DiscountAmount = Convert.ToDecimal(row["DiscountAmount"]),
                                VoucherId = Convert.ToInt32(row["VoucherId"])
                            };

                            order.listjson_orderDetail.Add(orderDetail);
                        }
                    }

                    orders.Add(order);
                }

                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<OrdersModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_orders_deleted_pagination",
                    "@orders_pageNumber", pageNumber,
                    "@orders_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                var orders = new List<OrdersModel>();

                if (result.Rows.Count == 0)
                {
                    return null;
                }
                // Nhóm dữ liệu theo OrderId
                var groupedData = result.AsEnumerable()
                                        .GroupBy(row => row["OrderId"])
                                        .ToList();

                foreach (var group in groupedData)
                {
                    // Tạo đối tượng OrdersModel từ dữ liệu nhóm
                    var order = new OrdersModel
                    {
                        OrderId = Convert.ToInt32(group.Key),
                        UserId = Convert.ToInt32(group.First()["UserId"]),
                        StaffId = Convert.ToInt32(group.First()["StaffId"]),
                        OrderStatus = group.First()["OrderStatus"].ToString(),
                        DayBuy = Convert.ToDateTime(group.First()["DayBuy"]),
                        DeliveryAddress = group.First()["DeliveryAddress"].ToString(),
                        Evaluate = Convert.ToInt32(group.First()["Evaluate"]),
                        Deleted = Convert.ToBoolean(group.First()["Deleted"]),
                        listjson_orderDetail = new List<OrderDetailModel>()
                    };

                    // Thêm chi tiết đơn hàng vào danh sách
                    foreach (var row in group)
                    {
                        if (row["OrderDetailId"] != DBNull.Value)
                        {
                            var orderDetail = new OrderDetailModel
                            {
                                OrderDetailId = Convert.ToInt32(row["OrderDetailId"]),
                                OrderId = order.OrderId,
                                ProductId = Convert.ToInt32(row["ProductId"]),
                                Quantity = Convert.ToInt32(row["Quantity"]),
                                Price = Convert.ToDecimal(row["Price"]),
                                DiscountAmount = Convert.ToDecimal(row["DiscountAmount"]),
                                VoucherId = Convert.ToInt32(row["VoucherId"])
                            };

                            order.listjson_orderDetail.Add(orderDetail);
                        }
                    }

                    orders.Add(order);
                }

                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<OrdersModel> Search(string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_orders_search_user",
                    "@userName", name);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                var orders = new List<OrdersModel>();

                if (result.Rows.Count == 0)
                {
                    return null;
                }
                // Nhóm dữ liệu theo OrderId
                var groupedData = result.AsEnumerable()
                                        .GroupBy(row => row["OrderId"])
                                        .ToList();

                foreach (var group in groupedData)
                {
                    // Tạo đối tượng OrdersModel từ dữ liệu nhóm
                    var order = new OrdersModel
                    {
                        OrderId = Convert.ToInt32(group.Key),
                        UserId = Convert.ToInt32(group.First()["UserId"]),
                        StaffId = Convert.ToInt32(group.First()["StaffId"]),
                        OrderStatus = group.First()["OrderStatus"].ToString(),
                        DayBuy = Convert.ToDateTime(group.First()["DayBuy"]),
                        DeliveryAddress = group.First()["DeliveryAddress"].ToString(),
                        Evaluate = Convert.ToInt32(group.First()["Evaluate"]),
                        Deleted = Convert.ToBoolean(group.First()["Deleted"]),
                        listjson_orderDetail = new List<OrderDetailModel>()
                    };

                    // Thêm chi tiết đơn hàng vào danh sách
                    foreach (var row in group)
                    {
                        if (row["OrderDetailId"] != DBNull.Value)
                        {
                            var orderDetail = new OrderDetailModel
                            {
                                OrderDetailId = Convert.ToInt32(row["OrderDetailId"]),
                                OrderId = order.OrderId,
                                ProductId = Convert.ToInt32(row["ProductId"]),
                                Quantity = Convert.ToInt32(row["Quantity"]),
                                Price = Convert.ToDecimal(row["Price"]),
                                DiscountAmount = Convert.ToDecimal(row["DiscountAmount"]),
                                VoucherId = Convert.ToInt32(row["VoucherId"])
                            };

                            order.listjson_orderDetail.Add(orderDetail);
                        }
                    }

                    orders.Add(order);
                }

                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrdersModel> SearchByProductName(string productName)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_orders_search_product",
                    "@productName", productName);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                var orders = new List<OrdersModel>();

                if (result.Rows.Count == 0)
                {
                    return null;
                }
                // Nhóm dữ liệu theo OrderId
                var groupedData = result.AsEnumerable()
                                        .GroupBy(row => row["OrderId"])
                                        .ToList();

                foreach (var group in groupedData)
                {
                    // Tạo đối tượng OrdersModel từ dữ liệu nhóm
                    var order = new OrdersModel
                    {
                        OrderId = Convert.ToInt32(group.Key),
                        UserId = Convert.ToInt32(group.First()["UserId"]),
                        StaffId = Convert.ToInt32(group.First()["StaffId"]),
                        OrderStatus = group.First()["OrderStatus"].ToString(),
                        DayBuy = Convert.ToDateTime(group.First()["DayBuy"]),
                        DeliveryAddress = group.First()["DeliveryAddress"].ToString(),
                        Evaluate = Convert.ToInt32(group.First()["Evaluate"]),
                        Deleted = Convert.ToBoolean(group.First()["Deleted"]),
                        listjson_orderDetail = new List<OrderDetailModel>()
                    };

                    // Thêm chi tiết đơn hàng vào danh sách
                    foreach (var row in group)
                    {
                        if (row["OrderDetailId"] != DBNull.Value)
                        {
                            var orderDetail = new OrderDetailModel
                            {
                                OrderDetailId = Convert.ToInt32(row["OrderDetailId"]),
                                OrderId = order.OrderId,
                                ProductId = Convert.ToInt32(row["ProductId"]),
                                Quantity = Convert.ToInt32(row["Quantity"]),
                                Price = Convert.ToDecimal(row["Price"]),
                                DiscountAmount = Convert.ToDecimal(row["DiscountAmount"]),
                                VoucherId = Convert.ToInt32(row["VoucherId"])
                            };

                            order.listjson_orderDetail.Add(orderDetail);
                        }
                    }

                    orders.Add(order);
                }

                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrdersModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_orders_search_pagination",
                    "@orders_pageNumber", pageNumber,
                    "@orders_pageSize", pageSize,
                    "@userName", name);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                var orders = new List<OrdersModel>();

                if (result.Rows.Count == 0)
                {
                    return null;
                }
                // Nhóm dữ liệu theo OrderId
                var groupedData = result.AsEnumerable()
                                        .GroupBy(row => row["OrderId"])
                                        .ToList();

                foreach (var group in groupedData)
                {
                    // Tạo đối tượng OrdersModel từ dữ liệu nhóm
                    var order = new OrdersModel
                    {
                        OrderId = Convert.ToInt32(group.Key),
                        UserId = Convert.ToInt32(group.First()["UserId"]),
                        StaffId = Convert.ToInt32(group.First()["StaffId"]),
                        OrderStatus = group.First()["OrderStatus"].ToString(),
                        DayBuy = Convert.ToDateTime(group.First()["DayBuy"]),
                        DeliveryAddress = group.First()["DeliveryAddress"].ToString(),
                        Evaluate = Convert.ToInt32(group.First()["Evaluate"]),
                        Deleted = Convert.ToBoolean(group.First()["Deleted"]),
                        listjson_orderDetail = new List<OrderDetailModel>()
                    };

                    // Thêm chi tiết đơn hàng vào danh sách
                    foreach (var row in group)
                    {
                        if (row["OrderDetailId"] != DBNull.Value)
                        {
                            var orderDetail = new OrderDetailModel
                            {
                                OrderDetailId = Convert.ToInt32(row["OrderDetailId"]),
                                OrderId = order.OrderId,
                                ProductId = Convert.ToInt32(row["ProductId"]),
                                Quantity = Convert.ToInt32(row["Quantity"]),
                                Price = Convert.ToDecimal(row["Price"]),
                                DiscountAmount = Convert.ToDecimal(row["DiscountAmount"]),
                                VoucherId = Convert.ToInt32(row["VoucherId"])
                            };

                            order.listjson_orderDetail.Add(orderDetail);
                        }
                    }

                    orders.Add(order);
                }

                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(OrdersModel orderModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_orders_update",
                    "@orders_Id", orderModel.OrderId,
                    "@orders_UserId", orderModel.UserId,
                    "@orders_StaffId", orderModel.StaffId,
                    "@orders_OrderStatus", orderModel.OrderStatus,
                    "@orders_DayBuy", orderModel.DayBuy,
                    "@orders_DeliveryAddress", orderModel.DeliveryAddress,
                    "@orders_Evaluate", orderModel.Evaluate,
                    "@orders_Deleted", orderModel.Deleted,
                    "@listjson_orderDetail", orderModel.listjson_orderDetail != null ? MessageConvert.SerializeObject(orderModel.listjson_orderDetail) : null);
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
