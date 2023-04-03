using System;
using System.Collections.Generic;

namespace webMalefashion.Models;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
