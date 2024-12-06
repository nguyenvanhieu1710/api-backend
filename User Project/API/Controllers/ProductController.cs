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
            List<ProductModel> products = _interfaceProductBLL.GetAll();
            foreach (var item in products)
            {
                if (!string.IsNullOrEmpty(item.ProductImage))
                {
                    var filePath = Path.Combine("D:/Documents Of Year 3/Service-oriented Software Development/Admin Project/Image/product", item.ProductImage);

                    item.ProductImage = Utils.ImageFile.ConvertImageToBase64(filePath);
                }
            }
            return products;
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
            List<ProductModel> products = _interfaceProductBLL.GetBestSellingProduct();
            foreach (var item in products)
            {
                if (!string.IsNullOrEmpty(item.ProductImage))
                {
                    var filePath = Path.Combine("D:/Documents Of Year 3/Service-oriented Software Development/Admin Project/Image/product", item.ProductImage);

                    item.ProductImage = Utils.ImageFile.ConvertImageToBase64(filePath);
                }
            }
            return products;
        }

        [Route("search/{name}")]
        [HttpGet]
        public List<ProductModel> Search(string name)
        {
            List<ProductModel> products = _interfaceProductBLL.Search(name);
            foreach (var item in products)
            {
                if (!string.IsNullOrEmpty(item.ProductImage))
                {
                    var filePath = Path.Combine("D:/Documents Of Year 3/Service-oriented Software Development/Admin Project/Image/product", item.ProductImage);

                    item.ProductImage = Utils.ImageFile.ConvertImageToBase64(filePath);
                }
            }
            return products;
        }

        [Route("page={pageNumber}&pageSize={pageSize}")]
        [HttpGet]
        public List<ProductModel> Pagination(int pageNumber, int pageSize)
        {
            List<ProductModel> products = _interfaceProductBLL.Pagination(pageNumber, pageSize);
            foreach (var item in products)
            {
                if (!string.IsNullOrEmpty(item.ProductImage))
                {
                    var filePath = Path.Combine("D:/Documents Of Year 3/Service-oriented Software Development/Admin Project/Image/product", item.ProductImage);

                    item.ProductImage = Utils.ImageFile.ConvertImageToBase64(filePath);
                }
            }
            return products;
        }

        [Route("search-and-pagination")]
        [HttpGet]
        public List<ProductModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _interfaceProductBLL.SearchAndPagination(name, pageNumber, pageSize);
        }
    }
}
