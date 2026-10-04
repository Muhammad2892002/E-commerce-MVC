using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using myshop.DAL.Data;
using myshop.DAL.Interfaces;
using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories
{
    public class ProductRepo : GenericRepository<Product>,IProduct
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ProductRepo(ApplicationDbContext context,ILogger logger) :base(context,logger)
        {
            _context = context;
            logger=_logger;

        }
   

      

        public override async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                var allProducts = await _context.Products.Include(p => p.Category).ToListAsync();
                return allProducts;
            }
            catch (Exception ex) {

                _logger.LogError(ex.Message.ToString());
                throw new Exception(ex.Message.ToString());
            
            }

        }

      

        public async Task<bool> CheckIfProductExist(Product obj)
        {
            try {
                if (obj.Id == 0)
                {
                    var NormalizedProductname = obj.Name.Trim().ToUpper();
                    var isProductExist = await _context.Products.AnyAsync(p => p.Name.Trim().ToUpper().Contains(NormalizedProductname));
                    if (isProductExist)
                    {

                        return true;

                    }
                    
                }
                else {
                    var NormalizedProductname = obj.Name.Trim().ToUpper();
                    var isProductExist = await _context.Products.AnyAsync(p =>  p.Id!=obj.Id&& p.Name.Trim().ToUpper().Contains(NormalizedProductname));
                   return isProductExist;

                }
                return false;

               
            }
            catch(Exception ex) {
                _logger.LogError(ex.Message.ToString());
                throw new Exception(ex.Message);


                
            }
        
        
        
        
        }

        public override async Task<string?> DeleteAsync(int? id)
        {
            try
            {
                var product = await GetById(id);
                if (product != null)
                {
                    _context.Remove(product);

                    return product.Img;

                }

                return null;



            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.ToString());


            }
        }
    }
}
