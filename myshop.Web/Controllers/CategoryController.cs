using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using myshop.BLL.Dto;
using myshop.BLL.IServices;
using myshop.BLL.Services;
using myshop.DAL.Data;
using myshop.DAL.Interfaces;
using myshop.Domain.Models;
using myshop.Web.ViewModels;

namespace myshop.Web.Areas.Admin.Controllers
{
    [Authorize(Policy= "AdminOnly")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _category;
        private readonly IMapper _mapper;

    

        public CategoryController(ICategoryService category,IMapper maper)
        {
            _category = category;
            _mapper = maper;
        }

        public async Task<IActionResult> Index()
        {
            var allCats= (from Category in await _category.GetAllcategories()
                         select new CategoryVM() { 
                          Id = Category.Id,
                          Name = Category.Name,
                          Description = Category.Description,
                          CreatedTime = Category.CreatedTime,
                         
                         
                         }).ToList();
            //var categories = _context.Categories.ToList();
            //return View(categories);
            return View(allCats);
        }

        [HttpGet]
        public IActionResult Create()
        {
            



            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryVM category)
        {
            if (ModelState.IsValid)
            {
              var CategoryDtoObj=_mapper.Map<CategoryDto>(category);
               var AddingResult= await _category.AddCategory(CategoryDtoObj);
                if (!AddingResult) {
                    ModelState.AddModelError("Name","category already exist");
                    return View(category);


                }

             


                TempData["Create"] = "Item has Created Successfully";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null | id == 0) NotFound();
             var item = await _category.GetCategoryById(id);
          var vm = _mapper.Map<CategoryVM>(item);
            return View(vm); 
            
           
          

           
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryVM category)
        {
            if (ModelState.IsValid)
            {
             var catObj=_mapper.Map<CategoryDto>(category);
                bool isEdited=await _category.EditCategory(catObj);
                if (isEdited) { 
                  TempData["Update"] = "Data has Updated Successfully";
                return RedirectToAction("Index");
                }
                ModelState.AddModelError("Name","Category Name already exist choose another one");
               
              
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null | id == 0)
            {
                NotFound();
            }
            var BringCategory =await _category.GetCategoryById(id);
          var categoryVm=_mapper.Map<CategoryVM>(BringCategory);

            return View(categoryVm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _category.DeleteCategory(id);
           
            TempData["Delete"] = "Item has Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
