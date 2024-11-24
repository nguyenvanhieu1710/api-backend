using BLL;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountBLL _IAccountBLL;
        public AccountController(IAccountBLL accountBLL)
        {
            _IAccountBLL = accountBLL;
        }

        [HttpGet("get-data-by-id")]
        public AccountModel GetDataById(int id)
        {
            return _IAccountBLL.GetDataById(id);
        }

        [HttpGet("get-data-by-accountname-and-password")]
        public AccountModel GetDataByAccountNameAndPassword(string accountName, string password)
        {
            return _IAccountBLL.GetDataByAccountNameAndPassword(accountName, password);
        }

        [HttpPost("login-admin")]
        public AccountModel LoginAdmin(LoginRequest loginRequest)
        {
            return _IAccountBLL.Authenticate(loginRequest.AccountName, loginRequest.Password);
        }
    }
}
