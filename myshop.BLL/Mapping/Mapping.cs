using AutoMapper;
using myshop.BLL.Dto;
using myshop.Domain.Dto_temp;
using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myshop.Domain.Dto_temp;

namespace myshop.BLL.Mapping
{
    public class Mapping :Profile
    {
        public Mapping() { 
        
         CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap().ForMember(dest=>dest.UserName,opt=>opt.MapFrom(src=>src.Email));
          

        }
    }
}
