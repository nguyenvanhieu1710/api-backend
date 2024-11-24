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
    public class CartBLL : ICartBLL
    {
        private ICartDAL _ICartDAL;
        public CartBLL(ICartDAL icartDAL)
        {
            _ICartDAL = icartDAL;
        }

        public bool Create(CartModel cartModel)
        {
            return _ICartDAL.Create(cartModel);
        }
        public bool Delete(CartModel cartModel)
        {
            return _ICartDAL.Delete(cartModel);
        }
        public List<CartModel> SearchForProductsInTheShoppingCart(string name)
        {
            return _ICartDAL.SearchForProductsInTheShoppingCart(name);
        }
    }
}
