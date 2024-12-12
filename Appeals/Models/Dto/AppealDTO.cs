using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Appeals.Models
{
    public class AppealDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Text { get; set; } = null!;

        [Required]
        public DateOnly CreationDate { get; set; }

        public DateOnly? DecisionDate { get; set; } = null!;

        [Required]
        public StatusDTO Status { get; set; }

        [Required]
        public TopicDTO Topic { get; set; }

    }
}
