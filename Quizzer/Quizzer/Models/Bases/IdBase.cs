using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzer.Models.Bases
{
    public class IdBase
    {
        [Key]
        [Column(TypeName = "nvarchar(36)")]
        [Required(ErrorMessage = "Id field is required.")]
        public string? Id { get; set; }
    }
}
