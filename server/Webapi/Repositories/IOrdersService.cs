using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webapi.Models;

namespace Webapi.Repositories
{
  public interface IOrdersService
  {
    /*
      Create a Order
    */
    Task Create(Order newOrder);

    /*
      Get All Orders
    */
    Task<IEnumerable<Order>> GetAll();

    /*
      Get Order by its id
    */
    Task<Order> GetById(int id);

    /*
      Remove Order by its id
    */
    Task RemoveById(int id);
  }
}
