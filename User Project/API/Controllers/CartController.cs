using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartBLL _ICartBLL;
        public CartController(ICartBLL iCartBLL)
        {
            _ICartBLL = iCartBLL;
        }

        [Route("create")]
        [HttpPost]
        public bool Create(CartModel cartModel)
        {
            return _ICartBLL.Create(cartModel);
        }

        [Route("adjust-quantity")]
        [HttpGet]
        public bool AdjustQuantity(CartModel cartModel)
        {
            return true;
        }

        [Route("calculate-total-amount")]
        [HttpGet]
        public bool CalculateTotalAmount(CartModel cartModel)
        {
            return true;
        }

        [Route("delete")]
        [HttpPost]
        public bool Delete(CartModel cartModel)
        {
            return _ICartBLL.Delete(cartModel);
        }

        [Route("search-product")]
        [HttpGet]
        public List<CartModel> SearchForProductsInTheShoppingCart(string name)
        {
            return _ICartBLL.SearchForProductsInTheShoppingCart(name);
        }
    }
}
