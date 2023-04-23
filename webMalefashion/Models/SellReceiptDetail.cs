using System;
using System.Collections.Generic;

namespace webMalefashion.Models;

public partial class SellReceiptDetail
{
    public SellReceiptDetail(int id, int sellReceiptId, int productId, int? amount, float? discount) {
        Id = id;
        SellReceiptId = sellReceiptId;
        ProductId = productId;
        Amount = amount;
        Discount = discount;
    }

    public int Id { get; set; }

    public int SellReceiptId { get; set; }

    public int ProductId { get; set; }

    public int? Amount { get; set; }

    public float? Discount { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual SellReceipt SellReceipt { get; set; } = null!;
}
