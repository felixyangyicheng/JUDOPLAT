using System.ComponentModel.DataAnnotations;

namespace JUDOPLAT.Shared.Dtos.User
{
    public class UserResetForgottenPasswordDto : UserEmailDto
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordConfirm { get; set; }

    }
}
