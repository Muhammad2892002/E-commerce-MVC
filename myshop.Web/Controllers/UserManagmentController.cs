using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.IServices;
using myshop.BLL.Services;
using myshop.Web.ViewModels;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace myshop.Web.Controllers
{
    [Authorize(Policy = "AdminOnly")]
  
    public class UserManagmentController : Controller
    {
        private readonly IUserManagmentService _userManagmentServices;

        public UserManagmentController(IUserManagmentService userManagmentServices)
        {
            _userManagmentServices = userManagmentServices;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.LockoutFlagMsgAppear = TempData["ChangingLockOutFlag"]??false;
            ViewBag.LockOutMsgResult=TempData["MsgThatTheTheLockOutChanged"] ??null;
            ViewBag.RoleFlagChanged = TempData["ChangingRoleFlag"]??false;
            ViewBag.RoleMsgIfChanged=TempData["MsgThatTheRoleChanged"]??"";
            var allUsersFromBLL=await _userManagmentServices.GetAllUsers();
            var allUserAsVM=allUsersFromBLL.Select(user=>new DisplayUserVM() { 
               Id = user.Id,
                UserName = user.UserName,
                CurrentRole = user.CurrentRole,
                Email=user.Email,
                LockStatus = user.LockStatus,
            
            }).ToList();

            return View(allUserAsVM);
        }

        public async Task<IActionResult> PremoteUser(string Id) {
            var cuurentAccount = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var UserCurrentId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isPremoted = await  _userManagmentServices.ChangeRole(Id,"Admin", cuurentAccount);
            if (isPremoted!=null ) {
                if (isPremoted == "Your account role has been changed Successfully")
                {
                    TempData["ChangingRoleFlag"] = true;
                    TempData["MsgThatTheRoleChanged"] = isPremoted;
                    return RedirectToAction("Login", "Account");
                }

                TempData["ChangingRoleFlag"] = true;
                TempData["MsgThatTheRoleChanged"] = isPremoted;


            }
            return RedirectToAction("Index");





        }

        public async Task <IActionResult> DemoteUser(string Id) {
            var cuurentAccount = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var isDemoted =await _userManagmentServices.ChangeRole(Id, "Customer",cuurentAccount);
            if (isDemoted!=null)
            {
                if (isDemoted == "Your account role has been changed Successfully")
                {
                    TempData["ChangingRoleFlag"] = true;
                    TempData["MsgThatTheRoleChanged"] = isDemoted;
                    return RedirectToAction("Login", "Account");

                }

              
            }
            TempData["ChangingRoleFlag"] = true;
            TempData["MsgThatTheRoleChanged"] = isDemoted;
            return RedirectToAction("Index");


        }

        public async Task<IActionResult> LockUser(string Id) {
            TempData.Clear();
            var cuurentAccount = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var LockoutChangingResult=await _userManagmentServices.ChangeLookOutAccount(Id,cuurentAccount,true);
            TempData["ChangingLockOutFlag"] = true;
            TempData["MsgThatTheTheLockOutChanged"] = LockoutChangingResult;
            return RedirectToAction("Index");


        }

        public async Task<IActionResult> UnlockUser(string Id) {
            TempData.Clear();
            var cuurentAccount = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var LockoutChangingResult = await _userManagmentServices.ChangeLookOutAccount(Id, cuurentAccount, false);
            TempData["ChangingLockOutFlag"] = true;
            TempData["MsgThatTheTheLockOutChanged"] = LockoutChangingResult;
            return RedirectToAction("Index");



        }
    }
}
