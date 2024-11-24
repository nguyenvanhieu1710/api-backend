using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private ISupplierBLL _interfaceSupplierBLL;
        public SupplierController(ISupplierBLL InterfaceSupplierBLL)
        {
            _interfaceSupplierBLL = InterfaceSupplierBLL;
        }

        [Route("create")]
        [HttpPost]
        public bool Create([FromBody] SupplierModel supplierModel)
        {
            return _interfaceSupplierBLL.Create(supplierModel);
        }

        [Route("update")]
        [HttpPost]
        public bool Update(SupplierModel supplierModel)
        {
            return _interfaceSupplierBLL.Update(supplierModel);
        }

        [Route("delete/{id}")]
        [HttpPost]
        public bool Delete(int id)
        {
            return _interfaceSupplierBLL.Delete(id);
        }

        [Route("get-data-by-id/{id}")]
        [HttpGet]
        public SupplierModel GetDataById(int id)
        {
            return _interfaceSupplierBLL.GetDataById(id);
        }

        [Route("get-all")]
        [HttpGet]
        public List<SupplierModel> GetAll()
        {
            return _interfaceSupplierBLL.GetAll();
        }

        [Route("search/{name}")]
        [HttpGet]
        public List<SupplierModel> Search(string name)
        {
            return _interfaceSupplierBLL.Search(name);
        }

        [Route("page={pageNumber}&pageSize={pageSize}")]
        [HttpGet]
        public List<SupplierModel> Pagination(int pageNumber, int pageSize)
        {
            return _interfaceSupplierBLL.Pagination(pageNumber, pageSize);
        }

        [Route("search-and-pagination")]
        [HttpGet]
        public List<SupplierModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _interfaceSupplierBLL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
