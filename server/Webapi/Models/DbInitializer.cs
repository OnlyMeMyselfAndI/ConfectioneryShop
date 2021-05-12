using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webapi.Models
{
  public class DbInitializer
  {
    public static async Task Init(IServiceProvider provider)
    {
      var ctx = provider.GetRequiredService<AppDbContext>();

      ctx.Database.EnsureCreated();

      await EnsureAdminCreated(provider);

      //if (ctx.Users.Any() || ctx.Products.Any() || ctx.Orders.Any())
      //{
      //  // already created, don't duplicate
      //  return;
      //}

      //ctx.Products.AddRange(new Product[]
      //{
      //          new Product { Name = "Banana Cake", Price = 300 },
      //          new Product { Name = "Brownies", Price = 210 },
      //          new Product { Name = "Pure sugar", Price = 29.75 },
      //});
      //ctx.SaveChanges();

      //ctx.Orders.AddRange(new Order[]
      //{
      //          // Vlad Sook's orders
      //          new Order { UserID = 1, ProductID = 1, Amount = 2 },
      //          new Order { UserID = 1, ProductID = 3, Amount = 1 },
      //          // Anton Volkov's orders
      //          new Order { UserID = 3, ProductID = 2, Amount = 10 },
      //});
      //ctx.SaveChanges();
    }

    protected static async Task EnsureAdminCreated(IServiceProvider provider)
    {
      var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
      var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
      var configuration = provider.GetRequiredService<IConfiguration>();

      string[] roleNames = { Roles.Admin, Roles.Customer };
      IdentityResult roleResult;

      foreach (var roleName in roleNames)
      {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        // ensure that the role does not exist
        if (!roleExist)
        {
          //create the roles and seed them to the database: 
          roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
        }
      }

      var adminData = configuration.GetSection("Admin");

      // find the user with the admin email 
      var admin = await userManager.FindByEmailAsync(adminData["Email"]);

      // check if the user exists
      if (admin == null)
      {
        admin = new ApplicationUser
        {
          Email = adminData["Email"],
          FullName = adminData["FullName"],
          UserName = adminData["UserName"],
        };

        var createAdmin = await userManager.CreateAsync(admin, adminData["Password"]);
        if (createAdmin.Succeeded)
        {
          //here we tie the new user to the role
          await userManager.AddToRoleAsync(admin, Roles.Admin);

        }
      }
    }
  }
}
