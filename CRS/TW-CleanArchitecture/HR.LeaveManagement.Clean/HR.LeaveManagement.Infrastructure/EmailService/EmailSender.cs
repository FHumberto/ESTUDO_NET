using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastructure.EmailService;

public class EmailSender : IEmailSender
{
    public EmailSettings _emailSettings { get; }

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task<bool> SendEmail(EmailMessage email)
    {
        SendGridClient client = new(_emailSettings.ApiKey);
        EmailAddress to = new(email.To);
        EmailAddress from = new()
        {
            Email = _emailSettings.FormAddress,
            Name = _emailSettings.FormName
        };

        SendGridMessage message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
        Response response = await client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}
