namespace NiceOne.Location.Data
{
    using Microsoft.EntityFrameworkCore;
    using NiceOne.Data;
    using NiceOne.Location.Data.Entities;
    using System.Reflection;

    public class NiceOneLocationDbContext : MessageDbContext
    {
        public NiceOneLocationDbContext(DbContextOptions<NiceOneLocationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();
    }
}