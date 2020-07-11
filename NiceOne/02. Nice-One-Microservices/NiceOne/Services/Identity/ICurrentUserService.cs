using System.Reflection.Metadata;

namespace NiceOne.Services.Identity
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string UserRole { get; }
        string FirstName { get; }
    }
}
