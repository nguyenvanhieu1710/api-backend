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
    public class AdvertisementBLL : IAdvertisementBLL
    {
        private IAdvertisementDAL _IAdvertisementDAL;
        public AdvertisementBLL(IAdvertisementDAL InterfaceAdvertisementDAL)
        {
            _IAdvertisementDAL = InterfaceAdvertisementDAL;
        }

        public AdvertisementModel GetDataById(int id)
        {
            return _IAdvertisementDAL.GetDataById(id);
        }

        public List<AdvertisementModel> GetAll()
        {
            return _IAdvertisementDAL.GetAll();
        }

        public bool Create(AdvertisementModel advertisementModel)
        {
            return _IAdvertisementDAL.Create(advertisementModel);
        }

        public bool Update(AdvertisementModel advertisementModel)
        {
            return _IAdvertisementDAL.Update(advertisementModel);
        }
        public bool Delete(int id)
        {
            return _IAdvertisementDAL.Delete(id);
        }

        public List<AdvertisementModel> Search(string name)
        {
            return _IAdvertisementDAL.Search(name);
        }
        public List<AdvertisementModel> Pagination(int pageNumber, int pageSize)
        {
            return _IAdvertisementDAL.Pagination(pageNumber, pageSize);
        }
        public List<AdvertisementModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            return _IAdvertisementDAL.GetDataDeletedPagination(pageNumber, pageSize);
        }
        public List<AdvertisementModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            return _IAdvertisementDAL.SearchAndPagination(pageNumber, pageSize, name);
        }
    }
}
