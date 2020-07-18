namespace NiceOne.Client
{
    using AutoMapper;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NiceOne.Client.Infrastructure;
    using NiceOne.Client.Services.Gateway;
    using NiceOne.Client.Services.Identity;
    using NiceOne.Client.Services.Location;
    using NiceOne.Client.Services.Place;
    using NiceOne.Infrastructure;
    using NiceOne.Place.Services.Categories;
    using NiceOne.Services.Identity;
    using Refit;
    using Services;

    public class Startup
    {
        public Startup(IConfiguration configuration) 
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var serviceEndpoints = this.Configuration
                .GetSection(nameof(ServiceEndpoints))
                .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

            services
                .AddAutoMapper(typeof(Startup))
                .AddTokenAuthentication(this.Configuration)
                .AddScoped<ICurrentTokenService, CurrentTokenService>()
                .AddTransient<JwtCookieAuthenticationMiddleware>()
                .AddControllersWithViews(options => options
                    .Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            services
                .AddRefitClient<IIdentityService>()
                .WithConfiguration(serviceEndpoints.Identity);

            services
                .AddRefitClient<IPlaceService>()
                .WithConfiguration(serviceEndpoints.Place);
            services
                .AddRefitClient<ICategoryService>()
                .WithConfiguration(serviceEndpoints.Place);

            services
                .AddRefitClient<ILocationService>()
                .WithConfiguration(serviceEndpoints.Location);

            services
                .AddRefitClient<IGatewayService>()
                .WithConfiguration(serviceEndpoints.Gateway);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage();
            }
            else
            {
                app
                    .UseExceptionHandler("/Home/Error")
                    .UseHsts();
            }

            app
                .UseStaticFiles()
                .UseRouting()
                .UseJwtCookieAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints
                    .MapDefaultControllerRoute());
        }
    }
}
