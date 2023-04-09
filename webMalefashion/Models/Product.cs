using System;
using System.Collections.Generic;

namespace webMalefashion.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public int? ManufacturerId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Manufacturer? Manufacturer { get; set; }

    public virtual List<Option> Options { get; } = new List<Option>();

    public virtual ICollection<SellReceiptDetail> SellReceiptDetails { get; } = new List<SellReceiptDetail>();
}
