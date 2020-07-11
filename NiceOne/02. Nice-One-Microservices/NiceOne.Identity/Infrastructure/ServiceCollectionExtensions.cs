namespace NiceOne.Identity.Infrastructure
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using NiceOne.Identity.Data;
    using NiceOne.Identity.Data.Entities;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUserStorage(
            this IServiceCollection services)
        {
            services
             .AddIdentity<User, IdentityRole>(opt =>
             {
                 opt.Password.RequiredLength = 7;
                 opt.Password.RequireDigit = false;
                 opt.Password.RequireUppercase = false;
                 opt.Password.RequireNonAlphanumeric = false;

                 opt.User.RequireUniqueEmail = true;
             })
                 .AddEntityFrameworkStores<NiceOneIdentityDbContext>();

            return services;
        }
    }
}
