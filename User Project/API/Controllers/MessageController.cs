using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageBLL _interfaceMessageBLL;
        public MessageController(IMessageBLL InterfaceMessageBLL)
        {
            _interfaceMessageBLL = InterfaceMessageBLL;
        }

        [Route("create")]
        [HttpPost]
        public bool Create([FromBody] MessageModel messageModel)
        {
            return _interfaceMessageBLL.Create(messageModel);
        }
        
        [Route("delete/{id}")]
        [HttpPost]
        public bool Delete(int id)
        {
            return _interfaceMessageBLL.Delete(id);             
        }
       
        [Route("get-all")]
        [HttpGet]
        public List<MessageModel> GetAll()
        {
            return _interfaceMessageBLL.GetAll();
        }

        [Route("search/{name}")]
        [HttpGet]
        public List<MessageModel> Search(string name)
        {
            return _interfaceMessageBLL.Search(name);
        }        
    }
}
