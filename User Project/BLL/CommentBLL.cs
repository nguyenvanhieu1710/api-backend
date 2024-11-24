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
    public class CommentBLL : ICommentBLL
    {
        private ICommentDAL _ICommentDAL;
        private string Secret;
        public CommentBLL(ICommentDAL ICommentDAL)
        {
            _ICommentDAL = ICommentDAL;
        }
        public bool Create(CommentModel commentModel)
        {
            return _ICommentDAL.Create(commentModel);
        }

        public bool Delete(int id)
        {
            return _ICommentDAL.Delete(id);
        }

        public List<CommentModel> GetAll()
        {
            return _ICommentDAL.GetAll();
        }

        public CommentModel GetDataById(int id)
        {
            return _ICommentDAL.GetDataById(id);
        }

        public bool Update(CommentModel commentModel)
        {
            return _ICommentDAL.Update(commentModel);
        }
    }
}
