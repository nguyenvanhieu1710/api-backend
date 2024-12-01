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
    public class AdvertisementDAL: IAdvertisementDAL
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

        public bool Create(AdvertisementModel advertisementModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_advertisement_create",
                    "@advertisement_Name", advertisementModel.AdvertisementName,
                    "@advertisement_AdvertisementImage", advertisementModel.AdvertisementImage,
                    "@advertisement_Location", advertisementModel.Location,
                    "@advertisement_AdvertiserId", advertisementModel.AdvertiserId);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(Convert.ToString(result));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_advertisement_delete",
                    "@advertisement_Id", id);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(Convert.ToString(result));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(AdvertisementModel advertisementModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_advertisement_update",
                    "@advertisement_Id", advertisementModel.AdvertisementId,
                    "@advertisement_Name", advertisementModel.AdvertisementName,
                    "@advertisement_AdvertisementImage", advertisementModel.AdvertisementImage,
                    "@advertisement_Location", advertisementModel.Location,
                    "@advertisement_AdvertiserId", advertisementModel.AdvertiserId);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(Convert.ToString(result));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AdvertisementModel> Search(string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_advertisement_search",
                    "@advertisement_Name", name);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<AdvertisementModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AdvertisementModel> Pagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_advertisement_pagination",
                    "@advertisement_pageNumber", pageNumber,
                    "@advertisement_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<AdvertisementModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AdvertisementModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_advertisement_deleted_pagination",
                    "@advertisement_pageNumber", pageNumber,
                    "@advertisement_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<AdvertisementModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AdvertisementModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_advertisement_search_pagination",
                    "@advertisement_pageNumber", pageNumber,
                    "@advertisement_pageSize", pageSize,
                    "@advertisement_Name", name);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<AdvertisementModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
