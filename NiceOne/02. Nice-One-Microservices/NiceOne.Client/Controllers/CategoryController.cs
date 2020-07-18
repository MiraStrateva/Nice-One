namespace NiceOne.Client.Controllers
{
    using AutoMapper;

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    using NiceOne.Infrastructure;
    using NiceOne.Client.Models.Place.Categories;
    using NiceOne.Place.Services.Categories;

    [AuthorizeAdministrator]
    public class CategoryController : BaseController
    {
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;
        private readonly IWebHostEnvironment env;
        public CategoryController(ICategoryService service, IMapper mapper, IWebHostEnvironment env)
        {
            categoryService = service;
            this.mapper = mapper;
            this.env = env;
        }

        public async Task<IActionResult> All()
            => Ok(View("List", await categoryService.GetCategories()));

        public async Task<IActionResult> Details(int Id)
            => Ok(View(await categoryService.GetCategory(Id)));
        
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategorySetModel input)
            => await this.Handle(
                async () => await this.categoryService.Create(input),
                success: RedirectToAction(nameof(CategoryController.All)),
                failure: View(input));

        public async Task<IActionResult> Edit(int Id)
            => View(mapper.Map<CategorySetModel>(await categoryService.GetCategory(Id)));

        [HttpPost]
        public async Task<IActionResult> Edit(CategorySetModel input)
            => await this.Handle(
                async () => await this.categoryService.Edit(input.Id, input),
                success: RedirectToAction(nameof(CategoryController.All)),
                failure: View(input));

        public IActionResult Delete(int id)
        {
            return this.View(id);
        }

        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            await this.categoryService.Delete(Id);
            return RedirectToAction(nameof(CategoryController.All));
        }
    }
}
