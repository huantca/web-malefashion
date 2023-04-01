using System;
using System.Collections.Generic;

namespace webMalefashion.Models;

public partial class Permission
{
    public int Id { get; set; }

    public int? PositionId { get; set; }

    public virtual Position? Position { get; set; }
}
