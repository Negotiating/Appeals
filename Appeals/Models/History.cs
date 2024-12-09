using System;
using System.Collections.Generic;

namespace Appeals.Models;

public partial class History
{
    public int Id { get; set; }

    public int IdAppeal { get; set; }

    public int IdUser { get; set; }

    public DateTime ChangeDate { get; set; }

    public string? Description { get; set; }

    public DateTime? DeletionDate { get; set; }

    public virtual Appeal IdAppealNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
