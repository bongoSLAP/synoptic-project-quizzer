using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Quizzer.Models.Bases;
using Quizzer.Models.Entities.Info;
using Quizzer.Models.Enums;

namespace Quizzer.Models.Entities
{
    public class User : UserLogin
    {
        [Column(TypeName = "nvarchar(70)")]
        [Required(ErrorMessage = "FirstName field is required.")]
        public string FirstName { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(120)")]
        [Required(ErrorMessage = "LastName field is required.")]
        public string LastName { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = "Email field is required.")]
        public string Email { get; set; } = string.Empty;
        [Column(TypeName = "text")]
        [Required(ErrorMessage = "Role field is required.")]
        public Role Role { get; set; }

        public UserInfo Map(User? user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User object cannot be null.");
            
            return new UserInfo {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
