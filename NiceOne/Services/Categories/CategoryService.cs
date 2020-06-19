using NiceOne.Data;
using NiceOne.Data.Entities;
using NiceOne.DTOs.Categories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiceOne.Services.Categories
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(NiceOneDbContext data)
            :base(data)
        {
        }

        public async Task<IEnumerable<CategoryGetModel>> GetAllOrderedByPlacesAsync()
        {
            var result = await this
                    .GetAllAsync(orderBy: c => c.Places.Count, ascending: false);
            return result.Select(c => new CategoryGetModel()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ImageUrl = c.ImageUrl,
                PlacesCount = c.Places.Count
            });
        }
    }
}
