using System;
using System.Collections.Generic;

namespace webMalefashion.Models;

public partial class Position
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Permission> Permissions { get; } = new List<Permission>();

    public virtual ICollection<Staff> Staff { get; } = new List<Staff>();
}
