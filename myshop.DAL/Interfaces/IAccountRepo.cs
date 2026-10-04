using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Interfaces
{
    public interface IAccountRepo
    {
        public Task<bool> RegisterAsync(ApplicationUser obj,string Password);

        public Task<string> Login(string email,string password,bool rememberMe);

        public Task<bool> LogOut();
    }
}
