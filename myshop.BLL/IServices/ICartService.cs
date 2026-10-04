using Microsoft.AspNetCore.Http;
using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.IServices
{
    public interface ICartService
    {
        public Task AddProductToCartAsync(int id,ISession session);
        public List<Cart> GetAllProductFromCart(ISession session);

        public bool RemoveItem(int id,ISession session);

        public bool removeAllProductsFromCart(ISession session);

        public Task<bool> IncreasedProduct(int ProductId,int quantity,ISession session);

        public Task<bool> DecreaseProduct(int productId, int quantity, ISession session);
    }
}
