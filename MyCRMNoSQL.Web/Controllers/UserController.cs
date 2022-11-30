using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Core;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.Service.Interfaces;
using System.Diagnostics;

namespace MyCRMNoSQL.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IExtension _extension;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IExtension extension, IUserService userService)
        {
            _logger = logger;
            _extension = extension;
            _userService = userService;
        }

        public IActionResult Login()
        {
            if (_extension.LoggedIn())
            {
                return RedirectToAction("Dashboard", "CRM");
            }

            return View("Login");
        }

        public IActionResult Register()
        {
            if (_extension.LoggedIn())
            {
                return RedirectToAction("Dashboard", "CRM");
            }

            return View("Register");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Login();
        }

        public IActionResult Profile()
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login");
            }

            var User = _userService.Get(_extension.UserId());

            return View(User);
        }

        [HttpPost]
        public IActionResult Logging(LoginFormModel User)
        {
            if(!ModelState.IsValid)
            {
                return Login();
            }

            User = LoginFormModel.DbPrep(User);

            bool Check = _userService.CheckByEmail(User.Email); 

            if(Check == true)
            {
                ModelState.AddModelError("Email", "not found");
                return Login();
            }

            User Result = _userService.Login(User.Email);

            PasswordHasher<LoginFormModel> HashBrowns = new PasswordHasher<LoginFormModel>();
            PasswordVerificationResult PWCheck = HashBrowns.VerifyHashedPassword(User, Result.Password, User.Password);

            if(PWCheck == 0)
            {
                ModelState.AddModelError("Password", "is invalid");
                return Login();
            }

            _userService.UpdateTimeStamp(Result.Id);

            HttpContext.Session.SetString("UserId", Result.Id);
            HttpContext.Session.SetString("Type", Result.Type);
            return RedirectToAction("Dashboard", "CRM");
        }

        [HttpPost]
        public IActionResult Registering(RegisterFormModel NewUser)
        {
            if (!ModelState.IsValid)
            {
                return Register();
            }

            NewUser = RegisterFormModel.DbPrep(NewUser);

            bool Check = _userService.CheckByEmail(NewUser.Email);
            
            if (Check == false)
            {
                ModelState.AddModelError("Email", "is taken");
                return Register();
            }

            PasswordHasher<RegisterFormModel> hashBrowns = new PasswordHasher<RegisterFormModel>();
            NewUser.Password = hashBrowns.HashPassword(NewUser, NewUser.Password);

            User user = new()
            {
                FirstName = NewUser.FirstName,
                LastName = NewUser.LastName,
                Email = NewUser.Email,
                Password = NewUser.Password,
                Type = NewUser.Type,
                LastLoggedIn = NewUser.LastLoggedIn,
                CreatedDate = NewUser.CreatedDate,
                UpdatedDate = NewUser.UpdatedDate
            };

            string Id = _userService.Create(user); 

            HttpContext.Session.SetString("UserId", Id);
            HttpContext.Session.SetString("Type", user.Type);
            return RedirectToAction("Dashboard", "CRM");
        }

        [HttpPost]
        public IActionResult Update(RegisterFormModel model)
        {
            if (!ModelState.IsValid) 
            {
                return RedirectToAction("Profile");
            }

            User user = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UpdatedDate = model.UpdatedDate
            };

            string Result = _userService.Update(user);
            return Profile();
        }

        public IActionResult Delete(string id)
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login");
            }

            _userService.Delete(id);

            return Logout();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}