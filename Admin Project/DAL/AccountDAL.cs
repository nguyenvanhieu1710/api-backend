using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AccountDAL : IAccountDAL
    {
        private IDatabaseHelper _IDatabaseHelper;
        public AccountDAL(IDatabaseHelper databaseHelper)
        {
            _IDatabaseHelper = databaseHelper;
        }
        public AccountModel GetDataById(int id)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_account_get_data_by_id",
                    "@account_Id", id);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<AccountModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AccountModel GetDataByAccountNameAndPassword(string accountName, string password)
        {
            {
                string msgError = "";
                try
                {
                    var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_account_get_data_by_accountName_and_password",
                        "@account_AccountName", accountName,
                        "@account_Password", password);
                    if (!string.IsNullOrEmpty(msgError))
                    {
                        throw new Exception(msgError);
                    }
                    return result.ConvertTo<AccountModel>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
