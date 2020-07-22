namespace NiceOne.Place
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using NiceOne.Infrastructure;
    using NiceOne.Place.Data;
    using NiceOne.Place.Infrastructure;
    using NiceOne.Place.Messages;
    using NiceOne.Place.Services.Categories;
    using NiceOne.Place.Services.Feedbacks;
    using NiceOne.Place.Services.Places;
    using System;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddWebService<NiceOnePlaceDbContext>(this.Configuration)
                .AddAutoMapper(typeof(Startup))
                .AddTransient<ICategoryService, CategoryService>()
                .AddTransient<IPlaceService, PlaceService>()
                .AddTransient<IFeedbackService, FeedbackService>()
                .AddMessaging(new Type[] { typeof(CountryUpdatedConsumer), typeof(CityUpdatedConsumer)});

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
            => app
                .UseWebService(env)
                .Initialize()
                .SeedData();
    }
}
