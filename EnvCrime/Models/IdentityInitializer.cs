using Microsoft.AspNetCore.Identity;

namespace EnvCrime.Models
{
    public class IdentityInitializer
    {
        public static async Task EnsurePopulated(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await CreateRoles(roleManager);
            await CreateUsers(userManager);
        }

        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                await roleManager.CreateAsync(new IdentityRole("Administrator"));
            }
            if (!await roleManager.RoleExistsAsync("Investigator"))
            {
                await roleManager.CreateAsync(new IdentityRole("Investigator"));
            }
            if (!await roleManager.RoleExistsAsync("Coordinator"))
            {
                await roleManager.CreateAsync(new IdentityRole("Coordinator"));
            }
            if (!await roleManager.RoleExistsAsync("Manager"))
            {
                await roleManager.CreateAsync(new IdentityRole("Manager"));
            }
        }

        private static async Task CreateUsers(UserManager<IdentityUser> userManager)
        {
            IdentityUser E000 = new IdentityUser("E000");
            IdentityUser E001 = new IdentityUser("E001");
            IdentityUser E100 = new IdentityUser("E100");
            IdentityUser E101 = new IdentityUser("E101");
            IdentityUser E102 = new IdentityUser("E102");
            IdentityUser E103 = new IdentityUser("E103");
            IdentityUser E200 = new IdentityUser("E200");
            IdentityUser E201 = new IdentityUser("E201");
            IdentityUser E202 = new IdentityUser("E202");
            IdentityUser E203 = new IdentityUser("E203");
            IdentityUser E300 = new IdentityUser("E300");
            IdentityUser E301 = new IdentityUser("E301");
            IdentityUser E302 = new IdentityUser("E302");
            IdentityUser E303 = new IdentityUser("E303");
            IdentityUser E400 = new IdentityUser("E400");
            IdentityUser E401 = new IdentityUser("E401");
            IdentityUser E402 = new IdentityUser("E402");
            IdentityUser E403 = new IdentityUser("E403");
            IdentityUser E500 = new IdentityUser("E500");
            IdentityUser E501 = new IdentityUser("E501");
            IdentityUser E502 = new IdentityUser("E502");
            IdentityUser E503 = new IdentityUser("E503");

            await userManager.CreateAsync(E000, "Pass00?");
            await userManager.CreateAsync(E001, "Pass01?");
            await userManager.CreateAsync(E100, "Pass02?");
            await userManager.CreateAsync(E101, "Pass03?");
            await userManager.CreateAsync(E102, "Pass04?");
            await userManager.CreateAsync(E103, "Pass05?");
            await userManager.CreateAsync(E200, "Pass06?");
            await userManager.CreateAsync(E201, "Pass07?");
            await userManager.CreateAsync(E202, "Pass08?");
            await userManager.CreateAsync(E203, "Pass09?");
            await userManager.CreateAsync(E300, "Pass10?");
            await userManager.CreateAsync(E301, "Pass11?");
            await userManager.CreateAsync(E302, "Pass12?");
            await userManager.CreateAsync(E303, "Pass13?");
            await userManager.CreateAsync(E400, "Pass14?");
            await userManager.CreateAsync(E401, "Pass15?");
            await userManager.CreateAsync(E402, "Pass16?");
            await userManager.CreateAsync(E403, "Pass17?");
            await userManager.CreateAsync(E500, "Pass18?");
            await userManager.CreateAsync(E501, "Pass19?");
            await userManager.CreateAsync(E502, "Pass20?");
            await userManager.CreateAsync(E503, "Pass21?");

            await userManager.AddToRoleAsync(E000, "Administrator");
            await userManager.AddToRoleAsync(E001, "Coordinator");
            await userManager.AddToRoleAsync(E100, "Manager");
            await userManager.AddToRoleAsync(E101, "Investigator");
            await userManager.AddToRoleAsync(E102, "Investigator");
            await userManager.AddToRoleAsync(E103, "Investigator");
            await userManager.AddToRoleAsync(E200, "Manager");
            await userManager.AddToRoleAsync(E201, "Investigator");
            await userManager.AddToRoleAsync(E202, "Investigator");
            await userManager.AddToRoleAsync(E203, "Investigator");
            await userManager.AddToRoleAsync(E300, "Manager");
            await userManager.AddToRoleAsync(E301, "Investigator");
            await userManager.AddToRoleAsync(E302, "Investigator");
            await userManager.AddToRoleAsync(E303, "Investigator");
            await userManager.AddToRoleAsync(E400, "Manager");
            await userManager.AddToRoleAsync(E401, "Investigator");
            await userManager.AddToRoleAsync(E402, "Investigator");
            await userManager.AddToRoleAsync(E403, "Investigator");
            await userManager.AddToRoleAsync(E500, "Manager");
            await userManager.AddToRoleAsync(E501, "Investigator");
            await userManager.AddToRoleAsync(E502, "Investigator");
            await userManager.AddToRoleAsync(E503, "Investigator");
        }
    }
}
