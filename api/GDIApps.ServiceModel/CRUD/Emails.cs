using ServiceStack;
using ServiceStack.Configuration;

using System.Collections;
using System.Collections.Generic;
using GDIApps.ServiceModel.Types;

namespace GDIApps.ServiceModel;

[ValidateIsAuthenticated]
[Tag("Emails")]
[AutoApply(Behavior.AuditQuery)]
public class QueryEmails : QueryDb<Email> {
    public string? ToContains { get; set; } 
    public string? SubjectContains { get; set; }
}