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
        private string _path;
        public CategoryController(ICategoryBLL categoryBLL, IConfiguration configuration)
        {
            _ICategoryBLL = categoryBLL;
            _path = configuration["AppSettings:PATH"];
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

        [Route("upload-image")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $@"category/{file.FileName}";
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
            List<CategoryModel> categories = _ICategoryBLL.Pagination(pageNumber, pageSize);
            foreach (var item in categories)
            {
                if (!string.IsNullOrEmpty(item.CategoryImage))
                {
                    var filePath = Path.Combine("D:/Documents Of Year 3/Service-oriented Software Development/Admin Project/Image/category", item.CategoryImage);

                    item.CategoryImage = Utils.ImageFile.ConvertImageToBase64(filePath);
                }
            }
            return categories;
        }

        [Route("get-data-deleted-pagination")]
        [HttpGet]
        public List<CategoryModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            List<CategoryModel> categories = _ICategoryBLL.GetDataDeletedPagination(pageNumber, pageSize);
            foreach (var item in categories)
            {
                if (!string.IsNullOrEmpty(item.CategoryImage))
                {
                    var filePath = Path.Combine("D:/Documents Of Year 3/Service-oriented Software Development/Admin Project/Image/category", item.CategoryImage);

                    item.CategoryImage = Utils.ImageFile.ConvertImageToBase64(filePath);
                }
            }
            return categories;
        }

        [Route("search-and-pagination")]
        [HttpGet]
        public List<CategoryModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _ICategoryBLL.SearchAndPagination(pageNumber, pageSize, name);
        }

    }
}
