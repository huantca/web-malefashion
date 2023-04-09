using System;
using System.Collections.Generic;

namespace webMalefashion.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Address { get; set; }

    public DateTime? RegistrationTime { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; } = new List<CartDetail>();

    public virtual ICollection<SellReceipt> SellReceipts { get; } = new List<SellReceipt>();
}
