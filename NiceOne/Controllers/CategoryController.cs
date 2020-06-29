namespace NiceOne.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using NiceOne.Data.Entities;
    using NiceOne.DTOs.Categories;
    using NiceOne.Services.Categories;
    using System.IO;
    using System.Threading.Tasks;

    public class CategoryController : Controller
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

        [Authorize]
        public async Task<IActionResult> All()
        {
            var categories = await categoryService.GetAsync();
            return View("List", categories);
        }

        [Authorize]
        public async Task<IActionResult> Details(int Id)
        {
            var category = await categoryService.GetByIdAsync(Id);
            return View(category);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CategorySetModel categorySetModel)
        {
            if (this.ModelState.IsValid)
            {
                if (categorySetModel.FormFile != null)
                {
                    string fileName = $"\\images\\{Path.GetRandomFileName()}.jpg";
                    string filePath = string.Concat(env.WebRootPath, fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        categorySetModel.FormFile.CopyTo(stream);
                        categorySetModel.ImageUrl = fileName;
                    }
                }

                var category = mapper.Map<Category>(categorySetModel);
                await this.categoryService.CreateAsync(category);
                return RedirectToAction(nameof(CategoryController.All));
            }

            return View(categorySetModel);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int Id)
        {
            var category = await categoryService.GetByIdAsync(Id);

            return View(mapper.Map<CategorySetModel>(category));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(CategorySetModel categorySetModel)
        {
            if (this.ModelState.IsValid)
            {
                var category = await this.categoryService.FindAsync(categorySetModel.Id);

                category.Name = categorySetModel.Name;
                category.Description = categorySetModel.Description;
                if (categorySetModel.FormFile != null)
                {
                    string fileName = $"\\images\\{Path.GetRandomFileName()}.jpg";
                    string filePath = string.Concat(env.WebRootPath, fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        categorySetModel.FormFile.CopyTo(stream);
                        category.ImageUrl = fileName;
                    }
                }

                await this.categoryService.SaveAsync(category);
                return RedirectToAction(nameof(CategoryController.All));
            }

            return View(categorySetModel);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return this.View(id);
        }

        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            await this.categoryService.DeleteAsync(Id);
            return RedirectToAction(nameof(CategoryController.All));
        }
    }
}
