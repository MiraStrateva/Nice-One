namespace NiceOne.Place.Data
{
    using Microsoft.EntityFrameworkCore;

    using NiceOne.Place.Data.Entities;

    using System.Reflection;

    public class NiceOnePlaceDbContext : DbContext
    {
        public NiceOnePlaceDbContext(DbContextOptions<NiceOnePlaceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
