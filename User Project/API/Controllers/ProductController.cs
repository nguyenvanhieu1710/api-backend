using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductBLL _interfaceProductBLL;
        private string _path;
        public ProductController(IProductBLL InterfaceProductBLL, IConfiguration configuration)
        {
            _interfaceProductBLL = InterfaceProductBLL;
            _path = configuration["AppSettings:PATH"];
        }
        
        [Route("get-data-by-id/{id}")]
        [HttpGet]
        public ProductModel GetDataById(int id)
        {
            return _interfaceProductBLL.GetDataById(id);
        }
        [Route("get-all")]
        [HttpGet]
        public List<ProductModel> GetAll()
        {
            return _interfaceProductBLL.GetAll();
        }

        [Route("new-imported-product")]
        [HttpGet]
        public List<ProductModel> NewImportedProduct()
        {
            return new List<ProductModel>();
        }

        [Route("get-best-selling-product")]
        [HttpGet]
        public List<ProductModel> BestSellingProduct()
        {
            return _interfaceProductBLL.GetBestSellingProduct();
        }

        [Route("search/{name}")]
        [HttpGet]
        public List<ProductModel> Search(string name)
        {
            return _interfaceProductBLL.Search(name);
        }

        [Route("page={pageNumber}&pageSize={pageSize}")]
        [HttpGet]
        public List<ProductModel> Pagination(int pageNumber, int pageSize)
        {
            return _interfaceProductBLL.Pagination(pageNumber, pageSize);
        }

        [Route("search-and-pagination")]
        [HttpGet]
        public List<ProductModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _interfaceProductBLL.SearchAndPagination(name, pageNumber, pageSize);
        }
    }
}
