using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ImportBillBLL : IImportBillBLL
    {
        private IImportBillDAL _IImportBillDAL;
        public ImportBillBLL(IImportBillDAL InterfaceImportBillDAL)
        {
            _IImportBillDAL = InterfaceImportBillDAL;
        }

        public ImportBillModel GetDataById(int id)
        {
            return _IImportBillDAL.GetDataById(id);
        }

        public List<ImportBillModel> GetAll()
        {
            return _IImportBillDAL.GetAll();
        }

        public bool Create(ImportBillModel importBillModel)
        {
            return _IImportBillDAL.Create(importBillModel);
        }

        public bool Update(ImportBillModel importBillModel)
        {
            return _IImportBillDAL.Update(importBillModel);
        }
        public bool Delete(int id)
        {
            return _IImportBillDAL.Delete(id);
        }

        public List<ImportBillModel> Search(string name)
        {
            return _IImportBillDAL.Search(name);
        }
        public List<ImportBillModel> Pagination(int pageNumber, int pageSize)
        {
            return _IImportBillDAL.Pagination(pageNumber, pageSize);
        }
        public List<ImportBillModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            return _IImportBillDAL.GetDataDeletedPagination(pageNumber, pageSize);
        }
        public List<ImportBillModel> SearchAndPagination(string name, int pageNumber, int pageSize)
        {
            return _IImportBillDAL.SearchAndPagination(name, pageNumber, pageSize);
        }
    }
}
