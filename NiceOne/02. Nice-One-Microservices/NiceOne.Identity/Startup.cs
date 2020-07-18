namespace NiceOne.Identity
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using NiceOne.Identity.Data;
    using NiceOne.Identity.Infrastructure;
    using NiceOne.Identity.Services;
    using NiceOne.Infrastructure;
    using NiceOne.Services;

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
                .AddTransient<IDataSeeder, IdentityDataSeeder>()
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<ITokenGeneratorService, TokenGeneratorService>();
                //.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsFactory>();
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseWebService(env)
                .Initialize();
    }
}
