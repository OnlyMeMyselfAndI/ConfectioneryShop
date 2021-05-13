using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Webapi.Models;

namespace Webapi.Repositories.Impl
{
  public class ProductsServiceDapper : IProductsService
  {
    private readonly IConfiguration config;
    private readonly string connectionString;

    public ProductsServiceDapper(IConfiguration config)
    {
      this.config = config;
      this.connectionString = config["ConnectionStrings:DefaultConnection"];
    }

    public async Task Create(Product newProduct)
    {
      string sql = $"INSERT INTO public.\"Products\"(\"Title\", \"Image\", \"Price\", \"Info\") VALUES ('{newProduct.Title}', '{newProduct.Image}', {newProduct.Price}, '{newProduct.Info}')";

      using (var connection = new NpgsqlConnection(this.connectionString))
      {
        var res = await connection.QueryAsync<Product>(sql);
      }
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
      string sql = "SELECT * FROM public.\"Products\"";
      using (var connection = new NpgsqlConnection(this.connectionString))
      {
        return await connection.QueryAsync<Product>(sql);
      }
    }

    public async Task<Product> GetById(int id)
    {
      string sql = $"SELECT * FROM public.\"Products\" WHERE \"ID\" = {id}";
      using (var connection = new NpgsqlConnection(this.connectionString))
      {
        return await connection.QueryFirstAsync<Product>(sql);
      }
    }

    public async Task<Product> GetByTitle(string title)
    {
      string sql = $"SELECT * FROM public.\"Products\" WHERE \"Title\" = '{title}'";
      using (var connection = new NpgsqlConnection(this.connectionString))
      {
        return await connection.QueryFirstAsync<Product>(sql);
      }
    }

    public async Task RemoveByTitle(string title)
    {
      string sql = $"DELETE FROM public.\"Products\" WHERE \"Title\" = '{title}'";
      using (var connection = new NpgsqlConnection(this.connectionString))
      {
        await connection.QueryAsync<Product>(sql);
      }
    }
  }
}
