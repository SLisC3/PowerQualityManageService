using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PowerQualityManageService.Core.Helpers;
public static class MailSenderExtensions
{
    public static async Task<bool> SendSmtpAsync(this MailMessage message)
    {
        string mailFrom = "PowerQualityManager@gmail.com";
        string passwordFrom = "mkmmzzrtqmtngzuz";

        var smtp = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(mailFrom, passwordFrom),
            EnableSsl = true
        };
        try
        {
            await smtp.SendMailAsync(message);
            return true;
        }
        catch (Exception ex) { return false; }
    }


    public static void AddTo(this MailMessage mailMessage, string addressTo, string? display = null)
    {
        mailMessage.To.Add(new MailAddress(addressTo, display));
    }

    public static void SetFrom(this MailMessage mailMessage, string addressFrom, string? display = null)
    {
        mailMessage.From = (new MailAddress(addressFrom,display));
    }

    public static void SetSubject(this MailMessage mailMessage, string subject)
    {
        mailMessage.Subject = subject;
    }

    public static void SetBody(this MailMessage mailMessage, string body, bool isHtml = false)  
    {
        mailMessage.Body = body;
        mailMessage.IsBodyHtml = isHtml;
    }

}
