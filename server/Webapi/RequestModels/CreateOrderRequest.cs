using System.ComponentModel.DataAnnotations;

namespace Webapi.RequestModels
{
  public class CreateOrderRequest
  {
    public int Amount { get; set; } = 1;

    [Required]
    [MaxLength(36)]
    public string UserID { get; set; }

    [Required]
    public int ProductID  { get; set; }
  }
}
