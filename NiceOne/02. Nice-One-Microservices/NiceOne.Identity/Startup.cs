namespace NiceOne.Identity
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using NiceOne.Identity.Data;
    using NiceOne.Identity.Data.Entities;
    using NiceOne.Identity.Factory;
    using NiceOne.Identity.Infrastructure;
    using NiceOne.Identity.Services;
    using NiceOne.Infrastructure;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddWebService<NiceOneIdentityDbContext>(this.Configuration)
                .AddAutoMapper(typeof(Startup))
                .AddUserStorage()
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<ITokenGeneratorService, TokenGeneratorService>()
                .AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsFactory>();
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseWebService(env)
                .Initialize();
    }
}
