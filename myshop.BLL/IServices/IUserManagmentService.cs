using myshop.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.IServices
{
    public interface IUserManagmentService
    {
        public Task<List<DisplayUserDtoBLL>> GetAllUsers();

        public Task<string> ChangeRole(string Id, string role, string obj);

        public Task<string> ChangeLookOutAccount(string Id, string CurrentAccountId, bool lockOrNot);

    }
}
