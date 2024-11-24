using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UsersBLL : IUsersBLL
    {
        private IUsersDAL _IUsersDAL;
        private IAccountBLL _IAccountBLL;
        private IStaffBLL _IStaffBLL;
        private string Secret;
        public UsersBLL(IUsersDAL IUsersDAL, IStaffBLL staffBLL, IAccountBLL accountBLL, IConfiguration configuration)
        {
            _IUsersDAL = IUsersDAL;
            _IStaffBLL = staffBLL;
            _IAccountBLL = accountBLL;
            Secret = configuration["AppSettings:Secret"];
        }
        public bool Create(UsersModel usersModel)
        {
            return _IUsersDAL.Create(usersModel);
        }

        public bool Delete(int id)
        {
            return _IUsersDAL.Delete(id);
        }

        public List<UsersModel> GetAll()
        {
            return _IUsersDAL.GetAll();
        }

        public UsersModel GetDataById(int id)
        {
            return _IUsersDAL.GetDataById(id);
        }

        public UsersModel GetDataByUserNameAndPassword(string userName, string password)
        {
            return _IUsersDAL.GetDataByUserNameAndPassword(userName, password);
        }

        public List<UsersModel> Pagination(int pageNumber, int pageSize)
        {
            return _IUsersDAL.Pagination(pageNumber, pageSize);
        }

        public List<UsersModel> Search(string name)
        {
            return _IUsersDAL.Search(name);
        }

        public bool Update(UsersModel usersModel)
        {
            return _IUsersDAL.Update(usersModel);
        }
        public (UsersModel, AccountModel) Authenticate(string accountName, string password)
        {
            var account = _IAccountBLL.GetDataByAccountNameAndPassword(accountName, password);
            if (account == null) return (null, null);

            UsersModel info = GetAccountInformation(account) as UsersModel;
            if (info == null) return (null, account);

            account.Token = GenerateJwtToken(account);

            return (info, account);
        }
        private object GetAccountInformation(AccountModel account)
        {
            switch (account.Role)
            {
                case "Admin":
                    // like staff
                    var adminInfo = _IStaffBLL.GetDataById(account.AccountId);
                    return adminInfo ?? null;
                case "Staff":
                    var staffInfo = _IStaffBLL.GetDataById(account.AccountId);
                    return staffInfo ?? null;
                case "User":
                    var userInfo = _IUsersDAL.GetDataById(account.AccountId);
                    return userInfo ?? null;
                default:
                    return null;
            }
        }

        private string GenerateJwtToken(AccountModel account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, account.AccountName),
                new Claim(ClaimTypes.Role, account.Role)
            }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public List<UsersModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _IUsersDAL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
