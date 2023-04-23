using System;
using System.Collections.Generic;

namespace webMalefashion.Models;

public partial class SellReceipt
{
    public SellReceipt(int id, int? staffId, DateTime? time, int? customerId) {
        Id = id;
        StaffId = staffId;
        Time = time;
        CustomerId = customerId;
    }

    public int Id { get; set; }

    public int? StaffId { get; set; }

    public DateTime? Time { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<SellReceiptDetail> SellReceiptDetails { get; } = new List<SellReceiptDetail>();

    public virtual Staff? Staff { get; set; }
}
