namespace NiceOne
{
    using AutoMapper;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using NiceOne.Data;
    using NiceOne.Data.Entities;
    using NiceOne.Factory;
    using NiceOne.Services.Categories;
    using NiceOne.Services.Cities;
    using NiceOne.Services.Countries;
    using NiceOne.Services.Feedbacks;
    using NiceOne.Services.Identity;
    using NiceOne.Services.Places;

    using System;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<NiceOneDbContext>
                (item => item.UseSqlServer(Configuration.GetConnectionString("myconn")));
            
            services.AddHttpContextAccessor();
            services.AddIdentity<User, IdentityRole>(opt =>
                {
                    opt.Password.RequiredLength = 7;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireNonAlphanumeric = false;

                    opt.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<NiceOneDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                    opt.TokenLifespan = TimeSpan.FromHours(2));

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IPlaceService, PlaceService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IFeedbackService, FeedbackService>();
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsFactory>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
