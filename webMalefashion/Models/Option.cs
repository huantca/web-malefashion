using System;
using System.Collections.Generic;

namespace webMalefashion.Models;

public partial class Option
{
    public int ProductId { get; set; }

    public string ColorHex { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public decimal? Price { get; set; }

    public string SizeId { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
