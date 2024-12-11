
using System.ComponentModel.DataAnnotations.Schema;

namespace Appeals.Models;

public partial class Appeal
{
    [Column("ID")]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Text { get; set; } = null!;

    [Column("Creation_date")]
    public DateOnly CreationDate { get; set; }

    [Column("Desision_date")]
    public DateOnly? DecisionDate { get; set; }
    
    [Column("Id_status")]
    public int IdStatus { get; set; }

    [Column("Id_executor")]
    public int IdExecutor { get; set; }

    [Column("Id_resident")]
    public int IdResident { get; set; }

    [Column("Id_plot")]
    public int IdPlot { get; set; }

    [Column("Id_topic")]
    public int IdTopic { get; set; }

    public int? Grade { get; set; }

    [Column("Deletion_date")]
    public DateTime? DeletionDate { get; set; }
   
    [NotMapped]
    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    [NotMapped] 
    public virtual User IdExecutorNavigation { get; set; } = null!;

    [NotMapped]
    public virtual Plot IdPlotNavigation { get; set; } = null!;

    [NotMapped]
    public virtual User IdResidentNavigation { get; set; } = null!;

    [NotMapped]
    public virtual Status IdStatusNavigation { get; set; } = null!;

    [NotMapped]
    public virtual Topic IdTopicNavigation { get; set; } = null!;
}
