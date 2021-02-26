using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Controllers
{
    public class UserController : Controller
    {
        private readonly ProductContext _context;

        public UserController(ProductContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel reg)
        {
            if(ModelState.IsValid)
            {
                // map data to user account instance
                UserAccount acc = new UserAccount()
                {
                    DateOfBirth = reg.DateOfBirth,
                    Email = reg.Email,
                    Password = reg.Password,
                    Username = reg.Username
                };

                // add to database
                _context.UserAccounts.Add(acc);
                await _context.SaveChangesAsync();

                // redirect to home page
                return RedirectToAction("Index", "Home");
            }
            return View(reg);
        }

        public IActionResult Login()
        {
            // Check if user already logged in
            if(HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            UserAccount account =
                await (from u in _context.UserAccounts
                       where (u.Username == model.Username
                           || u.Email == model.Username)
                           && u.Password == model.Password
                       select u).SingleOrDefaultAsync();

            //UserAccount account1 =
            //    await _context.UserAccounts
            //        .Where(userAcc => (userAcc.Username == model.Username ||
            //                userAcc.Email == model.Username) &&
            //                userAcc.Password == model.Password).SingleOrDefaultAsync();

            if(account == null)
            {
                // credentials didnt match

                // custom error message
                ModelState.AddModelError(string.Empty, "Credentials were not found.");

                return View(model);
            }

            // Log in user to website
            HttpContext.Session.SetInt32("UserId", account.UserId);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            // destroy the session
            HttpContext.Session.Clear();

            return RedirectToAction(actionName:"Index", controllerName: "Home");
        }
    }
}
