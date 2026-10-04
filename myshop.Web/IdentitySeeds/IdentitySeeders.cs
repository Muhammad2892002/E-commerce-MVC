using Microsoft.AspNetCore.Identity;
using myshop.Domain.Models;

namespace myshop.Web.IdentitySeeds
{
    public class IdentitySeeders
    {
        private readonly IServiceProvider _serviceProvider;

        public static async  Task AddAllSeeds(IServiceProvider service) { 
        
         await SeedRolesAsync(service);
            await SeedUser(service);

        }
       

        public static async Task SeedRolesAsync(IServiceProvider serviceProvider) {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //var User = serviceProvider.GetRequiredService<UserManager<IdentityRole>>();

            List<string> AllRoles=new List<string>() { 
              "Admin",
              "Customer",
              "Seller",
             
            
            
            };
            foreach (var role in AllRoles) {
                if (!await roleManager.RoleExistsAsync(role)) { 
                   
                    await roleManager.CreateAsync(new IdentityRole(role));


                }
            
            
            
            }
        
        
        }


        public static async Task SeedUser(IServiceProvider service) { 
            var userManager=service.GetRequiredService<UserManager<ApplicationUser>>();
            var email="admin@example.com";
            if (await userManager.FindByEmailAsync(email) == null) {
                var admin = new ApplicationUser
                {
                    FullName = "Admin User",
                    Email = email,
                    UserName = email,
                    Address = "Jordan,Balqaa,Salt",
                    City = "Salt"

                };
                var result = await userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded) { 
                  await userManager.AddToRoleAsync(admin, "Admin");

                }



            }





        }
    }
}
