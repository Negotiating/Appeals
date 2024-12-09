using System;
using System.Collections.Generic;

namespace Appeals.Models;

public partial class Organization
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly CreateDate { get; set; }

    public DateOnly? ArchivingDate { get; set; }

    public string Address { get; set; } = null!;

    public DateTime? DeletionDate { get; set; }

    public virtual ICollection<Plot> Plots { get; set; } = new List<Plot>();
}
