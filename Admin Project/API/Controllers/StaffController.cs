using BLL;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private IStaffBLL _IStaffBLL;
        private string _path;
        public StaffController(IStaffBLL iStaffBLL, IConfiguration configuration)
        {
            _IStaffBLL = iStaffBLL;
            _path = configuration["AppSettings:PATH"];
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var (staff, account) = _IStaffBLL.Authenticate(request.AccountName, request.Password);
                if (staff == null || account == null)
                {
                    return BadRequest(new { message = "Account or password is incorrect" });
                }
                return Ok(new
                {
                    staffId = staff.StaffId,
                    staffName = staff.StaffName,
                    token = account.Token
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("upload-image")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $"staff/{file.FileName}";
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

        [Authorize(Roles = "Admin")]
        [Route("create")]
        [HttpPost]
        public bool Create(StaffModel staffModel)
        {
            return _IStaffBLL.Create(staffModel);
        }

        [Authorize(Roles = "Admin")]
        [Route("update")]
        [HttpPost]
        public bool Update(StaffModel staffModel)
        {
            return _IStaffBLL.Update(staffModel);
        }

        [Authorize(Roles = "Admin")]
        [Route("delete/{id}")]
        [HttpPost]
        public bool Delete(int id)
        {
            return _IStaffBLL.Delete(id);
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("get-data-by-id/{id}")]
        [HttpGet]
        public StaffModel GetDataById(int id)
        {
            return _IStaffBLL.GetDataById(id);
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("get-all")]
        [HttpGet]
        public List<StaffModel> GetAll()
        {
            return _IStaffBLL.GetAll();
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("search/{name}")]
        [HttpGet]
        public List<StaffModel> Search(string name)
        {
            return _IStaffBLL.Search(name);
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("page={pageNumber}&pageSize={pageSize}")]
        [HttpGet]
        public List<StaffModel> Pagination(int pageNumber, int pageSize)
        {
            List<StaffModel> staffs = _IStaffBLL.Pagination(pageNumber, pageSize);
            foreach (var item in staffs)
            {
                if (!string.IsNullOrEmpty(item.Image))
                {
                    var filePath = Path.Combine("D:/Documents Of Year 3/Service-oriented Software Development/Admin Project/Image/staff", item.Image);

                    item.Image = Utils.ImageFile.ConvertImageToBase64(filePath);
                }
            }
            return staffs;
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("search-and-pagination")]
        [HttpGet]
        public List<StaffModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _IStaffBLL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
