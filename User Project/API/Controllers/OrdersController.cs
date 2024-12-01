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

        [Route("get-all")]
        [HttpGet]
        public List<OrdersModel> GetAll()
        {
            return _IOrdersBLL.GetAll();
        }
    }
}
