namespace NiceOne.Identity.Services
{
    using NiceOne.Identity.Data.Entities;
    using System.Collections.Generic;

    public interface ITokenGeneratorService
    {
        string GenerateToken(User user, IEnumerable<string> roles = null);
    }
}
