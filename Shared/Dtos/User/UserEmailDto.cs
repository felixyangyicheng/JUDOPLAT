using System.ComponentModel.DataAnnotations;

namespace JUDOPLAT.Shared.Dtos.User
{
    public class UserEmailDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
