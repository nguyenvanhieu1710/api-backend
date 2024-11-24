using DAL;
using DAL.Helper.Interfaces;
using Gateway_Project.Database.Interfaces;
using Model;

namespace Gateway_Project.Database
{
    public class Account : IAccount
    {
        private IDatabaseHelper _IDatabaseHelper;
        public Account(IDatabaseHelper databaseHelper)
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
