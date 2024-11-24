using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAccountDAL
    {
        AccountModel GetDataById(int id);
        AccountModel GetDataByAccountNameAndPassword(string accountName, string password);
    }
}
