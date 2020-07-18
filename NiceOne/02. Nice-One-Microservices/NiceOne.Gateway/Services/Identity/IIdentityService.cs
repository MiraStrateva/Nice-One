namespace NiceOne.Gateway.Services.Identity
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using NiceOne.Gateway.Models.Identity;
    using Refit;
    
    public interface IIdentityService
    {
        [Get("/Account/Names")]
        Task<IEnumerable<UserGetModel>> UserNames(
            [Query(CollectionFormat.Multi)] IEnumerable<string> ids);
    }
}
