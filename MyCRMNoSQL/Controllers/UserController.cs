using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Models;
using System.Diagnostics;

namespace MyCRMNoSQL.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
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

        public string StringToUpper(string s)
        {
            s = s.Trim().ToLower();
            return char.ToUpper(s[0]) + s.Substring(1);
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
        public IActionResult Logging(UserLogin User)
        {
            if(!ModelState.IsValid)
            {
                return Login();
            }

            User.Email = User.Email.Trim().ToLower();
            User.Password = User.Password.Trim();

            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Users").GetAll(User.Email)[new { index = "Email" }].IsEmpty().Run(Conn);

            if(Check == true)
            {
                ModelState.AddModelError("Email", "not found");
                return Login();
            }

            var DBUser = R.Db("MyCRM").Table("Users").GetAll(User.Email)[new { index = "Email"}].Pluck("id", "Password").CoerceTo("array").Run(Conn);
            string PW = DBUser[0].Password.ToString();

            PasswordHasher<UserLogin> HashBrowns = new PasswordHasher<UserLogin>();
            PasswordVerificationResult PWCheck = HashBrowns.VerifyHashedPassword(User, PW, User.Password);

            if(PWCheck == 0)
            {
                ModelState.AddModelError("Password", "is invalid");
                return Login();
            }

            string Id = DBUser[0].id.ToString();
            HttpContext.Session.SetString("UserId", Id);
            return RedirectToAction("Dashboard", "CRM");
        }

        [HttpPost]
        public IActionResult Registering(User NewUser)
        {
            if (!ModelState.IsValid)
            {
                return Register();
            }

            NewUser.FirstName = StringToUpper(NewUser.FirstName);
            NewUser.LastName = StringToUpper(NewUser.LastName);
            NewUser.Email = NewUser.Email.Trim().ToLower();
            NewUser.Password = NewUser.Password.Trim();

            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Users").GetAll(NewUser.Email)[new { index = "Email"}].IsEmpty().Run(Conn);
           
            
            if (Check == true)
            {
                ModelState.AddModelError("Email", "is taken");
                return Register();
            }

            PasswordHasher<User> hashBrowns = new PasswordHasher<User>();
            NewUser.Password = hashBrowns.HashPassword(NewUser, NewUser.Password);

            var Result = R.Db("MyCRM").Table("Users")
                .Insert(new
                {
                    FirstName = NewUser.FirstName,
                    LastName = NewUser.LastName,
                    Email = NewUser.Email,
                    Password = NewUser.Password,
                    CreatedAt = NewUser.CreatedAt,
                    UpdatedAt = NewUser.UpdatedAt,
                    UserType = NewUser.UserType
                })
            .Run(Conn);

            string Id = Result.generated_keys[0].ToString();
            HttpContext.Session.SetString("UserId", Id);
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