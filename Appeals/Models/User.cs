using System;
using System.Collections.Generic;

namespace Appeals.Models;

public partial class User
{
    public int Id { get; set; }

    public int? AddressId { get; set; }

    public string Name { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Middlename { get; set; }

    public string Email { get; set; } = null!;

    public int RoleId { get; set; }

    public DateTime? DeletionDate { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Appeal> AppealIdExecutorNavigations { get; set; } = new List<Appeal>();

    public virtual ICollection<Appeal> AppealIdResidentNavigations { get; set; } = new List<Appeal>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual Role Role { get; set; } = null!;
}
