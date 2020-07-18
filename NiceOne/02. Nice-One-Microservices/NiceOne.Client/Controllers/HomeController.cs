namespace NiceOne.Client.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using NiceOne.Client.Models;
    using NiceOne.Client.Services.Place;
    using NiceOne.Place.Services.Categories;

    public class HomeController : BaseController
    {
        private readonly ICategoryService categoryService;

        public HomeController(ICategoryService service)
        {
            categoryService = service;
        }

        public async Task<IActionResult> Index()
            => View(await categoryService.GetAllOrderedByPlacesAsync());
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route(nameof(Error))]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
