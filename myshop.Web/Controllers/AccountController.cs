using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using myshop.BLL.Dto;
using myshop.BLL.IServices;
using myshop.BLL.Services;
using myshop.Web.ViewModels;
using Stripe;

namespace myshop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper mapper;
        private readonly IAccountService accountService;


        public AccountController(IMapper mapper,IAccountService service)
        {
            this.mapper = mapper;
            accountService = service;
        }
        public IActionResult Login()
        {
            ViewBag.LockoutFlagMsgAppear = TempData["ChangingLockOutFlag"] ?? false;
            ViewBag.LockOutMsgResult = TempData["MsgThatTheTheLockOutChanged"] ?? null;
            ViewBag.RoleFlagChanged = TempData["ChangingRoleFlag"] ?? false;
            ViewBag.RoleMsgIfChanged = TempData["MsgThatTheRoleChanged"] ?? "";
            TempData["FailedToLogFlag"] = false;
            TempData["msg"] = "";
            ViewBag.RegisteredSuccessfully = TempData["RegisteredSuccessfully"] ?? false;
            ViewBag.massage = TempData["massage"] ?? "";
            return View();
        }
        

        public IActionResult SignUp()
        {
            TempData["FaildFlg"] = false;
            TempData["massage"] = "";
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(UserVM obj) {

            var userAsDto=mapper.Map<UserDto>(obj);
           var isRegistered= await accountService.SignUp(userAsDto);
            if (!isRegistered) {
                TempData["FaildFlg"] = !isRegistered;
                TempData["massage"]= "PhoneNumber or Email is already exist"; 
            return View(obj);
            }
            TempData["RegisteredSuccessfully"] = isRegistered;
            TempData["massage"] = "Registered Successfully";


            return RedirectToAction("Login");


            
        
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM obj)
        {
            ViewBag.LockoutFlagMsgAppear = TempData["ChangingLockOutFlag"] ?? false;
            ViewBag.LockOutMsgResult = TempData["MsgThatTheTheLockOutChanged"] ?? null;
            ViewBag.RegisteredSuccessfully = TempData["RegisteredSuccessfully"] ?? false;
            ViewBag.massage = TempData["massage"] ?? "";
            TempData["FailedToLogFlag"] = false;
            TempData["msg"] = "";
            var LogInResult=await accountService.Login(obj.UserName, obj.Password,obj.RememberMe);
            if (LogInResult== "LogedIn")
            {
             
              

                if (User.IsInRole("Admin")) {

                    return RedirectToAction("DashBoard", "Home");
                }
                if (User.IsInRole("Customer")) {
                    return RedirectToAction("CustomerHome", "Customer");

                }
            }
            else {
                TempData["FailedToLogFlag"] = true;
                TempData["msg"] = LogInResult;

                return View(obj);
            
            }
            return RedirectToAction("Error", "Home");
        }


        public async Task<IActionResult> Logout() { 

           var isSignedOut= await accountService.LogOut();
            if (isSignedOut)
            {
                return RedirectToAction("Login");
            }
            return RedirectToAction("Index", "Home");



        }


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
