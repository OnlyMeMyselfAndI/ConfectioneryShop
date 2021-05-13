using System.ComponentModel.DataAnnotations;

namespace Webapi.RequestModels
{
  public class CreateOrderRequest
  {
    public int Amount { get; set; } = 1;

    [Required]
    public string UserEmail { get; set; }

    [Required]
    public string ProductTitle  { get; set; }
  }
}
