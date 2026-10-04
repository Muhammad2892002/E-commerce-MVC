using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.Domain.Models
{
    public class ProductVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string Img { get; set; }

        [Required]
       
        public decimal Price { get; set; }

        [Required]

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

       
    }
}
