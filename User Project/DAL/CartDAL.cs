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
    public class CartDAL : ICartDAL
    {
        private IDatabaseHelper _IDatabaseHelper;
        public CartDAL(IDatabaseHelper IDatabaseHelper)
        {
            _IDatabaseHelper = IDatabaseHelper;
        }
        public bool Create(CartModel cartModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_cart_create",
                    "@cart_ProductId", cartModel.ProductId,
                    "@cart_UserId", cartModel.UserId);
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

        public bool Delete(CartModel cartModel)
        {
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedure("sp_cart_delete",
                    "@cart_ProductId", cartModel.ProductId,
                    "@cart_UserId", cartModel.UserId);
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
        public List<CartModel> SearchForProductsInTheShoppingCart(string name)
        {
            string msgError = "";
            try
            {
                var result = _IDatabaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_cart_search",
                    "@product_name", name);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<CartModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
