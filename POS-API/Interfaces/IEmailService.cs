namespace POS_API.Interfaces
{
    public interface IEmailService
    {
        Task<bool> sendEmail(string receiver, string subject, string body); 
    }
}
