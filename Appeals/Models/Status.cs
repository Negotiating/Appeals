using System;
using System.Collections.Generic;

namespace Appeals.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DeletionDate { get; set; }

    public virtual ICollection<Appeal> Appeals { get; set; } = new List<Appeal>();
}
