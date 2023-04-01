using System;
using System.Collections.Generic;

namespace webMalefashion.Models;

public partial class SellReceiptDetail
{
    public int SellReceiptId { get; set; }

    public int ProductId { get; set; }

    public int? Amount { get; set; }

    public float? Discount { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual SellReceipt SellReceipt { get; set; } = null!;
}
