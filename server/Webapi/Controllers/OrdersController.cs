using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webapi.Models;
using Webapi.Repositories;
using Webapi.RequestModels;

namespace Webapi.Controllers
{
  [Route("api/v1/orders")]
  [ApiController]
  public class OrdersController : ControllerBase
  {
    private readonly IOrdersService ordersService;
    private readonly IProductsService productsService;
    private readonly AppDbContext ctx;

    public OrdersController(IOrdersService ordersService, IProductsService productsService, AppDbContext ctx)
    {
      this.ordersService = ordersService;
      this.productsService = productsService;
      this.ctx = ctx;
    }

    /*
      POST (Admin) /api/v1/orders
    */
    [HttpPost]
    public async Task<ActionResult> Create(CreateOrderRequest reqBody)
    {
      if (!ModelState.IsValid)
        return BadRequest(new { errors = new { global = "Request body is invalid" } });

      var productFound = await productsService.GetByTitle(reqBody.ProductTitle);
      if (productFound == null) return Error("Product not found with the passed id");

      var userFound = await ctx.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == reqBody.UserEmail);
      if (userFound == null) return Error("User not found with the passed id");

      var order = new Order
      {
        ProductID = productFound.ID,
        ApplicationUserID = userFound.Id,

        Amount = reqBody.Amount,
        Time = DateTime.Now,
      };

      await ordersService.Create(order); 

      return CreatedAtAction(nameof(GetById), new { id = order.ID }, order);
    }

    /*
      GET (All) /api/v1/orders/{id}
    */
    [HttpGet("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> GetById(int id)
    {
      var (err, order) = await CheckOrder(await ordersService.GetById(id));
      if (order == null) return Error(err);

      return Ok(order);
    }

    /*
      GET (All) /api/v1/orders/{}
    */
    [HttpGet("remove/{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> RemoveById(int id)
    {
      await ordersService.RemoveById(id);
      return Ok();
    }

    /*
      GET (All) /api/v1/orders
    */
    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> GetAll()
    {
      var orders = await ordersService.GetAll();
      var shapedOrders = orders.Select(async o =>
      {
        var productFound = await productsService.GetById(o.ProductID);
        var userFound = await ctx.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == o.ApplicationUserID);

        return new
        {
          email = userFound.Email,
          product = productFound.Title,
          time = o.Time.ToString("dddd, dd MMMM yyyy HH:mm:ss"),
          amount = o.Amount,
        };
      }).Select(q => q.Result);

      return Ok(shapedOrders);
    }

    #region utils

    protected async Task<(string err, Order order)> CheckOrder(Order orderFound)
    {
      if (orderFound == null) return ("Order not found with the passed id", null);

      var productFound = await productsService.GetById(orderFound.ProductID);
      if (productFound == null) return ("Product not found with the passed id", null);

      var userFound = await ctx.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == orderFound.ApplicationUserID);
      if (userFound == null) return ("User not found with the passed id", null);

      return ("", orderFound);
    }

    protected ActionResult Error(string msg) => BadRequest(new { errors = new { global = msg } });

    #endregion
  }
}
