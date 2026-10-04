using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Interfaces
{
    public interface IUnitOfWork 
    {
        //public IGenericRepository<Category> GenricCategory { get; }
        public IProduct Product { get; }
        public ICategory Category { get; }
            
      

        
        Task<int> SaveChangesAsync();
    }
}
