using myshop.Domain.Dto_temp;
using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Interfaces
{
    public interface IUserManagment
    {
        public Task<List<UserInfoForDAL>> GetAllUsers();

        public Task<string> ChangeRole(string Id,string role, string obj);

        public Task<string> ChangeLockoutAccount(string Id, string CurrentUserId,bool LockOrnot);
    }
}
