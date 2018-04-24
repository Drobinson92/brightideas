using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using cbelt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace cbelt.Controllers
{
    public class HomeController : Controller
    {
        private BeltContext _context;
        public HomeController(BeltContext context){
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LoginPage(){
            return View("Login");
        }

        public IActionResult Register(RegisterViewModel user){
            if(ModelState.IsValid){
                PasswordHasher<RegisterViewModel> Hasher = new PasswordHasher<RegisterViewModel>();
                user.Password = Hasher.HashPassword(user, user.Password);
                User check = _context.Users.SingleOrDefault(a => a.Email == user.Email);
                if(check != null){
                    ViewBag.Error = "User already exists";
                    return View("Index");
                }
                User u = new User{
                    Name = user.Name,
                    Alias = user.Alias,
                    Email = user.Email,
                    Password = user.Password
                };
                // HttpContext.Session.SetString("email", u.Email);
                _context.Add(u);
                _context.SaveChanges();
                User newUser = _context.Users.SingleOrDefault(a => a.Email == user.Email);
                HttpContext.Session.SetInt32("id", newUser.UserId);
                return RedirectToAction("Dash", "Idea");
            }
            return View("Index");
        }
        public IActionResult Login(LoginViewModel user){
            // List<User> u = _context.Users.Where(a => a.Email == user.Email).ToList();
            if(ModelState.IsValid){

            User u = _context.Users.SingleOrDefault(a => a.Email == user.Email);
            if(u != null){
            if(user.Email != null && user.Password != null){
                var Hasher = new PasswordHasher<LoginViewModel>();
                if(0 != Hasher.VerifyHashedPassword(user, u.Password, user.Password)){
                    HttpContext.Session.SetInt32("id", u.UserId);
                    return RedirectToAction("Dash", "Idea");
                }
            }
            }
            }
            return View("Index");
        }
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
