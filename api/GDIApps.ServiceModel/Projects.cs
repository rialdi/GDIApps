using ServiceStack;
using ServiceStack.Configuration;

using System.Collections;
using System.Collections.Generic;
using GDIApps.ServiceModel.Types;

namespace GDIApps.ServiceModel;

[ValidateIsAuthenticated]
[Tag("Projects")]
[AutoApply(Behavior.AuditQuery)]
public class QueryProjects : QueryDb<Project, ProjectView>, IJoin<Project, Client>  {
    public string? Code {get; set;}
    public string? ClientId{get; set;}
    public string? ClientCode {get; set;}
}

[ValidateIsAuthenticated]
[Tag("Projects")]
[AutoApply(Behavior.AuditCreate)]
public class CreateProject : ICreateDb<Project>, IReturn<CRUDResponse>
{
    public int ClientId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Projects")]
[AutoApply(Behavior.AuditModify)]
public class UpdateProject : IPatchDb<Project>, IReturn<CRUDResponse>
{
    public int? Id { get; set; } 
     public int ClientId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Projects")]
// [AutoApply(Behavior.AuditSoftDelete)]
public class DeleteProject : IDeleteDb<Project>, IReturnVoid
{
    public int Id { get; set; }        
}