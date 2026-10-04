using Microsoft.AspNetCore.Http;
namespace myshop.BLL.ApplicationServices.Interfaces
{
    public interface IFileService
    {
        public Task<string> UploadImgAsync(IFormFile obj,string rootPath);

        public bool DeleteImg(string path, string rootPath);

        public string ValidateImg(IFormFile obj);
    }
}
