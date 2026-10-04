using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Interfaces
{
    public interface IProduct :IGenericRepository<Product>
    {
        public Task<bool> CheckIfProductExist(Product obj);
    }
}
