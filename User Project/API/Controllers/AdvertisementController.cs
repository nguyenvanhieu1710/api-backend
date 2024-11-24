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
    }
}
