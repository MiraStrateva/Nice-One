namespace NiceOne.Location.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using NiceOne.Location.Data;
    using NiceOne.Location.Data.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public static class ApplicationBuilderExtensions
    {
        private static IEnumerable<Country> GetData()
            => new List<Country>
                {
                new Country() { Name = "Bulgaria",
                                Cities = new List<City>{
                                        new City() { Name = "Sofia" },
                                        new City() { Name = "Plovdiv" },
                                        new City() { Name = "Varna" },
                                        new City() { Name = "Burgas" }
                                }},
                new Country() { Name = "Italy",
                                Cities = new List<City>{
                                        new City() { Name = "Rome" },
                                        new City() { Name = "Milan" },
                                        new City() { Name = "Naples" },
                                        new City() { Name = "Florence" }
                                }},
                new Country() { Name = "Spain",
                                Cities = new List<City>{
                                        new City() { Name = "Barcelona" },
                                        new City() { Name = "Madrid" },
                                        new City() { Name = "Valencia" },
                                        new City() { Name = "Seville" }
                                }},
                new Country() { Name = "Greece",
                                Cities = new List<City>{
                                        new City() { Name = "Athens" },
                                        new City() { Name = "Rhodes" },
                                        new City() { Name = "Thessaloniki" },
                                        new City() { Name = "Corfu" }
                                }},
                new Country() { Name = "Other" }
                };

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            var db = serviceProvider.GetRequiredService<NiceOneLocationDbContext>();

            if (db.Countries.Any())
            {
                return app;
            }

            foreach (var country in GetData())
            {
                db.Countries.Add(country);
            }

            db.SaveChanges();

            return app;
        }
    }
}
