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

        [HttpGet]
        public IActionResult Create()
        {
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

            sale.Owner = 1;
            var result = _saleService.Add(sale);
            if (result.Success)
            {
                ViewBag.createSuccess = result.Message;
                return RedirectToAction("Index");
            }
            ViewBag.createSuccess = result.Message;
            return View();
        }

        public IActionResult Update(int id)
        {
            var sale = _saleService.GetById(id);
            if (sale.Data != null)
            {
                sale.Data.Description = "Test";
                var result = _saleService.Update(sale.Data);

                if (result.Success)
                {
                    ViewBag.updateResult = result.Message;
                    return View();
                }
            }

            ViewBag.updateResult = "Ürün Güncellenemedi!";
            return View();
        }

        public IActionResult Delete(int id)
        {
            var sale = _saleService.GetById(id);

            var result = _saleService.Delete(sale.Data);
            if (result.Success)
            {
                ViewBag.deleteResult = result.Message;
                return View("Index");
            }

            ViewBag.deleteResult = result.Message;
            return View("Index");
        }
    }
}
