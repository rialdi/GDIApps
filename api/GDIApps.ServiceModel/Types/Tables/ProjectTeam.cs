using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(ProjectId), nameof(AppUserId))]
public class ProjectTeam
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(Project))]
    public int ProjectId { get; set;}

    [Required]
    [References(typeof(AppUser))]
    public int AppUserId { get; set;}

    [Required]
    [StringLength(100)]
    public PROJECT_TEAM_ROLE ProjectTeamRole { get; set; } 

    [Required]
    [StringLength(100)]
    public CURRENCY_RATE ProjectTeamRoleCurrRate { get; set; } 

    [Required]
    public decimal ProjectTeamRoleRatePerDay { get; set; }
}

public class ProjectTeamView : ProjectTeam
{
    public string? UserName {get; set;}
    public string? FirstName {get; set;}
    public string? LastName {get; set;}
    public string? Email {get; set;}
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class QueryProjectTeams : QueryDb<ProjectTeam, ProjectTeamView>, IJoin<ProjectTeam, AppUser>{
    public int? ProjectId { get; set;}
}


[Tag("Projects")]
[ValidateIsAuthenticated]
public class CreateProjectTeam : ICreateDb<ProjectTeam>, IReturn<CRUDResponse>
{
    public int ProjectId { get; set; }
    public int AppUserId { get; set; }

    [ApiAllowableValues(typeof(PROJECT_TEAM_ROLE))]
    public PROJECT_TEAM_ROLE ProjectTeamRole { get; set; } 

    [ApiAllowableValues(typeof(CURRENCY_RATE))]
    public CURRENCY_RATE ProjectTeamRoleCurrRate { get; set; } 
    public decimal ProjectTeamRoleRatePerDay { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class UpdateProjectTeam : IPatchDb<ProjectTeam>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public int ProjectId { get; set; }
    public int AppUserId { get; set; }
    
    [ApiAllowableValues(typeof(PROJECT_TEAM_ROLE))]
    public PROJECT_TEAM_ROLE ProjectTeamRole { get; set; } 
    [ApiAllowableValues(typeof(CURRENCY_RATE))]
    public CURRENCY_RATE ProjectTeamRoleCurrRate { get; set; } 
    public decimal ProjectTeamRoleRatePerDay { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class DeleteProjectTeam : IDeleteDb<Project>, IReturnVoid
{
    public int Id { get; set; }        
}
