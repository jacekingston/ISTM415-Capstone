using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectPrototype.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPrototype.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OpenPage(string page)
        {
            switch (page)
            {
                case "Teams":
                    {
                        return View(new Team());
                    }
                case "Players":
                    {
                        return View(new Player());
                    }
                case "Games":
                    {
                        return View(new Game());
                    }
                case "Managers":
                    {
                        return View(new Manager());
                    }

            }
            return View();
        }
    }
}
