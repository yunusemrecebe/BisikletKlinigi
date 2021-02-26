using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;

namespace WebUI.Controllers
{
    public class SalesController : Controller
    {
        private ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
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
