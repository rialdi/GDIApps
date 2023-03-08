using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

public class ContactUs : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    public string Message { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("ContactUs")]
[AutoApply(Behavior.AuditQuery)]
public class QueryContactUses : QueryDb<ContactUs> {
}

[ValidateIsAuthenticated]
[Tag("ContactUs")]
[Route("/contacts/email", "POST")]
public class ContactUsEmail : IReturn<ContactUsEmailResponse>
{
    public int ContactId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}