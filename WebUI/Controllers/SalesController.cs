﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Details(int id)
        {
            var result = _saleService.GetById(id);
           
            if (result.Success)
            {
                ViewBag.Description = result.Data.Description;
                return View();
            }
               
            ViewBag.Description = "Ürün bulunamadı!";
            return View();
        }
    }
}
