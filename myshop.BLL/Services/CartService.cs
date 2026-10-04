using Microsoft.AspNetCore.Http;
using myshop.BLL.IServices;
using myshop.DAL.Interfaces;
using myshop.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services
{
    public class CartService :ICartService
    {
        private IUnitOfWork _unitOfWork;
        public CartService(IUnitOfWork unitOfWork) { 
           
            _unitOfWork = unitOfWork;
        
        }
        public async Task AddProductToCartAsync(int id,ISession session) {
            var Product=await _unitOfWork.Product.GetById(id);
            if (Product != null)
            {
                Cart product = new Cart() {
                    ProductId = Product.Id,
                    ProductName = Product.Name,
                    Description = Product.Description,
                    Price = Product.Price,
                    ImageUrl = Product.Img,
                    Quantity = 1


                };
                var productAsJson=JsonConvert.SerializeObject(product);
                session.SetString($"CartItem-{Product.Id}", productAsJson);

               
                


            }
            else { 
             
            
            }
        
        
        }


        public  List<Cart> GetAllProductFromCart(ISession session) { 
           
            List<Cart>cartItems=new List<Cart>();
            foreach (var key in session.Keys) {

                if (key.StartsWith("CartItem-")) {

                    var productAsJson = session.GetString(key);
                    if (!string.IsNullOrEmpty(productAsJson)) {
                        var product = JsonConvert.DeserializeObject<Cart>(productAsJson);
                        cartItems.Add(product);
                      
                    
                    }
                
                
                
                }
            
            
            }
            return cartItems;
        
        
        }



        public bool RemoveItem(int id,ISession session) {

            var item = session.GetString($"CartItem-{id}");
            
            if (item != null) {
               
             
                session.Remove($"CartItem-{id}");
                return true;
            
            }
            else { 
              
                return false;
            
            }
        
        
        
        
        }


         public bool removeAllProductsFromCart(ISession session) { 
            session.Clear();
            return true;
        
        
        
        
        
        }


        public async Task<bool> IncreasedProduct(int productId, int quantity, ISession session) {


            var ProductAsJson = session.GetString($"CartItem-{productId}");
            Cart product;
            if (ProductAsJson != null)
            {
                product = JsonConvert.DeserializeObject<Cart>(ProductAsJson);
                product.Quantity=quantity;
                var updatedQuantity=JsonConvert.SerializeObject(product);
                session.SetString($"CartItem-{productId}",updatedQuantity);




                return true;





            }
            else
            {
                return false;


            }






        }

        public async Task<bool> DecreaseProduct(int productId, int quantity, ISession session) {
            var ProductAsJson = session.GetString($"CartItem-{productId}");
            Cart product;
            if (ProductAsJson != null)
            {
                product = JsonConvert.DeserializeObject<Cart>(ProductAsJson);
                product.Quantity = quantity;
                var updatedQuantity = JsonConvert.SerializeObject(product);
                session.SetString($"CartItem-{productId}", updatedQuantity);




                return true;





            }
            else
            {
                return false;


            }










        }





    }
}
