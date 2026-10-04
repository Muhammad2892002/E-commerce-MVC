using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using myshop.DAL.Data;
using myshop.DAL.Interfaces;
using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories
{
    public class CategoryRepo : GenericRepository<Category>,ICategory
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
       

        public CategoryRepo(ApplicationDbContext context,ILogger logger) :base(context,logger)
        {
            _context = context;
            _logger = logger;
          
        }



        public async Task<bool> CheckCategoryExistince(Category obj)
        {
            try
            {

                if (obj.Id == 0)
                {
                    var NormalizedName = obj.Name.ToUpper().Trim();
                    var checkCategory = _context.Categories.Any(x => x.Name.ToUpper().Trim().Contains(NormalizedName));
                    if (checkCategory)
                    {
                        return false;


                    }

                }
                else if (obj.Id > 0)
                {
                    var NormalizedName = obj.Name.ToUpper().Trim();
                    var checkCategory = _context.Categories.Any(x => x.Id != obj.Id && x.Name.ToUpper().Trim().Contains(NormalizedName));
                    if (checkCategory)
                    {
                        return false;

                    }




                }
                return true;
            }
            catch (Exception ex) {

                _logger.LogError(ex.Message.ToString());
                throw new Exception(ex.Message.ToString());
            
            }



            }
            

     
    }
}
