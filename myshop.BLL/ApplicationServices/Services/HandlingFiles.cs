using myshop.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata;
using myshop.BLL.ApplicationServices.Interfaces;
 

namespace myshop.BLL.ApplicationServices.Services
{
    public class HandlingFiles : IFileService
    {
      
        private static string storageImgFolder = @"Images\Products\";
      
        private List <string> allowdExtensions= new List<string>() { 
         ".jpg",
         ".jpeg",
         ".png",
         ".webp"
        
        };
        private const int MAX_SIZE= 2*1024*1024;

     
        public bool DeleteImg(string path,string rootPath)
        {
            var ImgFullPath = IsImgExist(path,rootPath);
            if (ImgFullPath!=null) {
                System.IO.File.Delete(ImgFullPath);

                return true;

            }
            return false;
        }

        public async Task<string> UploadImgAsync(IFormFile obj,string rootPath)
        {
           
          
          

           
            var newFileName = Guid.NewGuid().ToString();

            var storageFilePlace = Path.Combine(rootPath, storageImgFolder);
            var ext = Path.GetExtension(obj.FileName);

            var fullImgpath = Path.Combine(storageFilePlace, newFileName+ext);
            await using (var fileStream = new FileStream(fullImgpath, FileMode.Create))
            {

                obj.CopyTo(fileStream);


            }
            var path=storageImgFolder + newFileName + ext;
            return path;
        }

        public string ValidateImg(IFormFile obj)
        {
          var ext=Path.GetExtension(obj.FileName);
            string errMsg = "";
            var isExtensionValid = allowdExtensions.Contains(ext);
            if (!isExtensionValid) {
                errMsg += "The img extension it is not valid These are Valid .jpg,.jpeg,.png,.webp \n";



            }
            if (obj.Length > MAX_SIZE) {

                errMsg += "The size must be less than 2MB";
            
            }
            return errMsg;
        }

        public  string IsImgExist(string file,string rootPath) {

            var oldimg = Path.Combine(rootPath, file.TrimStart('\\'));

            if (System.IO.File.Exists(oldimg))
            {
                //System.IO.File.Delete(oldimg);
                return oldimg;
            }
            return null;
        }
    }
}
