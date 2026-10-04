using AutoMapper;
using myshop.BLL.ApplicationServices.Interfaces;
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
    
    public class ProductServices : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IFileService _fileService;
        public ProductServices(IUnitOfWork unitOfWork,IMapper mapper,IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<bool> AddNewProduct(ProductDto obj)
        {
            try { 
                var product=_mapper.Map<Product>(obj);
                product.Category = null;
              var item =await _unitOfWork.Product.CreateAsync(product);
                if (item!=null)
                {
                    var result = await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                return false;
                

               
            
            
            }
            catch(Exception ex) {

                throw new Exception("Contact Admin product didnot Add");
            }
        
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            try
            {

                var allProducts = await _unitOfWork.Product.GetAll();
                var allProductsAsDto = (from product in allProducts
                                        select new ProductDto()
                                        {
                                            Id = product.Id,
                                            Name = product.Name,
                                            Description = product.Description,
                                            CategoryName = product.Category.Name,
                                            CategoryId = product.Category.Id,
                                            Price = product.Price,
                                            Img = product.Img,


                                        }
                                      ).ToList();

                return allProductsAsDto;
            }
            catch (Exception ex) {

                throw new Exception("Cannot load products contact the admin");


            }
                    
                    

        }

        public async Task<bool> EditProduct(ProductDto obj) {
            try
            {

                var product = _mapper.Map<Product>(obj);
                product.Category = null;
                var isUpdated= await _unitOfWork.Product.UpdateAsync(product);
                if (isUpdated!=null)
                {
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                else {
                    return false;

                }
                
            }
            catch { 
            
              throw new Exception("Failed to Edit Contact the admin");
            
            }



        }


        public async Task<ProductDto?> GetProductById(int? id) {
            try
            {
                var productAsEntity = await _unitOfWork.Product.GetById(id);
                var productAsDto = _mapper.Map<ProductDto>(productAsEntity);

                return productAsDto;
            }
            catch  { 
              
                throw new Exception("There is a problem contact the admin");
            
            }

        }

        public async Task<bool> DeleteProduct(int? id,string rootPath) {
            try
            {
                var DeleteResult = await _unitOfWork.Product.DeleteAsync(id);
                if (DeleteResult != null)
                {
                    _fileService.DeleteImg(DeleteResult,rootPath);
                    await _unitOfWork.SaveChangesAsync();
                    return true;


                }
                else
                {
                    return false;


                }


            }
            catch { 
             
                throw new Exception("Delete Failed Contact the admin");
            
            }

        }

      
    }
}
