using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImportBillController : ControllerBase
    {
        private IImportBillBLL _IImportBillBLL;
        public ImportBillController(IImportBillBLL iimportBillBLL)
        {
            _IImportBillBLL = iimportBillBLL;
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("create")]
        [HttpPost]
        public bool Create(ImportBillModel importBillModel)
        {
            return _IImportBillBLL.Create(importBillModel);
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("update")]
        [HttpPost]
        public bool Update(ImportBillModel importBillModel)
        {
            return _IImportBillBLL.Update(importBillModel);
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("delete/{id}")]
        [HttpPost]
        public bool Delete(int id)
        {
            return _IImportBillBLL.Delete(id);
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("get-data-by-id/{id}")]
        [HttpGet]
        public ImportBillModel GetDataById(int id)
        {
            return _IImportBillBLL.GetDataById(id);
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("get-all")]
        [HttpGet]
        public List<ImportBillModel> GetAll()
        {
            return _IImportBillBLL.GetAll();
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("search/{name}")]
        [HttpGet]
        public List<ImportBillModel> Search(string name)
        {
            return _IImportBillBLL.Search(name);
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("page={pageNumber}&pageSize={pageSize}")]
        [HttpGet]
        public List<ImportBillModel> Pagination(int pageNumber, int pageSize)
        {
            return _IImportBillBLL.Pagination(pageNumber, pageSize);
        }

        [Authorize(Roles = "Admin, Staff")]
        [Route("search-and-pagination")]
        [HttpGet]
        public List<ImportBillModel> SearchAndPagination(string name, int pageNumber, int pageSize)
        {
            return _IImportBillBLL.SearchAndPagination(name, pageNumber, pageSize);
        }
    }
}
