using myshop.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.IServices
{
    public interface IProductService
    {
        public  Task<bool> AddNewProduct(ProductDto obj);

        public  Task<List<ProductDto>> GetAllProducts();

        public  Task<bool> EditProduct(ProductDto obj);

        public  Task<ProductDto?> GetProductById(int? id);

        public  Task<bool> DeleteProduct(int? id, string rootPath);
    }
}
