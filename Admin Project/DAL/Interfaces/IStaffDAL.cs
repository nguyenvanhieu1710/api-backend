using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IStaffDAL
    {
        StaffModel GetDataById(int id);
        List<StaffModel> GetAll();
        bool Create(StaffModel staffModel);
        bool Update(StaffModel staffModel);
        bool Delete(int id);
        List<StaffModel> Search(string name);
        List<StaffModel> Pagination(int pageNumber, int pageSize);
        List<StaffModel> GetDataDeletedPagination(int pageNumber, int pageSize);
        List<StaffModel> SearchAndPagination(int pageNumber, int pageSize, string name);
    }
}
