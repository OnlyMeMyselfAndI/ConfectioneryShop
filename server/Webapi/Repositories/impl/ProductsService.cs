using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webapi.Models;

namespace Webapi.Repositories.Impl
{
  public class ProductsService : IProductsService
  {
    private readonly AppDbContext ctx;

    public ProductsService(AppDbContext ctx) { ctx = ctx; }

    public async Task Create(Product newProduct)
    {
      await ctx.Products.AddAsync(newProduct);
      await ctx.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAll() => await ctx.Products.ToListAsync();

    public async Task<Product> GetById(int id) =>
      await ctx.Products.FirstOrDefaultAsync(product => product.ID == id);

    public async Task<Product> GetByTitle(string title) =>
      await ctx.Products.FirstOrDefaultAsync(product => product.Title == title);

    public async Task RemoveByTitle(string title)
    {
      var product = await GetByTitle(title);
      ctx.Products.Remove(product);
      await ctx.SaveChangesAsync();
    }
  }
}
