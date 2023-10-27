using System.ComponentModel.DataAnnotations;

namespace JUDOPLAT.Shared.Dtos.User
{
    public class UserRegisterDto : UserLoginDto
    {
        [Required]

        public string PasswordConfirm { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }


        public DateTime? Birthday { get; set; }

    }
}
