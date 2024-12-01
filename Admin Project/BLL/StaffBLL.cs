using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class StaffBLL : IStaffBLL
    {
        private IStaffDAL _IStaffDAL;
        private IAccountBLL _IAccountBLL;
        private IUsersDAL _IUsersDAL;
        private string Secret;
        public StaffBLL(IStaffDAL IStaffDAL, IUsersDAL usersDAL, IAccountBLL accountBLL, IConfiguration configuration)
        {
            _IStaffDAL = IStaffDAL;
            _IAccountBLL = accountBLL;
            _IUsersDAL = usersDAL;
            Secret = configuration["AppSettings:Secret"];
        }

        public bool Create(StaffModel staffModel)
        {
            return _IStaffDAL.Create(staffModel);
        }

        public bool Delete(int id)
        {
            return _IStaffDAL.Delete(id);
        }

        public List<StaffModel> GetAll()
        {
            return _IStaffDAL.GetAll();
        }

        public StaffModel GetDataById(int id)
        {
            return _IStaffDAL.GetDataById(id);
        }

        public List<StaffModel> Pagination(int pageNumber, int pageSize)
        {
            return _IStaffDAL.Pagination(pageNumber, pageSize);
        }
        public List<StaffModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            return _IStaffDAL.GetDataDeletedPagination(pageNumber, pageSize);
        }
        public List<StaffModel> Search(string name)
        {
            return _IStaffDAL.Search(name);
        }

        public bool Update(StaffModel staffModel)
        {
            return _IStaffDAL.Update(staffModel);
        }

        public (StaffModel, AccountModel) Authenticate(string accountName, string password)
        {
            var account = _IAccountBLL.GetDataByAccountNameAndPassword(accountName, password);
            if (account == null) return (null, null);

            StaffModel info = GetDataById(account.AccountId) as StaffModel;
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
                    var adminInfo = this.GetDataById(account.AccountId);
                    return adminInfo ?? null;
                case "Staff":
                    var staffInfo = this.GetDataById(account.AccountId);
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

        public List<StaffModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _IStaffDAL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
