namespace NiceOne.Location.Data
{
    using Microsoft.EntityFrameworkCore;
    using NiceOne.Location.Data.Entities;
    using System.Reflection;

    public class NiceOneLocationDbContext : DbContext
    {
        public NiceOneLocationDbContext(DbContextOptions<NiceOneLocationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}