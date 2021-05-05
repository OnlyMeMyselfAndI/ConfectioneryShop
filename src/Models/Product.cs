using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConfectineryShop.Models
{
    public class Product
    {
        public int ID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public double Price { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
