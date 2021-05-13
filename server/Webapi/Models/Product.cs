using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webapi.Models
{
  public class Product
  {
    public int ID { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }
    public string Info { get; set; }

    public ICollection<Order> Orders { get; set; }
  }
}
