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
    public class OrdersBLL : IOrdersBLL
    {
        private IOrderDAL _IOrderDAL;
        public OrdersBLL(IOrderDAL IOrderDAL)
        {
            _IOrderDAL = IOrderDAL;
        }
        public bool Create(OrdersModel orderModel)
        {
            return _IOrderDAL.Create(orderModel);
        }

        public bool Delete(int id)
        {
            return _IOrderDAL.Delete(id);
        }

        public List<OrdersModel> GetAll()
        {
            return _IOrderDAL.GetAll();
        }

        public OrdersModel GetDataById(int id)
        {
            return _IOrderDAL.GetDataById(id);
        }

        public List<OrdersModel> GetDataByUserIdAndPagination(int userId, int pageNumber, int pageSize)
        {
            return _IOrderDAL.GetDataByUserIdAndPagination(userId, pageNumber, pageSize);
        }

        public List<OrdersModel> Pagination(int pageNumber, int pageSize)
        {
            return _IOrderDAL.Pagination(pageNumber, pageSize);
        }

        public List<OrdersModel> Search(string name)
        {
            return _IOrderDAL.Search(name);
        }

        public List<OrdersModel> SearchByProductName(string productName)
        {
            return _IOrderDAL.SearchByProductName(productName);
        }

        public bool Update(OrdersModel orderModel)
        {
            return _IOrderDAL.Update(orderModel);
        }
    }
}
