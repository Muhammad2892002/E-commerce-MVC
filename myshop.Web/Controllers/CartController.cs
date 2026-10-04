using Microsoft.AspNetCore.Mvc;
using myshop.BLL.IServices;
using myshop.BLL.Services;
using myshop.Web.ViewModels;
using Stripe;

namespace myshop.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
      
        public CartController(ICartService cartService) { 
        _cartService = cartService;
        
        
        }
        public async Task<IActionResult> Index()
        {
            TempData["countOfProducts"] = HttpContext.Session.Keys.Count();

            var allProducts=  _cartService.GetAllProductFromCart( HttpContext.Session); 
                return View(allProducts);
        }
        [HttpPost]
        public async Task<IActionResult> AddCart(int productId) {
           
            await _cartService.AddProductToCartAsync(productId,HttpContext.Session);
            var count=HttpContext.Session.Keys.Count();
            TempData["countOfProducts"] = count;
        
           return RedirectToAction("CustomerHome","Customer");
        
        }

        public async Task<IActionResult> RemoveItem(int Id) {

            var isRemoved = _cartService.RemoveItem(Id, HttpContext.Session);
            if (isRemoved)
            {
                return RedirectToAction("Index");

            }
            else {
                return RedirectToAction("Index");
            
            
            }
        
        }

        public IActionResult ClearCart() {


            var isCleared=  _cartService.removeAllProductsFromCart(HttpContext.Session);
            return RedirectToAction("Index");
        
        }

        [HttpPost]
        public IActionResult IncreaseQuantity([FromBody]ChangeQuantity obj) {
            var IncreaseResult = _cartService.IncreasedProduct(obj.ProductId, obj.Quantity, HttpContext.Session);
        
          return RedirectToAction("Index");
        
        }

        [HttpPost]
        public IActionResult DecreaseQuantity([FromBody] ChangeQuantity obj)
        {
            var IncreaseResult = _cartService.DecreaseProduct(obj.ProductId, obj.Quantity, HttpContext.Session);

            return RedirectToAction("Index");

        }


    }
}
