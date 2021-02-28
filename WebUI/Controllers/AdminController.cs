using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string mail, string password)
        {
            var result = _userService.Login(mail,password);
            if (result.Success)
            {
                ViewBag.loginResult = result.Message;
                return RedirectToAction("Index");
            }

            ViewBag.loginResult = result.Message;
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            user.Role = 1;
            var result = _userService.Add(user);
            if (result.Success)
            {
                ViewBag.registerResult = result.Message;
                return RedirectToAction("Login");
            }

            ViewBag.registerResult = result.Message;
            return View();
        }
    }
}
