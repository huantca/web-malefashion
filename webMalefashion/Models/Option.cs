using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webMalefashion.Models;

public partial class Option
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string? ColorHex { get; set; }

    public string? ImageUrl { get; set; }


    [Display(Name = "Product Photo")]
    [NotMapped]
    [DataType(DataType.Upload)]
    [FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg")]
    public IFormFile ProductPhoto { get; set; }
    public decimal? Price { get; set; }

    public string? SizeId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
