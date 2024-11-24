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
    public class MessageBLL : IMessageBLL
    {
        private IMessageDAL _IMessageDAL;
        public MessageBLL(IMessageDAL messageDAL)
        {
            _IMessageDAL = messageDAL;
        }
        public bool Create(MessageModel messageModel)
        {
            return _IMessageDAL.Create(messageModel);
        }

        public bool Delete(int id)
        {
            return _IMessageDAL.Delete(id);
        }

        public List<MessageModel> GetAll()
        {
            return _IMessageDAL.GetAll();
        }

        public List<MessageModel> Search(string content)
        {
            return _IMessageDAL.Search(content);
        }
    }
}
