using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NiceOne.Data.Entities;
using System.Reflection;

namespace NiceOne.Data
{
    public class NiceOneDbContext : IdentityDbContext<User>
    {
        public NiceOneDbContext(DbContextOptions<NiceOneDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Eat", Description = "Favorite places to eat: restaurants, fast food places, pubs offering some delicious snacks, gourme shops, etc.", ImageUrl = "https://i.pinimg.com/564x/96/31/72/963172b7bc598ee3f293bad7b80efcb8.jpg" },
                new Category() { Id = 2, Name = "Have fun", Description = "All places where fun and laugh are!", ImageUrl = "https://pluspng.com/img-png/children-having-fun-at-school-png-kids-having-fun-at-school-png-pluspng-com-720-kids-having-fun-720.png" },
                new Category() { Id = 3, Name = "Learn", Description = "I really learned something here!", ImageUrl = "https://www.pngkey.com/png/full/332-3321371_learning-clipart-discovery-animations-png-discovery-learning.png" },
                new Category() { Id = 4, Name = "Have a drink", Description = "Search where to have a drink or two!", ImageUrl = "https://i1.wp.com/blog.hellofresh.co.uk/wp-content/uploads/2015/12/Xmas_Cocktails_2015_DEFINITIV1.png?resize=800%2C500" },
                new Category() { Id = 5, Name = "Do your hair", Description = "Best places to improve your look!", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcS5Y4eCLP8GJvkqCiPflhJzbXwIqnPDZotbang-O3FaE0_l3tVL&usqp=CAU" },
                new Category() { Id = 6, Name = "Relax", Description = "Perfect place to relax!", ImageUrl = "https://pngriver.com/wp-content/uploads/2018/04/Download-Relax-Transparent-PNG.png" });


            builder.Entity<Country>().HasData(
                new Country() { Id = 1, Name = "Bulgaria" },
                new Country() { Id = 2, Name = "Italy" },
                new Country() { Id = 3, Name = "Spain" },
                new Country() { Id = 4, Name = "Greece" },
                new Country() { Id = 5, Name = "Other" });

            builder.Entity<City>().HasData(
                new City() { Id = 1, CountryId = 1, Name = "Sofia" },
                new City() { Id = 2, CountryId = 1, Name = "Plovdiv" },
                new City() { Id = 3, CountryId = 1, Name = "Varna" },
                new City() { Id = 4, CountryId = 1, Name = "Burgas" },
                new City() { Id = 5, CountryId = 2, Name = "Rome" },
                new City() { Id = 6, CountryId = 2, Name = "Milan" },
                new City() { Id = 7, CountryId = 2, Name = "Naples" },
                new City() { Id = 8, CountryId = 2, Name = "Florence" },
                new City() { Id = 9, CountryId = 3, Name = "Barcelona" },
                new City() { Id = 10, CountryId = 3, Name = "Madrid" },
                new City() { Id = 11, CountryId = 3, Name = "Valencia" },
                new City() { Id = 12, CountryId = 3, Name = "Seville" },
                new City() { Id = 13, CountryId = 4, Name = "Athens" },
                new City() { Id = 14, CountryId = 4, Name = "Rhodes" },
                new City() { Id = 15, CountryId = 4, Name = "Thessaloniki" },
                new City() { Id = 16, CountryId = 4, Name = "Corfu" });

            base.OnModelCreating(builder);
        }
    }
}
