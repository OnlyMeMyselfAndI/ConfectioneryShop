using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webapi.Models
{
  public class Order
  {
    public int ID { get; set; }
    public int Amount { get; set; }
    public DateTime Time { get; set; }

    public int ProductID { get; set; }
    public string ApplicationUserID { get; set; }
  }
}
