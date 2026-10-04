using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Interfaces
{
    public interface ICategory :IGenericRepository<Category>
    {
       
     

        public Task<bool> CheckCategoryExistince(Category obj);


    }
}
