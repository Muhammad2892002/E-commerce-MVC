using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Interfaces
{
    public interface IGenericRepository<T> where T: class
    {
        public Task<string> CreateAsync(T obj);

        public Task<string> UpdateAsync(T obj);

        public Task<string> DeleteAsync(int ? id);

        public Task<T> GetById(int ? id);

        public  Task<IEnumerable<T>> GetAll();

    }
}
