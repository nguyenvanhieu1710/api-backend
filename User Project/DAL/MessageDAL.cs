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
    public class MessageDAL : IMessageDAL
    {
        public IDatabaseHelper _IDatabaseHelper;
        public MessageDAL(IDatabaseHelper IDatabaseHelper)
        {
            _IDatabaseHelper = IDatabaseHelper;
        }
        public bool Create(MessageModel message)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_message_create",
                    "@message_Content", message.Content,
                    "@message_Time", message.Time,
                    "@message_SenderId", message.SenderId,
                    "@message_ReceiverId", message.ReceiverId);
                if (!string.IsNullOrEmpty(result))
                {
                    throw new Exception(result);
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
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_message_delete",
                    "@message_Id", id);
                if (!string.IsNullOrEmpty(result))
                {
                    throw new Exception(result);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MessageModel> GetAll()
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_message_all");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<MessageModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MessageModel> Search(string content)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_message_search",
                    "@message_Content", content);
                if (!string.IsNullOrEmpty(msgError))
                {

                    throw new Exception(msgError);
                }
                return result.ConvertTo<MessageModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
