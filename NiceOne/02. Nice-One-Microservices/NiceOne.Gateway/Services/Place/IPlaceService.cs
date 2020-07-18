namespace NiceOne.Gateway.Services.Place
{
    using NiceOne.Gateway.Models.Places;
    using Refit;
    using System.Threading.Tasks;

    public interface IPlaceService
    {
        [Get("/Place/Details/{id}")]
        Task<PlaceGetModel> GetPlace(int id);
    }
}
