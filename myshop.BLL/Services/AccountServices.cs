using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using myshop.BLL.Dto;
using myshop.BLL.IServices;
using myshop.DAL.Interfaces;
using myshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services
{
    public class AccountServices : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepo _accountRepo;
        private readonly ILogger<AccountServices> _logger;
       
      
        public AccountServices(IMapper mapper, IAccountRepo accountRepo,SignInManager<ApplicationUser> signInManager,ILogger<AccountServices> logger)
        {

            _mapper = mapper;
            _accountRepo = accountRepo;
            _logger = logger;
         
        }

        public async Task<bool> SignUp(UserDto obj) {
            try
            {
                var UserAsEntity = _mapper.Map<ApplicationUser>(obj);
                var result = await _accountRepo.RegisterAsync(UserAsEntity, obj.Password);
                return result;
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message.ToString());
               
                throw new Exception($"An error occurred while signing up: {ex.Message}", ex);

            }
          
           
        
        
        }

        public async Task<string> Login(string email,string password,bool remeberMe)
        {
            try
            {
                var result = await _accountRepo.Login(email, password, remeberMe);
                return result;
            }
            catch (Exception ex) { 
             _logger.LogError($"Failed to login {email}", ex);
                throw new Exception(ex.Message.ToString());
            
            }



        }


        public async Task<bool> LogOut()
        {
            try
            {
                var isSignedOut = await _accountRepo.LogOut();
                if (isSignedOut)
                {


                    return true;
                }


                return false;
            }
            catch (Exception ex) { 
              _logger.LogError(ex.Message.ToString());
                throw new Exception(ex.Message);
            
            }

        }



    }
}
