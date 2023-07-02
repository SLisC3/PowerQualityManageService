using PowerQualityManageService.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Helpers;
public static class MailSender
{
    public static async Task<bool> SendMail(MailModel model)
    {
        var mailMessage = new MailMessage();
        mailMessage.SetFrom("powerqualitymanager@gmail.com", "PQM");
        mailMessage.SetSubject(model.Title);
        mailMessage.SetBody(model.Body);
        mailMessage.AddTo(model.Mail, model.DisplayName);
        mailMessage.Attachments.Add(new Attachment(Path.Combine(Directory.GetCurrentDirectory(), "Reports", model.Attachment)));
        return await mailMessage.SendSmtpAsync();
    }
}
