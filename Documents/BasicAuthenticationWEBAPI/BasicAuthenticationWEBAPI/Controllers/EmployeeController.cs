using BasicAuthenticationWEBAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;

namespace BasicAuthenticationWEBAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin")]
        [Route("api/AllMaleEmployees")]
        public HttpResponseMessage GetAllMaleEmployees()
        {
            var identity = (ClaimsIdentity)User.Identity;
            //Getting the ID value
            var ID = identity.Claims
            .FirstOrDefault(c => c.Type == "ID").Value;//Getting the Email value
            var Email = identity.Claims
            .FirstOrDefault(c => c.Type == "Email").Value;
            //Getting the Username value
            var username = identity.Name;
            //Getting the Roles only if you set the roles in the claims
            //var Roles = identity.Claims
            // .Where(c => c.Type == ClaimTypes.Role)
            // .Select(c => c.Value).ToArray();
            var EmpList = new EmployeeBL().GetEmployees().Where(e => e.Gender.ToLower() ==
           "male").ToList();
            return Request.CreateResponse(HttpStatusCode.OK, EmpList);
        }
        [BasicAuthentication]
        [MyAuthorize(Roles = "Superadmin")]
        [Route("api/AllFemaleEmployees")]
        public HttpResponseMessage GetAllFemaleEmployees()
        {
            var EmpList = new EmployeeBL().GetEmployees().Where(e => e.Gender.ToLower() ==
           "female").ToList();
            return Request.CreateResponse(HttpStatusCode.OK, EmpList);
        }
        [BasicAuthentication]
        [MyAuthorize(Roles = "Admin,Superadmin")]
        [Route("api/AllEmployees")]
        public HttpResponseMessage GetAllEmployees()
        {
            var EmpList = new EmployeeBL().GetEmployees();
            return Request.CreateResponse(HttpStatusCode.OK, EmpList);
        }
    }
}
