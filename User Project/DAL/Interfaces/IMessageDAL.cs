using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IMessageDAL
    {
        List<MessageModel> GetAll();
        bool Create(MessageModel message);
        bool Delete(int id);
        List<MessageModel> Search(string content);
    }
}
