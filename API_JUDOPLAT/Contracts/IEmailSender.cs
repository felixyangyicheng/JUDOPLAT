
namespace JUDOPLAT.API_JUDOPLAT.Contracts
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}
