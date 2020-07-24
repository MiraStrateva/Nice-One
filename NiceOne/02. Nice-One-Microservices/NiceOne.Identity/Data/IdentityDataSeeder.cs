namespace NiceOne.Identity.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using NiceOne.Services;
    using Microsoft.AspNetCore.Identity;
    using NiceOne.Identity.Data.Entities;

    public class IdentityDataSeeder : IDataSeeder
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public IdentityDataSeeder(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void SeedData()
        {
            if (this.userManager.Users.Any())
            {
                return;
            }

            Task
                .Run(async () =>
                {
                    var adminRole = new IdentityRole(Constants.AdministratorRoleName);

                    await this.roleManager.CreateAsync(adminRole);

                    var adminUser = new User
                    {
                        UserName = "Miroslava_Strateva@abv.bg",
                        Email = "Miroslava_Strateva@abv.bg",
                        FirstName = "Miroslava",
                        LastName = "Strateva",
                        SecurityStamp = "RandomSecurityStamp"
                    };

                    await userManager.CreateAsync(adminUser, "Strateva79");

                    await userManager.AddToRoleAsync(adminUser, Constants.AdministratorRoleName);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
