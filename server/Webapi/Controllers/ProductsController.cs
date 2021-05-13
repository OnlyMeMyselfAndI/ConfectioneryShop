using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models;
using Webapi.Repositories;
using Webapi.RequestModels;

namespace Webapi.Controllers
{
  [Route("api/v1/products")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private readonly IProductsService productsService;

    public ProductsController(IProductsService productsService)
    {
      this.productsService = productsService;
    }

    /*
      POST (Admin) /api/v1/products
    */
    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> Create(CreateProductRequest reqBody)
    {
      if (!ModelState.IsValid)
        return BadRequest(new { errors = new { global = "Request body is invalid" } });

      var productFound = await productsService.GetByTitle(reqBody.Title);
      if (productFound != null)
        return BadRequest(new { errors = new { global = "Product with this title already exists" } });

      var newProduct = new Product
      {
        Title = reqBody.Title,
        Image = reqBody.Image,
        Price = reqBody.Price,
        Info = reqBody.Info,
      };

      await productsService.Create(newProduct);

      return CreatedAtAction(nameof(GetByTitle), new { title = newProduct.Title }, newProduct);
    }

    /*
      GET (All) /api/v1/products/{title}
    */
    [HttpGet("{title}")]
    public async Task<ActionResult> GetByTitle(string title)
    {
      var productFound = await this.productsService.GetByTitle(title);

      if (productFound == null)
        return BadRequest(new { errors = new { global = "Product not found with the passed title" } });

      return Ok(productFound);
    }

    /*
      GET (All) /api/v1/products/remove{title}
    */
    [HttpGet("remove/{title}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> RemoveByTitle(string title)
    {
      await this.productsService.RemoveByTitle(title);
      return Ok();
    }

    /*
      GET (All) /api/v1/products
    */
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
      return Ok(await this.productsService.GetAll());
    }
  }
}
