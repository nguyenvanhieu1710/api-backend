using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private IAdvertisementBLL _interfaceAdvertisementBLL;
        public AdvertisementController(IAdvertisementBLL InterfaceAdvertisementBLL)
        {
            _interfaceAdvertisementBLL = InterfaceAdvertisementBLL;
        }

        [Route("create")]
        [HttpPost]
        public bool Create(AdvertisementModel advertisementModel)
        {
            return _interfaceAdvertisementBLL.Create(advertisementModel);
        }

        [Route("update")]
        [HttpPost]
        public bool Update(AdvertisementModel advertisementModel)
        {
            return _interfaceAdvertisementBLL.Update(advertisementModel);            
        }

        [Route("delete/{id}")]
        [HttpPost]
        public bool Delete(int id)
        {
            return _interfaceAdvertisementBLL.Delete(id);
        }

        [Route("get-data-by-id/{id}")]
        [HttpGet]
        public AdvertisementModel GetDataById(int id)
        {
            return _interfaceAdvertisementBLL.GetDataById(id);
        }

        [Route("get-all")]
        [HttpGet]
        public List<AdvertisementModel> GetAll()
        {
            return _interfaceAdvertisementBLL.GetAll();
        }

        [Route("search/{name}")]
        [HttpGet]
        public List<AdvertisementModel> Search(string name)
        {
            return _interfaceAdvertisementBLL.Search(name);
        }

        [Route("page={pageNumber}&pageSize={pageSize}")]
        [HttpGet]
        public List<AdvertisementModel> Pagination(int pageNumber, int pageSize)
        {
            return _interfaceAdvertisementBLL.Pagination(pageNumber, pageSize);
        }

        [Route("search-and-pagination")]
        [HttpGet]
        public List<AdvertisementModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _interfaceAdvertisementBLL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
