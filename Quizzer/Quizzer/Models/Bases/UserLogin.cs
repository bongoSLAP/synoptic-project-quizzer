using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzer.Models.Bases
{
    public class UserLogin : IdBase
    {
        [Column(TypeName = "nvarchar(60)")]
        [Required(ErrorMessage = "Username field is required.")]
        public string Username { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(128)")]
        [Required(ErrorMessage = "Password field is required.")]
        public string Password { get; set; } = string.Empty;
    }
}
