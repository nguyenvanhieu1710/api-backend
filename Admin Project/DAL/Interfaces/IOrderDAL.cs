using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrderDAL
    {
        OrdersModel GetDataById(int id);
        List<OrdersModel> GetAll();
        bool Create(OrdersModel orderModel);
        bool Update(OrdersModel orderModel);
        bool Delete(int id);
        List<OrdersModel> Search(string name);
        List<OrdersModel> Pagination(int pageNumber, int pageSize);
        List<OrdersModel> SearchAndPagination(int pageNumber, int pageSize, string name);
    }
}
