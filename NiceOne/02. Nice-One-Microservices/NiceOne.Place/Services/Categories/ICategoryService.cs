namespace NiceOne.Place.Services.Categories
{
    using NiceOne.Place.Data;
    using NiceOne.Place.Data.Entities;
    using NiceOne.Place.Models.Categories;
    using NiceOne.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService : IBaseService<NiceOnePlaceDbContext, Category>
    {
        Task<IEnumerable<CategoryGetModel>> GetAsync();
        Task<IEnumerable<CategoryGetModel>> GetAllOrderedByPlacesAsync();
        string GetCategoryName(int id);
        Task<CategoryGetModel> GetByIdAsync(int categoryId);
        Task DeleteAsync(int id);
    }
}
