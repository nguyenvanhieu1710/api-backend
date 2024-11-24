using BLL.Interfaces;
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
    }
}
