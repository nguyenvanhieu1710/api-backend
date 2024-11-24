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

        [Route("get-data-by-id/{id}")]
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

        [Route("create")]
        [HttpPost]
        public bool Create([FromBody] CategoryModel categoryModel)
        {
            return _ICategoryBLL.Create(categoryModel);
        }

        [Route("update")]
        [HttpPost]
        public bool Update(CategoryModel categoryModel)
        {
            return _ICategoryBLL.Update(categoryModel);
        }

        [Route("delete/{id}")]
        [HttpPost]
        public bool Delete(int id)
        {
            return _ICategoryBLL.Delete(id);
        }

        [Route("search/{name}")]
        [HttpGet]
        public List<CategoryModel> Search(string name)
        {
            return _ICategoryBLL.Search(name);
        }

        [Route("page={pageNumber}&pageSize={pageSize}")]
        [HttpGet]
        public List<CategoryModel> Pagination(int pageNumber, int pageSize)
        {
            return _ICategoryBLL.Pagination(pageNumber, pageSize);
        }

        [Route("search-and-pagination")]
        [HttpGet]
        public List<CategoryModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _ICategoryBLL.SearchAndPagination(pageNumber, pageSize, name);
        }

    }
}
