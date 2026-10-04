using AutoMapper;
using myshop.BLL.Dto;
using myshop.Domain.Models;
using myshop.Web.ViewModels;

namespace myshop.Web.PresentationMapper
{
    public class PLMapper:Profile
    {
        public PLMapper() {
            CreateMap<CategoryDto, CategoryVM>().ReverseMap();
            CreateMap<ProductDto, ProductVM>().ReverseMap();
            CreateMap<UserDto,UserVM>().ReverseMap().ForMember(dest=>dest.FullName,opt=>opt.MapFrom(src=>$"{src.FirstName}{src.LastName}"));

        }
    }
}

