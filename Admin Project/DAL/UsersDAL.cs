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
    public class UsersDAL : IUsersDAL
    {
        private IDatabaseHelper _IDatabaseHelper;
        public UsersDAL(IDatabaseHelper IDatabaseHelper)
        {
            _IDatabaseHelper = IDatabaseHelper;
        }
        public List<UsersModel> GetAll()
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_users_all");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<UsersModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UsersModel GetDataById(int id)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_users_get_data_by_id",
                    "@users_Id", id);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<UsersModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create(UsersModel usersModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_users_create",
                    "@users_UserName", usersModel.UserName,
                    "@users_Birthday", usersModel.Birthday,
                    "@users_PhoneNumber", usersModel.PhoneNumber,
                    "@users_Image", usersModel.Image,
                    "@users_Gender", usersModel.Gender,
                    "@users_Address", usersModel.Address,
                    "@users_Ranking", usersModel.Ranking);
                if (result != null && !string.IsNullOrEmpty(result))
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
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_users_delete",
                    "@users_Id", id);
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

        public bool Update(UsersModel usersModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_users_update",
                    "@users_Id", usersModel.UserId,
                    "@users_UserName", usersModel.UserName,
                    "@users_Birthday", usersModel.Birthday,
                    "@users_PhoneNumber", usersModel.PhoneNumber,
                    "@users_Image", usersModel.Image,
                    "@users_Gender", usersModel.Gender,
                    "@users_Address", usersModel.Address,
                    "@users_Ranking", usersModel.Ranking,
                    "@users_Deleted", usersModel.Deleted);
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

        public List<UsersModel> Search(string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_users_search",
                    "@users_Name", name);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<UsersModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UsersModel> Pagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_users_pagination",
                    "@users_pageNumber", pageNumber,
                    "@users_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<UsersModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UsersModel> GetDataDeletedPagination(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_users_deleted_pagination",
                    "@users_pageNumber", pageNumber,
                    "@users_pageSize", pageSize);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<UsersModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UsersModel GetDataByUserNameAndPassword(string userName, string password)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_users_get_data_by_username_and_password",
                    "@account_UserName", userName,
                    "@account_Password", password);
                return result.ConvertTo<UsersModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UsersModel> SearchAndPagination(int pageNumber, int pageSize, string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_users_search_pagination",
                    "@users_pageNumber", pageNumber,
                    "@users_pageSize", pageSize,
                    "@users_Name", name);
                if (result != null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(result.ToString());
                }
                return result.ConvertTo<UsersModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
