namespace NiceOne.Identity.Services
{
    using NiceOne.Identity.Data.Entities;

    public interface ITokenGeneratorService
    {
        string GenerateToken(User user);
    }
}
