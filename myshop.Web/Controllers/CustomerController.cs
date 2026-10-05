using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.IServices;
using myshop.BLL.Services;
using myshop.Domain.Models;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace myshop.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public CustomerController(IProductService productService, IMapper mapper) { 
           
            _productService = productService;
            _mapper = mapper;
        
        
        }

        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> CustomerHome()
        {

            TempData["countOfProducts"] = HttpContext.Session.Keys.Count();

            var allProductsDto = await _productService.GetAllProducts(null);
            var allProducts = _mapper.Map<List<ProductVM>>(allProductsDto);
            var allProductsJson = JsonConvert.SerializeObject(allProducts);


            return View(allProducts);

        }
        [HttpGet]
        public async Task<IActionResult> SearchProducts(string? searchByTitle = null)
        {
            var allProductsDto = await _productService.GetAllProducts(searchByTitle);
            var allProducts = _mapper.Map<List<ProductVM>>(allProductsDto);

            return Json(allProducts);
        }


    }
}
