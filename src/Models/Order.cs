using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfectineryShop.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int Amount { get; set; }

        public int ProductID { get; set; }
        public int UserID { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }

    }
}
