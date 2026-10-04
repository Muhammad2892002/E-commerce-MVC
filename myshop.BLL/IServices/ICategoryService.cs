using myshop.BLL.Dto;
using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.IServices
{
    public interface ICategoryService
    {
        public  Task<bool> AddCategory(CategoryDto? obj);

        public Task<List<Category>> GetAllcategories();
        public  Task<CategoryDto> GetCategoryById(int id);

        public  Task<bool> EditCategory(CategoryDto obj);

        public  Task DeleteCategory(int id);
    }
}
