using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.IServices;
using myshop.BLL.Services;
using myshop.DAL.Data;
using myshop.Domain.Models;
using Stripe;
using System.Diagnostics;

namespace myshop.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,IProductService productService,IMapper mapper)

        {
            _productService = productService;
            _logger = logger;
            _mapper = mapper;
            
        }
        [Authorize(Policy = "CustomerAndAdmin")]
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("DashBoard");
            }

            else if (User.IsInRole("Customer")) { 
                return RedirectToAction("CustomerHome","Customer");
            
            
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize(Policy = "AdminOnly")]
        public IActionResult DashBoard() {

            return View();
        
        
        }


   
    }
}