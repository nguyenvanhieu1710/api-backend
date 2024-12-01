using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IImportBillBLL
    {
        ImportBillModel GetDataById(int id);
        List<ImportBillModel> GetAll();
        bool Create(ImportBillModel importBillModel);
        bool Update(ImportBillModel importBillModel);
        bool Delete(int id);
        List<ImportBillModel> Search(string name);
        List<ImportBillModel> Pagination(int pageNumber, int pageSize);
        List<ImportBillModel> GetDataDeletedPagination(int pageNumber, int pageSize);
        List<ImportBillModel> SearchAndPagination(string name, int pageNumber, int pageSize);
    }
}
