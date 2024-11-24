using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUsersDAL
    {
        bool Register(RegisterRequest request);
        UsersModel GetDataById(int id);
        UsersModel GetDataByUserNameAndPassword(string userName, string password);
        List<UsersModel> GetAll();
        List<UsersModel> Search(string name);
        List<UsersModel> Pagination(int pageNumber, int pageSize);
    }
}
