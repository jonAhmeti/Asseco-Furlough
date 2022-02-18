namespace Furlough.Services.Mail
{
    public class MailRequest
    {
        public MailRequest(string toEmail, string subject, string body, List<IFormFile>? attachments)
        {
            ToEmail = toEmail;
            Subject = subject;
            Body = body;

            if (attachments != null)
                Attachments = attachments;
        }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
