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
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        //public IGenericRepository<Category> GenricCategory { get; }
        public IProduct Product { get; }
        public ICategory Category { get; }


        //Category IUnitOfWork.Category => throw new NotImplementedException();

        public UnitOfWork(ApplicationDbContext context,ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;

            //GenricCategory = new CategoryRepo(context,_logger);
            Category=new CategoryRepo(context,_logger);

            Product = new ProductRepo(context,_logger);
         
           
        }




        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex) { 
              
                Console.WriteLine(ex.ToString());
                throw;
            
            }
        }
    }
}
