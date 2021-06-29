
using Microsoft.AspNetCore.Identity;

namespace SimpleSales.WebAdmin.Databases
{
    public class AppDbInitializer
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("maseric7@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "maseric7@gmail.com",
                    Email = "maseric7@gmail.com"
                };
                _ = userManager.CreateAsync(user, "Petrokimia@2021").Result;
            }
        }
    }
}
