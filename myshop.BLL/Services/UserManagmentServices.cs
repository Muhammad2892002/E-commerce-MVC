using AutoMapper;
using myshop.BLL.Dto;
using myshop.BLL.IServices;
using myshop.DAL.Interfaces;
using myshop.Domain.Dto_temp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services
{
    public class UserManagmentServices : IUserManagmentService
    {
        private readonly IUserManagment _userManagment;
        private readonly IMapper _mapper;
        public UserManagmentServices(IUserManagment userManagment,IMapper mapper) { 
           
            _userManagment = userManagment;
            _mapper = mapper;
        
        }
        public async Task<List<DisplayUserDtoBLL>> GetAllUsers() {
            try
            {

                var allUsersFromDAL = await _userManagment.GetAllUsers();







                var allUsersAsDTOBLL = allUsersFromDAL.Select(obj => new DisplayUserDtoBLL()
                {
                    Id = obj.Id,
                    UserName = obj.UserName,
                    CurrentRole = obj.CurrentRole,
                    Email = obj.Email,
                    LockStatus = obj.LockStatus,


                }).ToList();


                return allUsersAsDTOBLL;
             
            }
            catch (Exception ex) {

                throw new Exception(ex.Message.ToString());
            
            }
            
        
        }

        public async Task<string> ChangeRole(string Id,string role,string obj) {
            try
            {

                var roleChangedMsg = await _userManagment.ChangeRole(Id, role, obj);
                if (roleChangedMsg != null)
                {
                    if (roleChangedMsg == "Your account role has been changed Successfully")
                    {
                        return roleChangedMsg;


                    }
                    return roleChangedMsg;



                }
                else
                {

                    return roleChangedMsg;
                }
            }
            catch (Exception ex) {

                throw new Exception(ex.Message.ToString());
            
            }

            
        
        }

        public async Task<string> ChangeLookOutAccount(string Id, string CurrentAccountId,bool lockOrNot) {
            try
            {
                var lockResult = await _userManagment.ChangeLockoutAccount(Id, CurrentAccountId, lockOrNot);

                return lockResult;
            }
            catch (Exception ex) { 
             
                throw new Exception($"{ex.Message}");
            
            }
        
        }
    }
}
