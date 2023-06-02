using System.Net.Mail;

namespace PowerQualityManageService.Model.Models;

public class MailModel
{
    public List<EmailAddress> AddressesTo { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!; 
    public string? Attachment { get; set; } = null!;
}

public class EmailAddress
{
    public string Mail { get; set; } = null!;
    public string? DisplayName { get; set; } 
}