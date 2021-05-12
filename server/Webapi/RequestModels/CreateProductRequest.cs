using System.ComponentModel.DataAnnotations;

namespace Webapi.RequestModels
{
  public class CreateProductRequest
  {
    [Required]
    [MaxLength(60)]
    public string Title { get; set; }

    [MaxLength(255)]
    public string Image { get; set; } = "";

    public decimal Price { get; set; }

    [MaxLength(1024)]
    public string Info { get; set; } = "";
  }
}
