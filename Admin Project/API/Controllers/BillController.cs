using BLL;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private IOrdersBLL _IOrdersBLL;
        public BillController(IOrdersBLL iordersBLL)
        {
            _IOrdersBLL = iordersBLL;
        }

        [Route("create")]
        [HttpPost]
        public bool Create(OrdersModel ordersModel)
        {
            return _IOrdersBLL.Create(ordersModel);            
        }

        [Route("update")]
        [HttpPost]
        public bool Update(OrdersModel ordersModel)
        {
            return _IOrdersBLL.Update(ordersModel);
        }

        [Route("delete/{id}")]
        [HttpPost]
        public bool Delete(int id)
        {
            return _IOrdersBLL.Delete(id);
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

        [Route("search/{name}")]
        [HttpGet]
        public List<OrdersModel> Search(string name)
        {
            return _IOrdersBLL.Search(name);
        }

        [Route("page={pageNumber}&pageSize={pageSize}")]
        [HttpGet]
        public List<OrdersModel> Pagination(int pageNumber, int pageSize)
        {
            return _IOrdersBLL.Pagination(pageNumber, pageSize);
        }

        [Route("search-and-pagination")]
        [HttpGet]
        public List<OrdersModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _IOrdersBLL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
