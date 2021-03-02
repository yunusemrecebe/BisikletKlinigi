using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
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

        public IActionResult Index(int? page)
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
    }
}
