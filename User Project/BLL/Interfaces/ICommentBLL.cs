using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICommentBLL
    {
        CommentModel GetDataById(int id);
        List<CommentModel> GetAll();
        bool Create(CommentModel model);
        bool Update(CommentModel model);
        bool Delete(int id);
    }
}
