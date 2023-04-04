using System;
using System.Collections.Generic;

namespace webMalefashion.Models;

public partial class CartDetail
{
    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public int OptionId { get; set; }

    public int? Amount { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
