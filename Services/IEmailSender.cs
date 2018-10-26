using System.Threading.Tasks;

namespace TranquilAlmatyWebsite.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
