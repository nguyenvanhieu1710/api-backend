using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrdersBLL _IOrdersBLL;
        public OrdersController(IOrdersBLL iordersBLL)
        {
            _IOrdersBLL = iordersBLL;
        }

        [Route("create")]
        [HttpPost]
        public bool Create(OrdersModel ordersModel)
        {
            return _IOrdersBLL.Create(ordersModel);
        }

        [Route("get-data-by-id/{id}")]
        [HttpGet]
        public OrdersModel GetDataById(int id)
        {
            return _IOrdersBLL.GetDataById(id);
        }

        [Route("get-data-by-userId-and-pagination")]
        [HttpGet]
        public List<OrdersModel> GetDataByUserIdAndPagination(int userId, int pageNumber, int pageSize)
        {
            return _IOrdersBLL.GetDataByUserIdAndPagination(userId, pageNumber, pageSize);
        }

        [Route("search-by-productname")]
        [HttpGet]
        public List<OrdersModel> SearchByProductName(string productName)
        {
            return _IOrdersBLL.SearchByProductName(productName);
        }

        [Route("page={pageNumber}&pageSize={pageSize}")]
        [HttpGet]
        public List<OrdersModel> Pagination(int pageNumber, int pageSize)
        {
            return _IOrdersBLL.Pagination(pageNumber, pageSize);
        }
    }
}
