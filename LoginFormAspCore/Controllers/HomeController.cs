using LoginFormAspCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LoginFormAspCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly LoginFormContext context;

        public HomeController(LoginFormContext context)
        {
            this.context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserInfo User)
        {
            var myUser = context.UserInfos.Where(x => x.Email  == User.Email && x.Password == User.Password).FirstOrDefault();
            if (myUser !!= null)
            {
                HttpContext.Session.SetString("UserSession", myUser.Email);
                HttpContext.Session.SetString("UserSession1", myUser.Password);
                return Redirect("Dashboard");
            }
            else
            {
                ViewBag.Message = "Login Failed..";
            }
            return View();
        }
        public IActionResult Dashboard()
        {
           if(HttpContext.Session.GetString("UserSession") != null)
           {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
                ViewBag.MySession1 = HttpContext.Session.GetString("UserSession1").ToString();

            }
           else
           {
                return Redirect("Login");
           }
            return View();
        }

        public IActionResult Logout()
        {
            if(HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                return Redirect("Login");
            }
            return View();
        }
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }
        // [HttpPost]
        //public async Task<IActionResult> Signup(UserInfo User)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (var connection = context.Database.GetDbConnection())
        //        {
        //            await connection.OpenAsync();

        //            using (var command = connection.CreateCommand())
        //            {
        //                // Enable IDENTITY_INSERT
        //                command.CommandText = "SET IDENTITY_INSERT User_Info ON;";
        //                await command.ExecuteNonQueryAsync();

        //                // Add the UserInfo entity (do not set the ID explicitly)
        //                context.UserInfos.Add(User);
        //                await context.SaveChangesAsync();

        //                // Disable IDENTITY_INSERT
        //                command.CommandText = "SET IDENTITY_INSERT User_Info OFF;";
        //                await command.ExecuteNonQueryAsync();
        //            }
        //        }

        //        TempData["success"] = "Registered Successfully";
        //        return RedirectToAction("Login");
        //    }
        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> Signup(UserInfo User)
        {
                if (ModelState.IsValid)
                {
                    await context.UserInfos.AddAsync(User); // No need to set `User.id`
                    await context.SaveChangesAsync(); // `id` will be auto-generated
                    TempData["success"] = "Registered Successfully";
                    return RedirectToAction("Login");
                }
                return View();

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
