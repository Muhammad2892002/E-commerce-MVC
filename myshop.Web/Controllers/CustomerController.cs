using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.IServices;
using myshop.BLL.Services;
using myshop.Domain.Models;
using myshop.Web.ViewModels;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace myshop.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryServices;

        public CustomerController(IProductService productService, IMapper mapper, ICategoryService categoryServices) { 
           
            _productService = productService;
            _mapper = mapper;
            _categoryServices = categoryServices;
        
        
        }

        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> CustomerHome()
        {

            TempData["countOfProducts"] = HttpContext.Session.Keys.Count();

            var allProductsDto = await _productService.GetAllProducts(null);
            var allProducts = _mapper.Map<List<ProductVM>>(allProductsDto);
         
            var allCats = (from cats in await _categoryServices.GetAllcategories()
                           select new CategoryVM()
                           {
                               Id = cats.Id,
                               Name = cats.Name,


                           }).ToList();
            ViewBag.AllCats = allCats;


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
