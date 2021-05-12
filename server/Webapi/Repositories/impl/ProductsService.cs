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
      await ctx.Products.FirstOrDefaultAsync(product => product.Id == id);

    public async Task RemoveById(int id)
    {
      var product = await GetById(id);
      ctx.Products.Remove(product);
      await ctx.SaveChangesAsync();
    }
  }
}
