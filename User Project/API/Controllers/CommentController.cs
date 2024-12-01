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
        public bool Create(CommentModel commentModel)
        {
            return _ICommentBLL.Create(commentModel);
        }

        [Route("update")]
        [HttpPost]
        public bool Update(CommentModel commentModel)
        {
            return _ICommentBLL.Update(commentModel);            
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
