using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ContactUsController : Controller
    {
        private IContactUsService _contactUsService;

        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(ContactUs contactUs)
        {
            var result = _contactUsService.Add(contactUs);

            if (result.Success)
            {
                ViewBag.result = result.Message;
                return RedirectToAction("Index");
            }

            ViewBag.error = "Mesaj gönderilemedi!";
            return RedirectToAction("Index");
        }
    }
}
