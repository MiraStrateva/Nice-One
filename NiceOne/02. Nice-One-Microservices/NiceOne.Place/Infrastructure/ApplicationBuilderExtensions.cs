namespace NiceOne.Place.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using NiceOne.Place.Data;
    using NiceOne.Place.Data.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public static class ApplicationBuilderExtensions
    {
        private static IEnumerable<Category> GetData()
            => new List<Category>
                {
                    new Category() { Name = "Eat", Description = "Favorite places to eat: restaurants, fast food places, pubs offering some delicious snacks, gourme shops, etc.", ImageUrl = "https://i.pinimg.com/564x/96/31/72/963172b7bc598ee3f293bad7b80efcb8.jpg" },
                    new Category() { Name = "Have fun", Description = "All places where fun and laugh are!", ImageUrl = "https://pluspng.com/img-png/children-having-fun-at-school-png-kids-having-fun-at-school-png-pluspng-com-720-kids-having-fun-720.png" },
                    new Category() { Name = "Learn", Description = "I really learned something here!", ImageUrl = "https://www.pngkey.com/png/full/332-3321371_learning-clipart-discovery-animations-png-discovery-learning.png" },
                    new Category() { Name = "Have a drink", Description = "Search where to have a drink or two!", ImageUrl = "https://i1.wp.com/blog.hellofresh.co.uk/wp-content/uploads/2015/12/Xmas_Cocktails_2015_DEFINITIV1.png?resize=800%2C500" },
                    new Category() { Name = "Do your hair", Description = "Best places to improve your look!", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcS5Y4eCLP8GJvkqCiPflhJzbXwIqnPDZotbang-O3FaE0_l3tVL&usqp=CAU" },
                    new Category() { Name = "Relax", Description = "Perfect place to relax!", ImageUrl = "https://pngriver.com/wp-content/uploads/2018/04/Download-Relax-Transparent-PNG.png" }
                };

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            var db = serviceProvider.GetRequiredService<NiceOnePlaceDbContext>();

            if (db.Categories.Any())
            {
                return app;
            }

            foreach (var category in GetData())
            {
                db.Categories.Add(category);
            }

            db.SaveChanges();

            return app;
        }
    }
}
