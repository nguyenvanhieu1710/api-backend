using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAdvertisementBLL
    {
        AdvertisementModel GetDataById(int id);
        List<AdvertisementModel> GetAll();
        bool Create(AdvertisementModel advertisementModel);
        bool Update(AdvertisementModel advertisementModel);
        bool Delete(int id);
        List<AdvertisementModel> Search(string name);
        List<AdvertisementModel> Pagination(int pageNumber, int pageSize);
        List<AdvertisementModel> SearchAndPagination(int pageNumber, int pageSize, string name);
    }
}
