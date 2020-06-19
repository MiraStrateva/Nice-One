using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NiceOne.DTOs.Categories;
using NiceOne.Models;
using NiceOne.Services.Categories;

namespace NiceOne.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, ICategoryService service)
        {
            _logger = logger;
            _categoryService = service;
        }

        public IActionResult Index()
        {
            List<CategoryGetModel> result = new List<CategoryGetModel>(_categoryService.GetAllOrderedByPlacesAsync().Result);
            return View(result);
        }
        
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
