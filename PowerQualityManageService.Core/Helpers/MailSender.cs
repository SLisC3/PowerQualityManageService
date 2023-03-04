using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Helpers;
public static class MailSender
{
    public static void SendMail(/* Model with results */ /* Model with attachement probably pdf */ IEnumerable<MailAddress> addressTo)
    {
        // Wyciągnąć dane do logowania

        var mailMessage = new MailMessage();

        //mailMessage.SetFrom() Z wyciągnietych wyżej danych 

        // Get body template with subject with overall short results

        //

        mailMessage.SendSmtpAsync();
    }
}
