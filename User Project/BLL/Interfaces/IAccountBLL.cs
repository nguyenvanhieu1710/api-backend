using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccountBLL
    {
        AccountModel GetDataById(int id);
        AccountModel GetDataByAccountNameAndPassword(string accountName, string password);
    }
}
