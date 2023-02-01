namespace IdentityNet7UdemyLesson.web.Services
{
    public interface IEmailService
    {
        Task SendResetPasswordEmail(string resetEmailLink,string ToEmail);
    }
}
