using Gateway_Project.Database.Interfaces;
using Gateway_Project.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Gateway_Project
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private IAccount _IAccount;
        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings,IConfiguration configuration)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public Task Invoke(HttpContext context, IAccount _IAccount)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Expose-Headers", "*");
            if (!context.Request.Path.Equals("/api/token", StringComparison.Ordinal))
            {
                return _next(context);
            }
            if (context.Request.Method.Equals("POST") && context.Request.HasFormContentType)
            {
                return GenerateToken(context, _IAccount);
            }
            context.Response.StatusCode = 400;
            return context.Response.WriteAsync("Bad request.");
        }

        public async Task GenerateToken(HttpContext context, IAccount _IAccount)
        {
            var accountName = context.Request.Form["accountName"].ToString();
            var password = context.Request.Form["password"].ToString();
            //var account = db.AccountModels.SingleOrDefault(x => x.AccountName == accountName && x.Password == password);
            var account = _IAccount.GetDataByAccountNameAndPassword(accountName, password);
            // return null if account not found
            if (account == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var result = JsonConvert.SerializeObject(new { code = (int)HttpStatusCode.BadRequest, error = "Account Name or Password is incorrect." });
                await context.Response.WriteAsync(result);
                return;
            }
            //var user = db.UsersModels.Where(u => u.UserId == account.AccountId).FirstOrDefault();
            // return null if user not found
            //if (user == null)
            //{
            //    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //    var result = JsonConvert.SerializeObject(new { code = (int)HttpStatusCode.BadRequest, error = "Account Name or Password is incorrect." });
            //    await context.Response.WriteAsync(result);
            //    return;
            //}
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.AccountName),
                    new Claim(ClaimTypes.Role, account.Role),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tmp = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(tmp);
            var response = new { AccountName = account.AccountName, Role = account.Role, Token = token };
            var serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, serializerSettings));
            return;
        }
    }
}
