using BLL;
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
        private string _path;
        public AdvertisementController(IAdvertisementBLL InterfaceAdvertisementBLL, IConfiguration configuration)
        {
            _interfaceAdvertisementBLL = InterfaceAdvertisementBLL;
            _path = configuration["AppSettings:PATH"];
        }

        [Route("create")]
        [HttpPost]
        public bool Create(AdvertisementModel advertisementModel)
        {
            return _interfaceAdvertisementBLL.Create(advertisementModel);
        }

        [Route("upload-image")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $@"advertisement/{file.FileName}";
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
            List<AdvertisementModel> advertisements = _interfaceAdvertisementBLL.Pagination(pageNumber, pageSize);
            foreach (var item in advertisements)
            {
                if (!string.IsNullOrEmpty(item.AdvertisementImage))
                {
                    var filePath = Path.Combine("D:/Documents Of Year 3/Service-oriented Software Development/Admin Project/Image/advertisement", item.AdvertisementImage);

                    item.AdvertisementImage = Utils.ImageFile.ConvertImageToBase64(filePath);
                }
            }
            return advertisements;
        }

        [Route("get-data-deleted-pagination")]
        [HttpGet]
        public List<AdvertisementModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            return _interfaceAdvertisementBLL.GetDataDeletedPagination(pageNumber, pageSize);
        }

        [Route("search-and-pagination")]
        [HttpGet]
        public List<AdvertisementModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _interfaceAdvertisementBLL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
