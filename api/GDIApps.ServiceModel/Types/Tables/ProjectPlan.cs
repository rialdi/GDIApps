// using System;
// using System.Collections.Generic;
// using ServiceStack;
// using ServiceStack.DataAnnotations;

// namespace GDIApps.ServiceModel.Types;

// [CompositeIndex(true, nameof(ProjectId), nameof(AppUserId))]
// public class ProjectPlan
// {
//     [AutoIncrement]
//     public int Id { get; set; }  

//     [Required]
//     [References(typeof(ProjectPlanVersion))]
//     public int ProjectPlanVersionId { get; set;}

//     [Required]
//     public string TaskCode { get; set;}

//     [Required]
//     public string TaskTitle { get; set;}


//     public DateTime StartDate { get; set; }
//     public DateTime EndDate { get; set; }
// }

// public class ProjectPlanView : ProjectPlan
// {
//     public string? UserName {get; set;}
//     public string? FirstName {get; set;}
//     public string? LastName {get; set;}
//     public string? Email {get; set;}
// }

// [ValidateIsAuthenticated]
// [Tag("Projects")]
// public class QueryProjectPlans : QueryDb<ProjectPlan, ProjectPlanView>, IJoin<ProjectPlan, AppUser>{
//     public int? ProjectId { get; set;}
// }


// [Tag("Projects")]
// [ValidateIsAuthenticated]
// public class CreateProjectPlan : ICreateDb<ProjectPlan>, IReturn<CRUDResponse>
// {
//     public int ProjectId { get; set; }
//     public int AppUserId { get; set; }

//     [ApiAllowableValues(typeof(PROJECT_TEAM_ROLE))]
//     public PROJECT_TEAM_ROLE ProjectPlanRole { get; set; } 

//     [ApiAllowableValues(typeof(CURRENCY_RATE))]
//     public CURRENCY_RATE ProjectPlanRoleCurrRate { get; set; } 
//     public decimal ProjectPlanRoleRatePerDay { get; set; }
// }

// [ValidateIsAuthenticated]
// [Tag("Projects")]
// public class UpdateProjectPlan : IPatchDb<ProjectPlan>, IReturn<CRUDResponse>
// {
//     public int Id { get; set; } 
//     public int ProjectId { get; set; }
//     public int AppUserId { get; set; }
    
//     [ApiAllowableValues(typeof(PROJECT_TEAM_ROLE))]
//     public PROJECT_TEAM_ROLE ProjectPlanRole { get; set; } 
//     [ApiAllowableValues(typeof(CURRENCY_RATE))]
//     public CURRENCY_RATE ProjectPlanRoleCurrRate { get; set; } 
//     public decimal ProjectPlanRoleRatePerDay { get; set; }
// }

// [ValidateIsAuthenticated]
// [Tag("Projects")]
// public class DeleteProjectPlan : IDeleteDb<Project>, IReturnVoid
// {
//     public int Id { get; set; }        
// }
