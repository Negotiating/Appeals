using System;
using System.Collections.Generic;

namespace Appeals.Models;

public partial class Appeal
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Text { get; set; } = null!;

    public DateOnly CreationDate { get; set; }

    public DateOnly? DecisionDate { get; set; }

    public int IdStatus { get; set; }

    public int IdExecutor { get; set; }

    public int IdResident { get; set; }

    public int IdPlot { get; set; }

    public int IdTopic { get; set; }

    public int? Grade { get; set; }

    public DateTime? DeletionDate { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual User IdExecutorNavigation { get; set; } = null!;

    public virtual Plot IdPlotNavigation { get; set; } = null!;

    public virtual User IdResidentNavigation { get; set; } = null!;

    public virtual Status IdStatusNavigation { get; set; } = null!;

    public virtual Topic IdTopicNavigation { get; set; } = null!;
}
