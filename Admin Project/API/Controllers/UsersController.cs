using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Reflection;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersBLL _IUsersBLL;
        private string _path;
        public UsersController(IUsersBLL iUsersBLL, IConfiguration configuration)
        {
            _IUsersBLL = iUsersBLL;
            _path = configuration["AppSettings:PATH"];
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var (user, account) = _IUsersBLL.Authenticate(request.AccountName, request.Password);
                if (user == null || account == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
                return Ok(new
                {
                    userId = user.UserId,
                    userName = user.UserName,
                    birthday = user.Birthday,
                    phoneNumber = user.PhoneNumber,
                    gender = user.Gender,
                    adrress = user.Address,
                    token = account.Token
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Roles = "Admin, Staff, User")]
        [Route("upload-image")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $"user/{file.FileName}";
                    var fullPath = CreatePathFile(filePath);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    //return Ok(new { filePath });
                    return Ok(new { fullPath });
                }
                else
                {
                    return BadRequest();
                }
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

        [Authorize(Roles = "Admin, Staff, User")]
        [HttpGet("{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var filePath = Path.Combine("D:/Documents Of Year 3/Service-oriented Software Development/Admin Project/Image/user", fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("Image not found");

            var image = System.IO.File.ReadAllBytes(filePath);
            var contentType = Utils.ImageFile.GetContentType(fileName);
            if (string.IsNullOrEmpty(contentType))
                return BadRequest("Unsupported file type");

            return File(image, contentType);
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("create")]
        [HttpPost]
        public bool Create(UsersModel usersModel)
        {
            return _IUsersBLL.Create(usersModel);
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("update")]
        [HttpPost]
        public bool Update(UsersModel usersModel)
        {
            return _IUsersBLL.Update(usersModel);
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("delete/{id}")]
        [HttpPost]
        public bool Delete(int id)
        {
            return _IUsersBLL.Delete(id);
        }

        [Authorize(Roles = "Admin, Staff, User")]
        [Route("get-data-by-id/{id}")]
        [HttpGet]
        public UsersModel GetDataById(int id)
        {
            return _IUsersBLL.GetDataById(id);
        }

        [Authorize(Roles = "Admin, Staff, User")]
        [Route("get-all")]
        [HttpGet]
        public List<UsersModel> GetAll()
        {
            return _IUsersBLL.GetAll();
        }

        [Authorize(Roles = "Admin, Staff, User")]
        [Route("get-data-by-username-and-password")]
        [HttpGet]
        public UsersModel GetDataByUserNameAndPassword(string userName, string password)
        {
            return _IUsersBLL.GetDataByUserNameAndPassword(userName, password);
        }

        [Authorize(Roles = "Admin, Staff, User")]
        [Route("search/{name}")]
        [HttpGet]
        public List<UsersModel> Search(string name)
        {
            return _IUsersBLL.Search(name);
        }

        [Authorize(Roles = "Admin, Staff, User")]
        [Route("page={pageNumber}&pageSize={pageSize}")]
        [HttpGet]
        public List<UsersModel> Pagination(int pageNumber, int pageSize)
        {
            List<UsersModel> users = _IUsersBLL.Pagination(pageNumber, pageSize);
            foreach (var user in users)
            {
                if (!string.IsNullOrEmpty(user.Image))
                {
                    var filePath = Path.Combine("D:/Documents Of Year 3/Service-oriented Software Development/Admin Project/Image/user", user.Image);

                    user.Image = Utils.ImageFile.ConvertImageToBase64(filePath);
                }
            }
            return users;
        }

        [Route("get-data-deleted-pagination")]
        [HttpGet]
        public List<UsersModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            return _IUsersBLL.GetDataDeletedPagination(pageNumber, pageSize);
        }

        [Authorize(Roles = "Admin, Staff, User")]
        [Route("search-and-pagination")]
        [HttpGet]
        public List<UsersModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _IUsersBLL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
