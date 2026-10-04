using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using myshop.BLL.Dto;
using myshop.BLL.Services;
using myshop.DAL.Data;
using myshop.Domain.Models;
using myshop.BLL.ApplicationServices.Interfaces;
using myshop.Web.ViewModels;
using X.PagedList;
using static System.Net.Mime.MediaTypeNames;
using myshop.BLL.IServices;


namespace myshop.Web.Areas.Admin.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class ProductController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        private readonly  string _rootPath = "";
        private readonly ICategoryService _categoryServices;
        private readonly IProductService _productServices;
        private readonly IMapper _mapper;
        public static List<ProductVM>? allProducts;

        public ProductController(IWebHostEnvironment webHostEnvironment, ICategoryService categoryServices, IMapper mapper, IProductService productService, IFileService fileService)
        {
            _mapper = mapper;

            _webHostEnvironment = webHostEnvironment;
            _categoryServices = categoryServices;
            _productServices = productService;
            _fileService = fileService;
            _rootPath = _webHostEnvironment.WebRootPath;
        }

        public async Task<IActionResult> Index()
        {
            try
            {

                var allProductsDto = await _productServices.GetAllProducts();
                allProducts = _mapper.Map<List<ProductVM>>(allProductsDto);


                return View(allProducts);

            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                // For simplicity, we'll just return the error message in the view.
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return Redirect("www.google.com");
            }
        }

        //[HttpGet]
        //public IActionResult GetData()
        //{
        //    var products = _context.Products
        //        .Include(x => x.Category)
        //        .Select(x => new
        //        {
        //            id = x.Id,
        //            name = x.Name,
        //            description = x.Description,
        //            price = x.Price,
        //            categoryName = x.Category.Name
        //        })
        //        .ToList();

        //    return Json(new { data = products });
        //}

        [HttpGet]
        //[AcceptVerbs("QUERY")]
        public async Task<IActionResult> Create()
        {
            try
            {
                TempData["ImgValidtionErrFlag"] = false ;
                TempData["ImgValidationMsg"] = "";
                TempData["IsFailedToAdd"] = false;
                var allCats = (from cats in await _categoryServices.GetAllcategories()
                               select new CategoryVM()
                               {
                                   Id = cats.Id,
                                   Name = cats.Name,


                               }).ToList();
                ViewBag.AllCats = allCats;

                return View();
            }
            catch (Exception ex) {
                return View();

            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVM productVM, IFormFile? file)
        {
            try
            {
                TempData["ImgValidtionErrFlag"] = false;
                TempData["ImgValidationMsg"] = "";
                //file = (IFormFile)productVM.Img;
                if (ModelState.IsValid)
                {
                    var ImgValidationResult = _fileService.ValidateImg(file);
                    if (ImgValidationResult != "") {
                        TempData["ImgValidtionErrFlag"] = true;
                        TempData["ImgValidationMsg"] = ImgValidationResult;
                     
                        ViewBag.AllCats = await   getAllCats();
                        TempData["IsFailedToAdd"] = false;

                        return View(productVM);
                    }
                  
                   
                    if (file != null)
                    {
                     
                      
                        var imgPath = await _fileService.UploadImgAsync(file, _rootPath);
                        productVM.Img = imgPath;
                    }
                    


                    var productObj = _mapper.Map<ProductDto>(productVM);
                    var result =await _productServices.AddNewProduct(productObj);
                    if (result) { 
                    TempData["Create"] = "Item has Created Successfully";
                        TempData["IsFailedToAdd"] = false;
                        return RedirectToAction("Index");
                    }
                    TempData["IsFailedToAdd"] = true;

                    var allCats = await getAllCats();
                    ViewBag.AllCats = allCats;
                }
                TempData["IsFailedToAdd"] = true;
                
                return View(productVM);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                // For simplicity, we'll just return the error message in the view.
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View(productVM);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["ImgValidtionErrFlag"] = false;
            TempData["ImgValidationMsg"] = "";
            TempData["IsFailedToAdd"] = false;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var allCats = await getAllCats();
            ViewBag.AllCats = allCats;
            var productAsDto = await _productServices.GetProductById(id);
            var ProductAsVm = _mapper.Map<ProductVM>(productAsDto);

            var productExistince = ProductAsVm;
            if (productExistince != null)
            {
                return View(productExistince);

            }
            else {

                return RedirectToAction("Index");

            }


        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductVM productVM, IFormFile? file)
        {
            TempData["ImgValidtionErrFlag"] = false;
            TempData["ImgValidationMsg"] = "";
            if (ModelState.IsValid)
            {
                var ImgValidationResult = _fileService.ValidateImg(file);
                if (ImgValidationResult != "")
                {
                    TempData["ImgValidtionErrFlag"] = true;
                    TempData["ImgValidationMsg"] = ImgValidationResult;

                    ViewBag.AllCats = await getAllCats();
                    TempData["IsFailedToAdd"] = false;

                    return View(productVM);
                }

                

                if (file != null)
                {
               

                    if (productVM.Img != null)
                    {
                        var result = _fileService.DeleteImg(productVM.Img, _rootPath);
                    }

                    var UploadImgResult =await  _fileService.UploadImgAsync(file, _rootPath);
                    productVM.Img = UploadImgResult??@"Images\Products\defaultProductsImage.webp";
                }
                if (productVM.Img == null && file==null) {
                    productVM.Img = @"Images\Products\defaultProductsImage.webp";
                }

                var isUpdated = await _productServices.EditProduct(_mapper.Map<ProductDto>(productVM));
                if (!isUpdated) {
                    TempData["ImgValidtionErrFlag"] = false;
                    TempData["ImgValidationMsg"] = "";
                    TempData["IsFailedToAdd"] = true;
                    var allCats =await getAllCats();
                    ViewBag.AllCats = allCats;

                    return View(productVM);
                
                }

                TempData["Update"] = "Data has Updated Successfully";
                return RedirectToAction("Index");
            }
            var allCatsIfModelItsNotValid = await getAllCats();
            ViewBag.AllCats = allCatsIfModelItsNotValid;
            TempData["ImgValidtionErrFlag"] = false;
            TempData["ImgValidationMsg"] = "";

            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? Id)
        {
            var IsDeleted =await _productServices.DeleteProduct(Id,_rootPath);


            return RedirectToAction("Index");
        }

        private  async Task<List<CategoryVM>> getAllCats()
        {

            var allCats = (from cats in await _categoryServices.GetAllcategories()
                           select new CategoryVM()
                           {
                               Id = cats.Id,
                               Name = cats.Name,


                           }).ToList();
            return allCats;



        }




    }
}
