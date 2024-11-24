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
    public class CommentDAL : ICommentDAL
    {
        private IDatabaseHelper _IDatabaseHelper;
        public CommentDAL(IDatabaseHelper dbhelper)
        {
            _IDatabaseHelper = dbhelper;
        }

        public List<CommentModel> GetAll()
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_comment_all");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<CommentModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CommentModel GetDataById(int id)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_comment_get_data_by_id",
                    "@comment_Id", id);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<CommentModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create(CommentModel commentModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_comment_create",
                    "@comment_Content", commentModel.Content,
                    "@comment_Time", commentModel.Time,
                    "@comment_SenderId", commentModel.SenderId,
                    "@comment_ProductId", commentModel.ProductId);
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

        public bool Delete(int id)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_comment_delete",
                    "@comment_Id", id);
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

        public bool Update(CommentModel commentModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_comment_update",
                    "@comment_Id", commentModel.CommentId,
                    "@comment_Content", commentModel.Content,
                    "@comment_Time", commentModel.Time,
                    "@comment_SenderId", commentModel.SenderId,
                    "@comment_ProductId", commentModel.ProductId);
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
    }
}
