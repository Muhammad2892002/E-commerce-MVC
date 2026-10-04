using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using myshop.DAL.Interfaces;
using myshop.Domain.Dto_temp;
using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories
{
    public class UserManagment : IUserManagment
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccountRepo _accountRepo;
        private readonly ILogger<UserManagment> _logger;
        public UserManagment(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IAccountRepo accountRepo,ILogger<UserManagment> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _accountRepo = accountRepo;
            _logger = logger;
        }

        public async  Task<string> ChangeLockoutAccount(string Id, string CurrentUserId ,bool lockOrNot)
        {
            try
            {
                string massage = null;
                var user = await _userManager.FindByIdAsync(Id);
                if (lockOrNot && user.LockoutEnabled == true)
                {
                    user.LockoutEnd = DateTimeOffset.UtcNow.AddDays(2);
                    await _userManager.UpdateAsync(user);

                    massage = "Account is Looked";
                }
                else
                {
                    user.LockoutEnd = null;
                    await _userManager.UpdateAsync(user);
                    massage = "Account is not  Looked";


                }
                if (Id == CurrentUserId)
                {

                    await _accountRepo.LogOut();
                }
                return massage;
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message.ToString());
                throw new Exception(ex.Message.ToString());

            }
            
        }

        public async Task<string> ChangeRole(string Id,string role, string CurrentuserId)
        {
            try
            {
                var userObj = await _userManager.FindByIdAsync(Id);
                if (userObj != null)
                {

                    var currentRole = await _userManager.GetRolesAsync(userObj);
                    await _userManager.RemoveFromRolesAsync(userObj, currentRole);
                    await _userManager.AddToRoleAsync(userObj, role);
                    //var CurrentUser = await _userManager.GetUserIdAsync(obj);
                    if (Id == CurrentuserId) {
                       
                        var isLoogedOut= await _accountRepo.LogOut();
                        if (isLoogedOut) {

                            return "Your account role has been changed Successfully";
                            
                        
                        
                        
                        }


                    
                    }




                    return "Changed Successfully";


                }
                return null;
            }
            catch (Exception ex) {
                _logger.LogError($"{ex.Message}");
                throw new Exception(ex.ToString());
            
            
            }
        }

        public async Task<List<UserInfoForDAL>> GetAllUsers()
        {
            try
            {
                List<UserInfoForDAL> allUsersAsaList = new List<UserInfoForDAL>();
                var allUsers = await _userManager.Users.ToListAsync();

                foreach (var user in allUsers)
                {

                    var allRoles = await _userManager.GetRolesAsync(user);
                    string roleAsAstring = allRoles.FirstOrDefault() ?? "No Role";

                    allUsersAsaList.Add(new UserInfoForDAL
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        LockStatus = user.LockoutEnd == null || user.LockoutEnd < DateTime.UtcNow,
                        CurrentRole = roleAsAstring,

                    });



                }
                return allUsersAsaList;



            }
            catch (Exception ex) {


                _logger.LogError(ex.Message.ToString());
                throw new Exception(ex.ToString());
            }
          
        
            
            
          
        }

       
    }
}
