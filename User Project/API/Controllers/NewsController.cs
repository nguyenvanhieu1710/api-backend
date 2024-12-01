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
        public NewsController(INewsBLL InterfaceNewsBLL)
        {
            _interfaceNewsBLL = InterfaceNewsBLL;
        }

        [Route("create")]
        [HttpPost]
        public bool Create([FromBody] NewsModel newsModel)
        {
            return _interfaceNewsBLL.Create(newsModel);
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
            List<NewsModel> news = _interfaceNewsBLL.GetAll();
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
            return _interfaceNewsBLL.Pagination(pageNumber, pageSize);
        }

        [Route("search-and-pagination")]
        [HttpGet]
        public List<NewsModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _interfaceNewsBLL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
