using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IStaffBLL
    {
        StaffModel GetDataById(int id);
        List<StaffModel> GetAll();
        bool Create(StaffModel staffModel);
        bool Update(StaffModel staffModel);
        bool Delete(int id);
        List<StaffModel> Search(string name);
        List<StaffModel> Pagination(int pageNumber, int pageSize);
        (StaffModel, AccountModel) Authenticate(string username, string password);
    }
}
