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
                return result.ConvertTo<OrdersModel>().ToList();
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
                return result.ConvertTo<OrdersModel>().FirstOrDefault();
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
                    "@pageNumber", pageNumber,
                    "@pageSize", pageSize);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<OrdersModel>().ToList();
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
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_orders_search",
                    "@@userName", name);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<OrdersModel>().ToList();
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
