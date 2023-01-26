using ServiceStack;
using ServiceStack.Configuration;

using System.Collections;
using System.Collections.Generic;
using GDIApps.ServiceModel.Types;

namespace GDIApps.ServiceModel;

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