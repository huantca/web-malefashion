using System;
using System.Collections.Generic;

namespace webMalefashion.Models;

public partial class Staff
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime? StartWorking { get; set; }

    public DateTime? EndWorking { get; set; }

    public int? PositionId { get; set; }

    public virtual Position? Position { get; set; }

    public virtual ICollection<SellReceipt> SellReceipts { get; } = new List<SellReceipt>();
}
