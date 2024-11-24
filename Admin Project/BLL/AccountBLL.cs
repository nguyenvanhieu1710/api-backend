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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AccountBLL : IAccountBLL
    {
        private IAccountDAL _IAccountDAL;
        private string Secret;
        public AccountBLL(IAccountDAL accountDAL, IConfiguration configuration)
        {
            _IAccountDAL = accountDAL;
            Secret = configuration["AppSettings:Secret"];
        }
        public AccountModel GetDataByAccountNameAndPassword(string accountName, string password)
        {
            return _IAccountDAL.GetDataByAccountNameAndPassword(accountName, password);
        }

        public AccountModel GetDataById(int id)
        {
            return _IAccountDAL.GetDataById(id);
        }

        public AccountModel Authenticate(string accountName, string password)
        {
            var account = GetDataByAccountNameAndPassword(accountName, password);
            if (account == null) return null;

            account.Token = GenerateJwtToken(account);

            return account;
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
    }
}
