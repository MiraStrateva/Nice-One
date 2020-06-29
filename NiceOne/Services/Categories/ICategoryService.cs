namespace NiceOne.Services.Categories
{
    using NiceOne.Data.Entities;
    using NiceOne.DTOs.Categories;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService : IBaseService<Category>
    {
        Task<IEnumerable<CategoryGetModel>> GetAsync();
        Task<IEnumerable<CategoryGetModel>> GetAllOrderedByPlacesAsync();
        string GetCategoryName(int categoryId);
        Task<CategoryGetModel> GetByIdAsync(int categoryId);
        Task DeleteAsync(int id);
    }
}
