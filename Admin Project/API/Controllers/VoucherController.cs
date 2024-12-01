using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private IVoucherBLL _interfaceVoucherBLL;
        public VoucherController(IVoucherBLL InterfaceVoucherBLL)
        {
            _interfaceVoucherBLL = InterfaceVoucherBLL;
        }

        [Route("create")]
        [HttpPost]
        public bool Create([FromBody] VoucherModel voucherModel)
        {
            return _interfaceVoucherBLL.Create(voucherModel);
        }

        [Route("update")]
        [HttpPost]
        public bool Update(VoucherModel voucherModel)
        {
            return _interfaceVoucherBLL.Update(voucherModel);
        }

        [Route("delete/{id}")]
        [HttpPost]
        public bool Delete(int id)
        {
            return _interfaceVoucherBLL.Delete(id);
        }

        [Route("get-data-by-id/{id}")]
        [HttpGet]
        public VoucherModel GetDataById(int id)
        {
            return _interfaceVoucherBLL.GetDataById(id);
        }

        [Route("get-all")]
        [HttpGet]
        public List<VoucherModel> GetAll()
        {
            return _interfaceVoucherBLL.GetAll();
        }

        [Route("search/{name}")]
        [HttpGet]
        public List<VoucherModel> Search(string name)
        {
            return _interfaceVoucherBLL.Search(name);
        }

        [Route("page={pageNumber}&pageSize={pageSize}")]
        [HttpGet]
        public List<VoucherModel> Pagination(int pageNumber, int pageSize)
        {
            return _interfaceVoucherBLL.Pagination(pageNumber, pageSize);
        }

        [Route("get-data-deleted-pagination")]
        [HttpGet]
        public List<VoucherModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            return _interfaceVoucherBLL.GetDataDeletedPagination(pageNumber, pageSize);
        }

        [Route("search-and-pagination")]
        [HttpGet]
        public List<VoucherModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _interfaceVoucherBLL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
