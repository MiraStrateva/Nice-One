using NiceOne.DTOs.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NiceOne.Services.Categories
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryGetModel>> GetAllOrderedByPlacesAsync();
    }
}
