using System;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Business.Abstract;
using WebUI.Models;
using Microsoft.AspNetCore.Http;
using X.PagedList;
using FeedBacks = Business.Constants.Messages;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IUserService _userService;
        private ISaleService _saleService;
        private IContactUsService _contactUsService;

        public AdminController(IUserService userService, ISaleService saleService, IContactUsService contactUsService)
        {
            _userService = userService;
            _saleService = saleService;
            _contactUsService = contactUsService;
        }

        public IActionResult Index(int? page)
        {
            if (HttpContext.Session.GetString("isUserLogin") == null)
            {
                HttpContext.Session.SetString("isUserLogin", "false");
            }

            if (HttpContext.Session.GetString("isUserLogin") == "true" && HttpContext.Session.GetInt32("userRole") == 2)
            {
                var result = _saleService.GetAll();
                if (result.Success)
                {
                    ViewBag.result = result.Data;
                    int pageSize = 8;
                    int pageNumber = (page ?? 1);
                    var onePageOfProducts = result.Data.ToPagedList(pageNumber, pageSize);
                    ViewBag.OnePageOfProducts = onePageOfProducts;
                    return View();
                }

                ViewBag.indexResult = result.Message;
                return View();
            }

            TempData["userIsNotLogin"] = "Yönetim Paneline Erişebilmek İçin Lütfen Giriş Yapınız!";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Messages()
        {
            var result = _contactUsService.GetAll();
            if (result.Success)
            {
                ViewBag.messageStatus = result.Success;
                return View(result.Data);
            }

            ViewBag.messageStatus = result.Success;
            ViewBag.messagesError = result.Message;
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
            var result = _userService.Login(mail, password);
            if (result.Success)
            {
                //Save the user login status to true
                HttpContext.Session.SetString("isUserLogin", "true");

                //Save the userId
                HttpContext.Session.SetInt32("userId", result.Data.Id);

                //Save the userRole
                HttpContext.Session.SetInt32("userRole", result.Data.Role);

                return RedirectToAction("Index");
            }

            //if User can not login
            ViewBag.loginResult = result.Message;
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
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
        public IActionResult UserManagement()
        {
            if (HttpContext.Session.GetString("isUserLogin") != "true" || HttpContext.Session.GetInt32("userRole") == 1)
            {
                TempData["userIsNotLogin"] = "Yönetim Paneline Erişebilmek İçin Lütfen Giriş Yapınız!";
                return RedirectToAction("Login");
            }

            var user = Convert.ToInt32(HttpContext.Session.GetInt32("userId"));
            var result = _userService.GetById(user);
            if (result.Success)
            {
                ViewBag.userManagementResult = result.Success;
                return View(result.Data);
            }
            ViewBag.userManagementResult = result.Success;
            ViewBag.userManagementMessage = result.Message;
            return View();
        }

        [HttpPost]
        public IActionResult UserManagement(User user)
        {
            var result = _userService.Update(user);
            if (result.Success)
            {
                //ViewBag.userManagementResult = result.Success;
                //ViewBag.userManagementMessage = result.Message;
                return RedirectToAction("UserManagement", user.Id);
            }
            ViewBag.userManagementResult = true;
            ViewBag.userManagementMessage = result.Message;
            return View();
        }

        [HttpGet]
        public IActionResult ShowAllUsers()
        {

            if (HttpContext.Session.GetString("isUserLogin") != "true" || HttpContext.Session.GetInt32("userRole") == 1)
            {
                TempData["userIsNotLogin"] = "Yönetim Paneline Erişebilmek İçin Lütfen Giriş Yapınız!";
                return RedirectToAction("Login");
            }

            var result = _userService.GetAll();
            if (result.Success)
            {
                ViewBag.userId = Convert.ToInt32(HttpContext.Session.GetInt32("userId"));
                ViewBag.showUsersResult = result.Success;
                return View(result.Data);
            }

            ViewBag.showUserErrorMessage = result.Message;
            return View();
        }

        [HttpGet]
        public IActionResult UserActivate(int id)
        {
            var user = _userService.GetById(id);
            user.Data.Role = 2;
            var result = _userService.Update(user.Data);
            if (result.Success)
            {
                TempData["userManagementMessage"] = FeedBacks.UserisActivated;
                return RedirectToAction("ShowAllUsers");
            }

            TempData["userManagementMessage"] = result.Message;
            return RedirectToAction("ShowAllUsers");
        }

        [HttpGet]
        public IActionResult UserPassive(int id)
        {
            var user = _userService.GetById(id);
            user.Data.Role = 1;
            var result = _userService.Update(user.Data);
            if (result.Success)
            {
                TempData["userManagementMessage"] = FeedBacks.UserisInactivated;
                return RedirectToAction("ShowAllUsers");
            }

            TempData["userManagementMessage"] = result.Message;
            return RedirectToAction("ShowAllUsers");
        }

        [HttpGet]
        public IActionResult UserDelete(int id)
        {
            var user = _userService.GetById(id);

            var result = _userService.Delete(user.Data);
            if (result.Success)
            {
                TempData["userManagementMessage"] = result.Message;
                return RedirectToAction("ShowAllUsers");
            }

            TempData["userManagementMessage"] = result.Message;
            return RedirectToAction("ShowAllUsers");
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("isUserLogin") != "true" || HttpContext.Session.GetInt32("userRole") == 1)
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
            if (HttpContext.Session.GetString("isUserLogin") != "true" || HttpContext.Session.GetInt32("userRole") == 1)
            {
                TempData["userIsNotLogin"] = "Yönetim Paneline Erişebilmek İçin Lütfen Giriş Yapınız!";
                return RedirectToAction("Login");
            }

            var sale = _saleService.GetById(id);
            if (sale != null)
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
                bool extensionIsChecked = false;
                var extension = Path.GetExtension(Image.FileName);
                if (extension == ".jpg" || extension == ".png")
                {
                    extensionIsChecked = true;
                }
                if (extensionIsChecked)
                {
                    var fileName = string.Format($"bisikletKlinigi_{Guid.NewGuid()}{extension}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\uploads", fileName);
                    sale.Image = fileName;

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\uploads", TempData["oldImage"].ToString()));
                }
                else
                {
                    ViewBag.updateResult = null;
                    ViewBag.updateMessage = "Yalnızca 'JPG' veya 'PNG' formatındaki görseller yüklenebilir!";
                    return View();
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
            if (HttpContext.Session.GetString("isUserLogin") != "true" || HttpContext.Session.GetInt32("userRole") == 1)
            {
                TempData["userIsNotLogin"] = "Yönetim Paneline Erişebilmek İçin Lütfen Giriş Yapınız!";
                return RedirectToAction("Login");
            }

            var sale = _saleService.GetById(id);

            var result = _saleService.Delete(sale.Data);
            if (result.Success)
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\uploads", sale.Data.Image));
                ViewBag.deleteResult = result.Message;
                return RedirectToAction("Index");
            }

            TempData["deleteResult"] = result.Message;
            return RedirectToAction("Index");
        }
    }
}
