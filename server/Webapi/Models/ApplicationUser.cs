using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Webapi.Models
{
  public class ApplicationUser : IdentityUser
  {
    [MaxLength(150)]
    public string FullName { get; set; }

    public ICollection<Order> Orders { get; set; }
  }

  public static class Roles
  {
    public const string Admin = "Admin";
    public const string Customer = "Customer";
  }
}
