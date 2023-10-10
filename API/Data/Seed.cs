using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class Seed
    {
        public static async Task Initialize(UserManager<AppUser> userManager)
        {
            // Seed admin user logic goes here

            // Check if admin user already exists
            var adminUser = await userManager.FindByEmailAsync("admin@example.com");
            if (adminUser == null)
            {
                // Create a new admin user
                var newAdminUser = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@example.com"
                };

                var result = await userManager.CreateAsync(newAdminUser, "P@ssw0rd"); // Replace with your desired password

                if (result.Succeeded)
                {
                    // Assign the admin role to the admin user
                    await userManager.AddToRoleAsync(newAdminUser, "Admin");
                }
                else
                {
                    // Handle any errors during user creation
                    // (e.g. invalid password format, username taken, etc.)
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
            }
        }
    }

}