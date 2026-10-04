using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using myshop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        
        private readonly DbSet<T> _dbSet;
        private readonly ILogger _logger;
        public GenericRepository(DbContext dbContext,ILogger logger) { 
           
          
            _dbSet = dbContext.Set<T>();
            _logger = logger;
        
        }


        public async Task<string> CreateAsync(T obj)
        {
            try { 
                await _dbSet.AddAsync(obj);
                return "Addedd Successfully";

            
            
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message);
                throw new Exception(ex.ToString());
            
            
            }
        }

        public virtual async Task<string> DeleteAsync(int ? id)
        {
            try
            {
                 var ele=await GetById(id);
                if (ele != null) {
                     _dbSet.Remove(ele);

                    return $"DeletedSuccessfully";

                }
                
                return "Failed To Deleted it's removed or something goes wrong";


                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.ToString());


            }
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                var allElements = await _dbSet.ToListAsync();
                if (allElements != null)
                {
                    

                    return allElements;

                }

                return null;



            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.ToString());


            }
        }

        public async Task<T> GetById(int ?id)
        {
            try
            {
                var element = await _dbSet.FindAsync(id);
                if (element != null)
                {


                    return element;

                }

                return null;



            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.ToString());


            }
        }

        public async Task<string> UpdateAsync(T obj)
        {
            try
            {
                  _dbSet.Update(obj);
              


                    return "Updated Successfully";

                

              



            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.ToString());


            }
        }
    }
}
