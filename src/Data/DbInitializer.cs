using ConfectineryShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfectineryShop.Data
{
    public class DbInitializer
    {
        public static void Init(ShopContext ctx)
        {
            ctx.Database.EnsureCreated();

            if (ctx.Users.Any() || ctx.Products.Any() || ctx.Orders.Any())
            {
                // already created, don't duplicate
                return;
            }

            ctx.Users.AddRange(new User[]
            {
                new User { FirstName = "Vlad", LastName = "Sook" },
                new User { FirstName = "Bohdan", LastName = "Zhuk" },
                new User { FirstName = "Anton", LastName = "Volkov" },
            });
            ctx.SaveChanges();

            ctx.Products.AddRange(new Product[]
            {
                new Product { Name = "Banana Cake", Price = 300 },
                new Product { Name = "Brownies", Price = 210 },
                new Product { Name = "Pure sugar", Price = 29.75 },
            });
            ctx.SaveChanges();

            ctx.Orders.AddRange(new Order[]
            {
                // Vlad Sook's orders
                new Order { UserID = 1, ProductID = 1, Amount = 2 },
                new Order { UserID = 1, ProductID = 3, Amount = 1 },
                // Anton Volkov's orders
                new Order { UserID = 3, ProductID = 2, Amount = 10 },
            });
            ctx.SaveChanges();
        }
    }
}
