using NiceOne.Client.Models.Place.Places;
using Refit;
using System.Threading.Tasks;

namespace NiceOne.Client.Services.Gateway
{
    public interface IGatewayService
    {
        [Get("/Place/Details/{id}")]
        Task<PlaceGetModel> Details(int id);
    }
}
