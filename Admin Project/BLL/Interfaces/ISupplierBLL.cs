using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISupplierBLL
    {
        SupplierModel GetDataById(int id);
        List<SupplierModel> GetAll();
        bool Create(SupplierModel supplierModel);
        bool Update(SupplierModel supplierModel);
        bool Delete(int id);
        List<SupplierModel> Search(string name);
        List<SupplierModel> Pagination(int pageNumber, int pageSize);
        List<SupplierModel> SearchAndPagination(int pageNumber, int pageSize, string name);
    }
}
