using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
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
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Sale sale)
        {
            var result = _saleService.Add(sale);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Details(int id)
        {
           var result = _saleService.GetById(id);

            if (result.Success)
            {
                ViewBag.id = id;
                return View();
            }
            
            return View();
        }
    }
}
