using BLL.Interfaces;
using DAL;
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

        public List<OrdersModel> Pagination(int pageNumber, int pageSize)
        {
            return _IOrderDAL.Pagination(pageNumber, pageSize);
        }
        public List<OrdersModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            return _IOrderDAL.GetDataDeletedPagination(pageNumber, pageSize);
        }
        public List<OrdersModel> Search(string name)
        {
            return _IOrderDAL.Search(name);
        }

        public List<OrdersModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _IOrderDAL.SearchAndPagination(pageNumber, pageSize, name);
        }

        public bool Update(OrdersModel orderModel)
        {
            return _IOrderDAL.Update(orderModel);
        }
    }
}
