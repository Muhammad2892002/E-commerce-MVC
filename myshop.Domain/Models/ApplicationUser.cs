using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace myshop.Domain.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
    }
}
