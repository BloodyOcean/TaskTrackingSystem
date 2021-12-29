using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}