using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(ProjectId), nameof(FileName))]
public class ProjectDoc
{
    [AutoIncrement]
    public int Id { get; set; }  

    [References(typeof(Project))]
    public int ProjectId { get; set; }

    [Required]
    public string FileName { get; set; } = string.Empty;

    [Input(Type = "file"), UploadTo("projectDocs")]
    public string? AttachmentUrl { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class QueryProjectDocs : QueryDb<ProjectDoc>{
    public int? ProjectId {get; set;}
}


[Tag("Projects")]
[ValidateIsAuthenticated]
public class CreateProjectDoc : ICreateDb<ProjectDoc>, IReturn<CRUDResponse>
{
    public int ProjectId { get; set; }
    public string FileName { get; set; } = string.Empty;
    [Input(Type = "file"), UploadTo("projectDocs")]
    public string? AttachmentUrl { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class UpdateProjectDoc : IPatchDb<ProjectDoc>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 

    [Input(Type = "file"), UploadTo("projectDocs")]
    public string FileName { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class DeleteProjectDoc : IDeleteDb<Project>, IReturnVoid
{
    public int Id { get; set; }        
}

