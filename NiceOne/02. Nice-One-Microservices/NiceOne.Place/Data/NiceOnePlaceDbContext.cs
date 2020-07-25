namespace NiceOne.Place.Data
{
    using System.Reflection;
    
    using Microsoft.EntityFrameworkCore;
    using NiceOne.Data;
    using NiceOne.Place.Data.Entities;

    public class NiceOnePlaceDbContext : MessageDbContext
    {
        public NiceOnePlaceDbContext(DbContextOptions<NiceOnePlaceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();
    }
}
