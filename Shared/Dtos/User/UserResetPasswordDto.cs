using System.ComponentModel.DataAnnotations;

namespace JUDOPLAT.Shared.Dtos.User
{
    public class UserResetPasswordDto : UserEmailDto
    {

        [Required]
        public string Password { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string NewPasswordConfirm { get; set; }
    }
}
