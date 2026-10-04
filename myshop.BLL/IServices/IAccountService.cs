using myshop.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.IServices
{
    public interface IAccountService
    {
        public Task<bool> SignUp(UserDto obj);

        public  Task<string> Login(string email, string password, bool remeberMe);

        public  Task<bool> LogOut();



    }
}
