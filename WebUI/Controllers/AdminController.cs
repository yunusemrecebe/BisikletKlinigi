using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Concrete.EntityFramework;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
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
            EfUserDal efUserDal = new EfUserDal();
            user.Role = 1;
            efUserDal.Add(user);
            return View();
        }
    }
}
