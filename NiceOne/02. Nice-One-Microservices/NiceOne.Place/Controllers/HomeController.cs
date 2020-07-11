namespace NiceOne.Place.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using NiceOne.Controllers;
    using NiceOne.Place.DTOs.Categories;
    using NiceOne.Place.Models;
    using NiceOne.Place.Services.Categories;

    public class HomeController : ApiController
    {
        private readonly ICategoryService categoryService;

        public HomeController(ICategoryService service)
        {
            categoryService = service;
        }

        [Route(nameof(Index))]
        public async Task<IActionResult> Index()
        {
            List<CategoryGetModel> result = new List<CategoryGetModel>(await categoryService.GetAllOrderedByPlacesAsync());
            return View(result);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route(nameof(Error))]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
