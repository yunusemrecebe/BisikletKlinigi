using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class SalesController : Controller
    {
        private ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public IActionResult Index()
        {
            var result = _saleService.GetAll();
            ViewBag.result = result.Data;
            return View(result.Data);
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
    }
}
