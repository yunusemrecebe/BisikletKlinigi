using System;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using WebUI.Models;
using Microsoft.AspNetCore.Http;
using X.PagedList;

namespace WebUI.Controllers
{
    public class SalesController : Controller
    {
        private ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public IActionResult Index(int? page, string q)
        {
            var result = _saleService.GetAll();
            if (result.Success)
            {
                ViewBag.result = result.Data;
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                var onePageOfProducts = result.Data.Where(x => x.Usage == q).ToPagedList(pageNumber, pageSize);
                ViewBag.OnePageOfProducts = onePageOfProducts;
                return View();
            }

            ViewBag.saleErrorMessage = result.Message;
            return View();
        }

        public IActionResult Details(int id)
        {
            var result = _saleService.GetById(id);
           
            if (result.Success)
            {
                ViewBag.detailsResult = result.Success;
                return View(result.Data);
            }

            ViewBag.detailsMessage = result.Message;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("isUserLogin") != "true" || HttpContext.Session.GetInt32("userRole") == 1)
            {
                TempData["userIsNotLogin"] = "Yönetim Paneline Erişebilmek İçin Lütfen Giriş Yapınız!";
                return RedirectToAction("Login","Admin");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Sale sale, IFormFile Image)
        {
            if (HttpContext.Session.GetString("isUserLogin") != "true" || HttpContext.Session.GetInt32("userRole") == 1)
            {
                TempData["userIsNotLogin"] = "Yönetim Paneline Erişebilmek İçin Lütfen Giriş Yapınız!";
                return RedirectToAction("Login","Admin");
            }

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
                }
                else
                {
                    ViewBag.updateResult = null;
                    ViewBag.updateMessage = "Yalnızca 'JPG' veya 'PNG' formatındaki görseller yüklenebilir!";
                    return View();
                }
            }

            sale.Owner = Convert.ToInt32(HttpContext.Session.GetInt32("userId"));
            var result = _saleService.Add(sale);
            if (result.Success)
            {
                TempData["createSuccess"] = result.Message;
                return RedirectToAction("Index","Admin");
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
                return RedirectToAction("Login","Admin");
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
            if (HttpContext.Session.GetString("isUserLogin") != "true" || HttpContext.Session.GetInt32("userRole") == 1)
            {
                TempData["userIsNotLogin"] = "Yönetim Paneline Erişebilmek İçin Lütfen Giriş Yapınız!";
                return RedirectToAction("Login","Admin");
            }

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
                TempData["updateSuccess"] = result.Message;
                return RedirectToAction("Index","Admin");
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
                return RedirectToAction("Login","Admin");
            }

            var sale = _saleService.GetById(id);

            var result = _saleService.Delete(sale.Data);
            if (result.Success)
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\uploads", sale.Data.Image));
                TempData["deleteResult"] = result.Message;
                return RedirectToAction("Index","Admin");
            }

            TempData["deleteResult"] = result.Message;
            return RedirectToAction("Index","Admin");
        }
    }
}
