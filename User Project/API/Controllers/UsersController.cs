using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
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
        private IAccountBLL _IAccountBLL;
        private string _path;
        public UsersController(IUsersBLL iUsersBLL, IAccountBLL accountBLL, IConfiguration configuration)
        {
            _IUsersBLL = iUsersBLL;
            _IAccountBLL = accountBLL;
            _path = configuration["AppSettings:PATH"];
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] Model.LoginRequest request)
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

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public IActionResult Register([FromBody] Model.RegisterRequest request)
        {
            try
            {
                var user = _IAccountBLL.GetDataByAccountNameAndPassword(request.AccountName, request.Password);
                if (user != null)
                {
                    return BadRequest(new { message = "Username or password is existed" });
                }
                _IUsersBLL.Register(request);
                return Ok(new
                {
                    message = "Register is successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Roles = "User")]
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
                    return Ok(new { filePath });
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
            return _IUsersBLL.Pagination(pageNumber, pageSize);
        }
    }
}
