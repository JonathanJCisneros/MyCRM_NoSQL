using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Core;
using MyCRMNoSQL.CustomExtensions;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.Service.Interfaces;
using System.Diagnostics;

namespace MyCRMNoSQL.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        private string? Uid
        {
            get
            {
                return HttpContext.Session.GetString("UserId");
            }
        }

        private bool LoggedIn
        {
            get
            {
                return Uid != null;
            }
        }

        public IActionResult Login()
        {
            if (LoggedIn)
            {
                return RedirectToAction("Dashboard", "CRM");
            }

            return View("Login");
        }

        public IActionResult Register()
        {
            if (LoggedIn)
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

            var Result = _userService.Login(User.Email);

            PasswordHasher<LoginFormModel> HashBrowns = new PasswordHasher<LoginFormModel>();
            PasswordVerificationResult PWCheck = HashBrowns.VerifyHashedPassword(User, Result.Password, User.Password);

            if(PWCheck == 0)
            {
                ModelState.AddModelError("Password", "is invalid");
                return Login();
            }

            HttpContext.Session.SetString("UserId", Result.Id);
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

            User user = new User()
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

            string Query = _userService.Register(user); 

            HttpContext.Session.SetString("UserId", Query);
            return RedirectToAction("Dashboard", "CRM");
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