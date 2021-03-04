using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Http;
using WebUI.Models;
using FeedBacks = Business.Constants.Messages;

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
        public IActionResult Index(Entities.Concrete.ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                var result = _contactUsService.Add(contactUs);

                if (result.Success)
                {
                    TempData["ContactSendMessageResult"] = result.Message;
                    return View();
                }
            }
            
            TempData["ContactSendMessageResult"] = FeedBacks.MessagesCanNotAdded;
            return View(contactUs);
        }

        [HttpGet]
        public IActionResult Messages()
        {
            if (HttpContext.Session.GetString("isUserLogin") != "true" || HttpContext.Session.GetInt32("userRole") == 1)
            {
                TempData["userIsNotLogin"] = "Yönetim Paneline Erişebilmek İçin Lütfen Giriş Yapınız!";
                return RedirectToAction("Login","Admin");
            }

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
        public IActionResult DeleteMessage(int id)
        {
            if (HttpContext.Session.GetString("isUserLogin") != "true" || HttpContext.Session.GetInt32("userRole") == 1)
            {
                TempData["userIsNotLogin"] = "Yönetim Paneline Erişebilmek İçin Lütfen Giriş Yapınız!";
                return RedirectToAction("Login","Admin");
            }

            var message = _contactUsService.GetById(id);

            var result = _contactUsService.Delete(message.Data);
            if (result.Success)
            {
                TempData["messagesError"] = result.Message;
                return RedirectToAction("Messages");
            }

            TempData["messagesError"] = result.Message;
            return RedirectToAction("Messages");
        }
    }
}
