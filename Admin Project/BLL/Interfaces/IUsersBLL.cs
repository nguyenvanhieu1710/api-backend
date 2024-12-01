using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUsersBLL
    {
        UsersModel GetDataById(int id);
        UsersModel GetDataByUserNameAndPassword(string userName, string password);
        List<UsersModel> GetAll();
        bool Create(UsersModel usersModel);
        bool Update(UsersModel usersModel);
        bool Delete(int id);
        List<UsersModel> Search(string name);
        List<UsersModel> Pagination(int pageNumber, int pageSize);
        List<UsersModel> GetDataDeletedPagination(int pageNumber, int pageSize);
        
        List<UsersModel> SearchAndPagination(int pageNumber, int pageSize, string name);
        (UsersModel, AccountModel) Authenticate(string username, string password);
    }
}
