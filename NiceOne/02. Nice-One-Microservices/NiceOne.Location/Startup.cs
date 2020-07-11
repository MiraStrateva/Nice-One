namespace NiceOne.Location
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NiceOne.Location.Infrastructure;
    using NiceOne.Location.Data;
    using NiceOne.Infrastructure;
    using AutoMapper;
    using NiceOne.Location.Services.Countries;
    using NiceOne.Location.Services.Cities;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddWebService<NiceOneLocationDbContext>(this.Configuration)
                .AddAutoMapper(typeof(Startup))
                .AddTransient<ICountryService, CountryService>()
                .AddTransient<ICityService, CityService>();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseWebService(env)
                .Initialize()
                .SeedData();
    }
}
