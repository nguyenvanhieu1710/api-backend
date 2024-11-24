using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMessageBLL
    {
        public bool Create(MessageModel messageModel);
        public bool Delete(int id);
        public List<MessageModel> GetAll();
        public List<MessageModel> Search(string content);
    }
}
