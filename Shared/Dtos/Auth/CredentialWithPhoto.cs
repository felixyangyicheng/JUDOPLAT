
namespace JUDOPLAT.Shared.Dtos.Auth
{
    public class CredentialWithPhoto 
    {
        public string Photo { get; set; } = "";
        public string UserId { get; set; } = "";
        public string Email { get; set; } = "";
        public string Name { get; set; } = "";
        public bool IsLogged { get; set; } = false;
    }
}
