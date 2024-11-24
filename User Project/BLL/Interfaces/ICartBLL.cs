using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICartBLL
    {
        bool Create(CartModel cartModel);
        bool Delete(CartModel cartModel);
        List<CartModel> SearchForProductsInTheShoppingCart(string name);
    }
}
