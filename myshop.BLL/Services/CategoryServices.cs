using AutoMapper;
using myshop.BLL.Dto;
using myshop.BLL.IServices;
using myshop.DAL.Interfaces;
using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace myshop.BLL.Services
{
    public class CategoryServices : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryServices(IUnitOfWork unit,IMapper mapper) { 
         _mapper=mapper;
            _unitOfWork = unit;
        
        }
        public async Task<bool> AddCategory(CategoryDto? obj)
        {
            try
            {

                var category = _mapper.Map<Category>(obj);
                var result = await _unitOfWork.Category.CreateAsync(category);
                if (result != null)
                {

                    await _unitOfWork.Category.CreateAsync(category);
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;

                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            
            }
        }

        public async Task<List<Category>> GetAllcategories() {
            try
            {
                var allCats = await _unitOfWork.Category.GetAll();

                return allCats.ToList();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            
            }
            
        
        
        }

        public async Task<CategoryDto> GetCategoryById(int id ) {

            try
            {


                var category = await _unitOfWork.Category.GetById(id);
                var categoryDto = _mapper.Map<CategoryDto>(category);


                return categoryDto;
            }
            catch (Exception ex) {

                throw new Exception(ex.Message.ToString());
            
            }
        }

        public async Task<bool> EditCategory(CategoryDto obj)
        {
            try
            {
                var catObj = _mapper.Map<Category>(obj);
                string result = await _unitOfWork.Category.UpdateAsync(catObj);
                if (result != null)
                {

                    await _unitOfWork.Category.UpdateAsync(catObj);
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;




                }
            }
            catch (Exception ex) {

                throw new Exception(ex.Message.ToString());
            
            }
        
        }

        public async Task DeleteCategory(int id) {
            try
            {

                await _unitOfWork.Category.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex) {

                throw new Exception(ex.Message.ToString());
            
            }
        
        }

        

        
    }
}
