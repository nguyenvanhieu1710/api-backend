using BLL;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.IO;

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

        [Route("create")]
        [HttpPost]
        public bool Create([FromBody] ProductModel productModel)
        {
            try
            {
                return _interfaceProductBLL.Create(productModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("upload-image")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $@"product/{file.FileName}";
                    var fullPath = CreatePathFile(filePath);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    //return Ok(new { filePath });
                    return Ok(new { fullPath });
                }
                else { return BadRequest(); }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [NonAction]
        private string CreatePathFile(string RelativePathFileName)
        {
            try
            {
                string serverRootPath = _path;
                string fullPathFile = $@"{serverRootPath}\{RelativePathFileName}";
                string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);
                if (!Directory.Exists(fullPathFolder))
                {
                    Directory.CreateDirectory(fullPathFolder);
                }
                return fullPathFile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("update")]
        [HttpPost]
        public bool Update(ProductModel productModel)
        {
            return _interfaceProductBLL.Update(productModel);
        }

        [Route("delete/{id}")]
        [HttpPost]
        public bool Delete(int id)
        {
            return _interfaceProductBLL.Delete(id);
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

        [Route("get-data-deleted-pagination")]
        [HttpGet]
        public List<ProductModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            return _interfaceProductBLL.GetDataDeletedPagination(pageNumber, pageSize);
        }

        [Route("search-and-pagination")]
        [HttpGet]
        public List<ProductModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _interfaceProductBLL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
