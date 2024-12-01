using BLL;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private INewsBLL _interfaceNewsBLL;
        private string _path;
        public NewsController(INewsBLL InterfaceNewsBLL, IConfiguration configuration)
        {
            _interfaceNewsBLL = InterfaceNewsBLL;
            _path = configuration["AppSettings:PATH"];
        }

        [Route("create")]
        [HttpPost]
        public bool Create([FromBody] NewsModel newsModel)
        {
            return _interfaceNewsBLL.Create(newsModel);
        }

        [Route("upload-image")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $@"news/{file.FileName}";
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
        public bool Update(NewsModel newsModel)
        {
            return _interfaceNewsBLL.Update(newsModel);
        }

        [Route("delete/{id}")]
        [HttpPost]
        public bool Delete(int id)
        {
            return _interfaceNewsBLL.Delete(id);
        }

        [Route("get-data-by-id/{id}")]
        [HttpGet]
        public NewsModel GetDataById(int id)
        {
            return _interfaceNewsBLL.GetDataById(id);
        }

        [Route("get-all")]
        [HttpGet]
        public List<NewsModel> GetAll()
        {
            return _interfaceNewsBLL.GetAll();
        }

        [Route("search/{name}")]
        [HttpGet]
        public List<NewsModel> Search(string name)
        {
            return _interfaceNewsBLL.Search(name);
        }

        [Route("page={pageNumber}&pageSize={pageSize}")]
        [HttpGet]
        public List<NewsModel> Pagination(int pageNumber, int pageSize)
        {
            List<NewsModel> news = _interfaceNewsBLL.Pagination(pageNumber, pageSize);
            foreach (var item in news)
            {
                if (!string.IsNullOrEmpty(item.NewsImage))
                {
                    var filePath = Path.Combine("D:/Documents Of Year 3/Service-oriented Software Development/Admin Project/Image/news", item.NewsImage);

                    item.NewsImage = Utils.ImageFile.ConvertImageToBase64(filePath);
                }
            }
            return news;
        }

        [Route("get-data-deleted-pagination")]
        [HttpGet]
        public List<NewsModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            return _interfaceNewsBLL.GetDataDeletedPagination(pageNumber, pageSize);
        }

        [Route("search-and-pagination")]
        [HttpGet]
        public List<NewsModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _interfaceNewsBLL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
