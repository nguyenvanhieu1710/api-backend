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
    public class AccountBLL : IAccountBLL
    {
        private IAccountDAL _IAccountDAL;
        public AccountBLL(IAccountDAL accountDAL)
        {
            _IAccountDAL = accountDAL;
        }
        public AccountModel GetDataByAccountNameAndPassword(string accountName, string password)
        {
            return _IAccountDAL.GetDataByAccountNameAndPassword(accountName, password);
        }

        public AccountModel GetDataById(int id)
        {
            return _IAccountDAL.GetDataById(id);
        }
    }
}
