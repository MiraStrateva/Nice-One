namespace NiceOne.Place.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using NiceOne.Controllers;
    using NiceOne.Infrastructure;
    using NiceOne.Place.Data.Entities;
    using NiceOne.Place.Models.Categories;
    using NiceOne.Place.Services.Categories;
    using System.IO;
    using System.Threading.Tasks;

    public class CategoryController : ApiController
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

        public async Task<IActionResult> Index()
            => Ok(await categoryService.GetAllOrderedByPlacesAsync());
        
        [Route(nameof(All))]
        public async Task<IActionResult> All()
            => Ok(await categoryService.GetAsync());
                
        [Route(nameof(Details) + PathSeparator + Id)]
        public async Task<IActionResult> Details(int Id)
            => Ok(await categoryService.GetByIdAsync(Id));

        [Route(nameof(CategoryName) + PathSeparator + Id)]
        public IActionResult CategoryName(int id)
            => Ok(categoryService.GetCategoryName(id));

        [HttpPost]
        //[AuthorizeAdministrator]
        [Route(nameof(Create))]
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
                return Ok();
            }

            return BadRequest("Model is not valid");
        }

        [HttpPost]
        //[AuthorizeAdministrator]
        [Route(nameof(Edit) + PathSeparator + Id)]
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
                return Ok();
            }

            return BadRequest("Model is not valid"); 
        }

        //[AuthorizeAdministrator]
        [Route(nameof(ConfirmDelete) + PathSeparator + Id)]
        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            await this.categoryService.DeleteAsync(Id);
            return Ok();
        }
    }
}
