using System;
using System.Collections.Generic;

namespace Appeals.Models;

public partial class Plot
{
    public int Id { get; set; }

    public int OrganizationId { get; set; }

    public DateTime? DeletionDate { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Appeal> Appeals { get; set; } = new List<Appeal>();

    public virtual Organization Organization { get; set; } = null!;
}
