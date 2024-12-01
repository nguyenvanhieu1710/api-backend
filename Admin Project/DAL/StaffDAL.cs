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
    public class StaffDAL : IStaffDAL
    {
        private IDatabaseHelper _IDatabaseHelper;
        public StaffDAL(IDatabaseHelper IDatabaseHelper)
        {
            _IDatabaseHelper = IDatabaseHelper;
        }
        public List<StaffModel> GetAll()
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_staff_all");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<StaffModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public StaffModel GetDataById(int id)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_staff_get_data_by_id",
                    "@staff_Id", id);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<StaffModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create(StaffModel staffModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_staff_create",
                    "@staff_StaffName", staffModel.StaffName,
                    "@staff_Birthday", staffModel.Birthday,
                    "@staff_PhoneNumber", staffModel.PhoneNumber,
                    "@staff_Image", staffModel.Image,
                    "@staff_Gender", staffModel.Gender,
                    "@staff_Address", staffModel.Address,
                    "@staff_Position", staffModel.Position);
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
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_staff_delete",
                    "@staff_Id", id);
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

        public bool Update(StaffModel staffModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_staff_update",
                    "@staff_Id", staffModel.StaffId,
                    "@staff_StaffName", staffModel.StaffName,
                    "@staff_Birthday", staffModel.Birthday,
                    "@staff_PhoneNumber", staffModel.PhoneNumber,
                    "@staff_Image", staffModel.Image,
                    "@staff_Gender", staffModel.Gender,
                    "@staff_Address", staffModel.Address,
                    "@staff_Position", staffModel.Position);
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

        public List<StaffModel> Search(string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_staff_search",
                    "@staff_Name", name);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<StaffModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StaffModel> Pagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_staff_pagination",
                    "@staff_pageNumber", pageNumber,
                    "@staff_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<StaffModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StaffModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_staff_deleted_pagination",
                    "@staff_pageNumber", pageNumber,
                    "@staff_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<StaffModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StaffModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_staff_search_pagination",
                    "@staff_pageNumber", pageNumber,
                    "@staff_pageSize", pageSize,
                    "@staff_Name", name);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<StaffModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
