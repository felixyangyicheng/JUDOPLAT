
namespace JUDOPLAT.API_JUDOPLAT.Contracts
{
    public interface ITokenRepo
    {
       Task<string> GenerateToken(ApiUser user,bool thirdParty, string? imageLink);
    }
}
