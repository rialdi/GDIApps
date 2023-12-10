using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(ProjectId), nameof(VersionNo))]
public class ProjectPlanVersion
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(Project))]
    public int ProjectId { get; set;}

    [Required]
    public int VersionNo { get; set;}

    public bool? IsActive { get; set; }
}

public class ProjectPlanVersionView : ProjectPlanVersion
{
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class QueryProjectPlanVersions : QueryDb<ProjectPlanVersion>{
    public int? ProjectId { get; set;}
}


[Tag("Projects")]
[ValidateIsAuthenticated]
public class CreateProjectPlanVersion : ICreateDb<ProjectPlanVersion>, IReturn<CRUDResponse>
{
    public int ProjectId { get; set; }
    public int AppUserId { get; set; }
    public int VersionNo { get; set;}
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class UpdateProjectPlanVersion : IPatchDb<ProjectPlanVersion>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public int ProjectId { get; set; }
    public int AppUserId { get; set; }
    public int VersionNo { get; set;}
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class DeleteProjectPlanVersion : IDeleteDb<Project>, IReturnVoid
{
    public int Id { get; set; }        
}
