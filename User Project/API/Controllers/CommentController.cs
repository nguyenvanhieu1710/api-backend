using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentBLL _ICommentBLL;
        public CommentController(ICommentBLL iCommentBLL)
        {
            _ICommentBLL = iCommentBLL;
        }

        [Route("create")]
        [HttpPost]
        public CommentModel Create(CommentModel commentModel)
        {
            _ICommentBLL.Create(commentModel);
            return commentModel;
        }

        [Route("update")]
        [HttpPost]
        public CommentModel Update(CommentModel commentModel)
        {
            _ICommentBLL.Update(commentModel);
            return commentModel;
        }

        [Route("delete/{id}")]
        [HttpPost]
        public bool Delete(int id)
        {
            return _ICommentBLL.Delete(id);
        }

        [Route("get-data-by-id/{id}")]
        [HttpGet]
        public CommentModel GetDataById(int id)
        {
            return _ICommentBLL.GetDataById(id);
        }

        [Route("get-all")]
        [HttpGet]
        public List<CommentModel> GetAll()
        {
            return _ICommentBLL.GetAll();
        }
    }
}
