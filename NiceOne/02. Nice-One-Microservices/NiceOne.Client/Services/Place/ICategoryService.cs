namespace NiceOne.Place.Services.Categories
{
    using NiceOne.Client.Models.Place.Categories;
    using NiceOne.Services;
    using Refit;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService 
    {
        [Get("/Category")]
        Task<IEnumerable<CategoryGetModel>> GetAllOrderedByPlacesAsync();

        [Get("/Category/All")]
        Task<IEnumerable<CategoryGetModel>> GetCategories();

        [Get("/Category/CategoryName/{id}")]
        Task<string> GetCategoryName(int id);

        [Get("/Categoty/Details/{id}")]
        Task<CategoryGetModel> GetCategory(int id);

        [Post("/Category/Create")]
        Task Create([Body] CategorySetModel category);

        [Post("/Category/Edit/{id}")]
        Task Edit(int id, [Body] CategorySetModel category);

        [Get("/Category/ConfirmDelete/{id}")]
        Task Delete(int id);
    }
}
