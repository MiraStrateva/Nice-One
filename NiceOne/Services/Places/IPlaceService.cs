using NiceOne.DTOs.Places;
using System.Threading.Tasks;

namespace NiceOne.Services.Places
{
    public interface IPlaceService
    {
        Task CreateAsync(PlaceSetModel placeSetModel);
    }
}
