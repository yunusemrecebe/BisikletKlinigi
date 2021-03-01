using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using WebUI.Models;
using Microsoft.AspNetCore.Http;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IUserService _userService;
        private ISaleService _saleService;

        public AdminController(IUserService userService, ISaleService saleService)
        {
            _userService = userService;
            _saleService = saleService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("isUserLogin") == null)
            {
                HttpContext.Session.SetString("isUserLogin", "false");
            }

            if (HttpContext.Session.GetString("isUserLogin") == "true")
            {
                var result = _saleService.GetAll();
                if (result.Success)
                {
                    ViewBag.indexResult = result.Message;
                    return View(result.Data);
                }

                ViewBag.indexResult = result.Message;
                return View();
            }
            
            TempData["userIsNotLogin"] = "Yönetim Paneline Erişebilmek İçin Lütfen Giriş Yapınız!";
            return RedirectToAction("Login");
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
                //Save the user login status to true
                HttpContext.Session.SetString("isUserLogin", "true");

                //Save the userId
                HttpContext.Session.SetInt32("userId", result.Data.Id);

                return RedirectToAction("Index");
            }

            //if User can not login
            ViewBag.loginResult = result.Message;
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("isUserLogin") == "true")
            {
                return RedirectToAction("Index");
            }

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

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("isUserLogin") != "true")
            {
                TempData["userIsNotLogin"] = "Yönetim Paneline Erişebilmek İçin Lütfen Giriş Yapınız!";
                return RedirectToAction("Login");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Sale sale, IFormFile Image)
        {
            if (Image != null)
            {
                var extension = Path.GetExtension(Image.FileName);
                var fileName = string.Format($"bisikletKlinigi{Guid.NewGuid()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\uploads", fileName);
                sale.Image = fileName;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }
            }

            sale.Owner = Convert.ToInt32(HttpContext.Session.GetInt32("userId"));
            var result = _saleService.Add(sale);
            if (result.Success)
            {
                ViewBag.createSuccess = result.Message;
                return RedirectToAction("Index");
            }
            ViewBag.createSuccess = result.Message;
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("isUserLogin") != "true")
            {
                TempData["userIsNotLogin"] = "Yönetim Paneline Erişebilmek İçin Lütfen Giriş Yapınız!";
                return RedirectToAction("Login");
            }

            var sale = _saleService.GetById(id);
            if (sale!=null)
            {
                if (sale.Success)
                {
                    ViewBag.updateResult = sale.Success;
                    TempData["oldImage"] = sale.Data.Image;
                    return View(sale.Data);
                }
            }

            ViewBag.updateResult = null;
            ViewBag.updateMessage = sale.Message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Sale sale, IFormFile? Image)
        {
            if (Image != null)
            {
                var extension = Path.GetExtension(Image.FileName);
                var fileName = string.Format($"bisikletKlinigi{Guid.NewGuid()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\uploads", fileName);
                sale.Image = fileName;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }
            }
            else
            {
                sale.Image = TempData["oldImage"].ToString();
            }

            sale.Owner = Convert.ToInt32(HttpContext.Session.GetInt32("userId"));
            var result = _saleService.Update(sale);
            if (result.Success)
            {
                ViewBag.updateResult = result.Message;
                return RedirectToAction("Index");
            }

            ViewBag.updateResult = null;
            ViewBag.updateMessage = result.Message;
            return View();
        }

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("isUserLogin") != "true")
            {
                TempData["userIsNotLogin"] = "Yönetim Paneline Erişebilmek İçin Lütfen Giriş Yapınız!";
                return RedirectToAction("Login");
            }

            var sale = _saleService.GetById(id);

            var result = _saleService.Delete(sale.Data);
            if (result.Success)
            {
                ViewBag.deleteResult = result.Message;
                return RedirectToAction("Index");
            }

            TempData["deleteResult"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}
