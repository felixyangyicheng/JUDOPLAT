using System.ComponentModel.DataAnnotations;

namespace JUDOPLAT.Shared.Dtos.User
{
    public class UserLoginDto : UserEmailDto
    {

        [Required]
        public string Password { get; set; }
    }
}
