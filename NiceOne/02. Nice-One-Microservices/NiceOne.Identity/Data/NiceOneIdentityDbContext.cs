namespace NiceOne.Identity.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using NiceOne.Identity.Data.Entities;
    using System.Reflection;

    public class NiceOneIdentityDbContext : IdentityDbContext<User>
    {
        public NiceOneIdentityDbContext(DbContextOptions<NiceOneIdentityDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
