namespace NiceOne.Place.Services.Categories
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using NiceOne.Place.Data;
    using NiceOne.Place.Data.Entities;
    using NiceOne.Place.Models.Categories;
    using NiceOne.Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoryService : BaseService<NiceOnePlaceDbContext, Category>, ICategoryService
    {
        private readonly IMapper mapper;
        public CategoryService(NiceOnePlaceDbContext data, IMapper mapper)
            : base(data)
            => this.mapper = mapper;

        public async Task<IEnumerable<CategoryGetModel>> GetAsync()
            => await this.mapper
                .ProjectTo<CategoryGetModel>(this.Data.Categories)
                .ToListAsync();

        public async Task<IEnumerable<CategoryGetModel>> GetAllOrderedByPlacesAsync()
            => await this.Data.Categories
                .OrderByDescending(c => c.Places.Count)
                .Select(c => new CategoryGetModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    PlacesCount = c.Places.Count
                })
                .ToArrayAsync();

        public string GetCategoryName(int id)
            => this.Data.Categories
                .FirstOrDefault(c => c.Id == id).Name;

        public async Task<CategoryGetModel> GetByIdAsync(int categoryId)
            => await this.mapper
                .ProjectTo<CategoryGetModel>(this.Data.Categories)
                .FirstOrDefaultAsync(c => c.Id == categoryId);

        public async Task DeleteAsync(int id)
        {
            var category = new Category { Id = id };
            await this.DeleteAsync(category);
        }
    }
}