using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdvertisementDAL : IAdvertisementDAL
    {
        private IDatabaseHelper _IDatabaseHelper;
        public AdvertisementDAL(IDatabaseHelper dbhelper)
        {
            _IDatabaseHelper = dbhelper;
        }

        public List<AdvertisementModel> GetAll()
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_advertisement_all");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<AdvertisementModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AdvertisementModel GetDataById(int id)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_advertisement_get_data_by_id",
                    "@advertisement_Id", id);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<AdvertisementModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
