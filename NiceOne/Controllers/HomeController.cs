namespace NiceOne.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using NiceOne.DTOs.Categories;
    using NiceOne.Models;
    using NiceOne.Services.Categories;

    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;

        public HomeController(ICategoryService service)
        {
            categoryService = service;
        }

        public async Task<IActionResult> Index()
        {
            List<CategoryGetModel> result = new List<CategoryGetModel>(await categoryService.GetAllOrderedByPlacesAsync());
            return View(result);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
