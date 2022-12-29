using Microsoft.AspNetCore.Identity;

namespace labwebprojeto.Data
{
    public class SeedRoles
    {
        public static void Seed(RoleManager<IdentityRole> roleManager)
        {
            if(roleManager.Roles.Any() == false) 
            {
                roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
                roleManager.CreateAsync(new IdentityRole("Func")).Wait();
                roleManager.CreateAsync(new IdentityRole("Client")).Wait();
            }
        }
    }
}
