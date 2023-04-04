﻿using System;
using System.Collections.Generic;

namespace webMalefashion.Models;

public partial class Option
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string? ColorHex { get; set; }

    public string? ImageUrl { get; set; }

    public decimal? Price { get; set; }

    public string? SizeId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
