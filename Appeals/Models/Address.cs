using System;
using System.Collections.Generic;

namespace Appeals.Models;

public partial class Address
{
    public int Id { get; set; }

    public int PlotId { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Build { get; set; } = null!;

    public int? Appart { get; set; }

    public DateTime? DeletionDate { get; set; }

    public virtual Plot Plot { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
