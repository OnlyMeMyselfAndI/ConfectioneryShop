using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webapi.Models;

namespace Webapi.Repositories.impl
{
  public class OrdersService : IOrdersService
  {
    private readonly AppDbContext ctx;

    public OrdersService(AppDbContext ctx) { this.ctx = ctx; }

    public async Task Create(Order newOrder)
    {
      await ctx.Orders.AddAsync(newOrder);
      await ctx.SaveChangesAsync();
    }

    public async Task<IEnumerable<Order>> GetAll() => await ctx.Orders.ToListAsync();

    public async Task<Order> GetById(int id) =>
      await ctx.Orders.FirstOrDefaultAsync(order => order.ID == id);

    public async Task RemoveById(int id)
    {
      var order = await GetById(id);
      ctx.Orders.Remove(order);
      await ctx.SaveChangesAsync();
    }
  }
}
