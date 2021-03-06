﻿namespace NiceOne.Services.Categories
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using NiceOne.Data;
    using NiceOne.Data.Entities;
    using NiceOne.DTOs.Categories;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly IMapper mapper;
        public CategoryService(NiceOneDbContext data, IMapper mapper)
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

        public string GetCategoryName(int categoryId)
            => this.Data.Categories
                .FirstOrDefault(c => c.Id == categoryId).Name;

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