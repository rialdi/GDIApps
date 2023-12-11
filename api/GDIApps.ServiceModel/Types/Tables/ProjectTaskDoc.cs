using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(ProjectTaskId), nameof(FileName))]
public class ProjectTaskDoc
{
    [AutoIncrement]
    public int Id { get; set; }  

    [References(typeof(ProjectTask))]
    public int ProjectTaskId { get; set; }

    [Required]
    public string FileName { get; set; } = string.Empty;

    [Input(Type = "file"), UploadTo("ProjectTaskDocs")]
    public string? AttachmentUrl { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class QueryProjectTaskDocs : QueryDb<ProjectTaskDoc>{
    public int? ProjectTaskId {get; set;}
}


[Tag("Projects")]
[ValidateIsAuthenticated]
public class CreateProjectTaskDoc : ICreateDb<ProjectTaskDoc>, IReturn<CRUDResponse>
{
    public int ProjectTaskId { get; set; }
    public string FileName { get; set; } = string.Empty;
    [Input(Type = "file"), UploadTo("ProjectTaskDocs")]
    public string? AttachmentUrl { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class UpdateProjectTaskDoc : IPatchDb<ProjectTaskDoc>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
     public int ProjectTaskId { get; set; }

    [Input(Type = "file"), UploadTo("ProjectTaskDocs")]
    public string FileName { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class DeleteProjectTaskDoc : IDeleteDb<Project>, IReturnVoid
{
    public int Id { get; set; }        
}

