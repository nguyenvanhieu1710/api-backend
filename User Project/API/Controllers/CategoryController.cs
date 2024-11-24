using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Numerics;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryBLL _ICategoryBLL;
        public CategoryController(ICategoryBLL categoryBLL)
        {
            _ICategoryBLL = categoryBLL;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public CategoryModel GetDataById(int id)
        {
            return _ICategoryBLL.GetDataById(id);
        }

        [Route("get-all")]
        [HttpGet]
        public List<CategoryModel> GetAll()
        {
            return _ICategoryBLL.GetAll();
        }
        
        [Route("search/{name}")]
        [HttpGet]
        public List<CategoryModel> Search(string name)
        {
            return _ICategoryBLL.Search(name);
        }
    }
}
