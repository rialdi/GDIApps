
using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(AppRoleId), nameof(AppMenuId), nameof(Sequence))]
public class AppMenu 
{
    [AutoIncrement]
    public int Id { get; set; }  

    [References(typeof(AppRole))]
    public int? AppRoleId { get; set;}

    // [Required]
    // [StringLength(100)]    
    // public APP_MENU_TYPE MenuType { get; set; }

    [References(typeof(AppMenu))]
    public int? AppMenuId { get; set;}

    [Required]   
    public int Sequence { get; set; }

    [Required]
    [StringLength(100)]    
    public string Name { get; set; } = string.Empty;
    public string? To { get; set; }  
    public string? Icon { get; set; }
    public bool IsActive { get; set; }

    [Reference]
    public List<AppMenu>? Sub { get; set; }
}

// public class AppMenuView
// {
//     public int Id { get; set; }  
//     public APP_MENU_TYPE MenuType { get; set; }
//     public int? AppMenuId { get; set;}
//     public int Sequence { get; set; }
//     public string Name { get; set; } = string.Empty;
//     public string? To { get; set; }  
//     public string? Icon { get; set; }
//     public bool IsActive { get; set; }

//     [Reference]
//     public List<AppMenu>? Sub { get; set; }

//     // public APP_MENU_TYPE AppMenuMenuType { get; set; }
//     // public int AppMenuSequence { get; set; }
//     // public string AppMenuName { get; set; } = string.Empty;
//     // public string? AppMenuTo { get; set; }  
//     // public string? AppMenuIcon { get; set; }
//     // public bool AppMenuIsActive { get; set; }
// }

[Tag("Apps")]
// [AutoApply(Behavior.AuditQuery)]
public class QueryAppMenus : QueryDb<AppMenu> {
// , ILeftJoin<AppMenu, AppMenuRole>{
    // public string[]? MenuTypes { get; set; }

    [AutoDefault(Eval = null)] 
    public int[]? Ids { get; set; }
    
    [AutoDefault(Eval = null)]
    public string[]? Names { get; set; }
    
    [AutoDefault(Eval = null)]
    public int? Sequence { get; set; }

    // [AutoDefault(Eval = null)]
    // public string[]? AppMenuRoleRoles { get; set; }
    // [AutoDefault(Eval = null)]
    // public string? SubName { get; set; }
}

[Tag("Apps")]
public class GetAppMenuByRole : IReturn<CRUDResponse> {
    public string RoleName { get; set; } = string.Empty;
}

[RequiredRole("Admin")]
[Tag("Apps")]
// [AutoApply(Behavior.AuditCreate)]
public class CreateAppMenu : ICreateDb<AppMenu>, IReturn<CRUDResponse>
{
    [ApiAllowableValues(typeof(APP_MENU_TYPE))]
    public APP_MENU_TYPE MenuType { get; set; }
    public int? AppMenuId { get; set;}
    public int Sequence { get; set; }
    public string Name { get; set; } = string.Empty;

    [AutoDefault(Eval = null)]
    public string? To { get; set; }

    [AutoDefault(Eval = null)]
    public string? Icon { get; set; }
    
    [AutoDefault(Eval = null)]
    // public string? Role { get; set; }
    public bool? IsActive { get; set; }
}

[RequiredRole("Admin")]
[Tag("Apps")]
// [AutoApply(Behavior.AuditModify)]
public class UpdateAppMenu : IPatchDb<AppMenu>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    [ApiAllowableValues(typeof(APP_MENU_TYPE))]
    public APP_MENU_TYPE MenuType { get; set; }
    [AutoDefault(Eval = null)]
    public int? AppMenuId { get; set;}
    public int Sequence { get; set; }
    public string? Name { get; set; }
    public string? To { get; set; }
    public string? Icon { get; set; }
    // public string? Role { get; set; }
    public bool? IsActive { get; set; }
}

[RequiredRole("Admin")]
[Tag("Apps")]
public class DeleteAppMenu : IDeleteDb<AppMenu>, IReturnVoid
{
    public int Id { get; set; }
}











// [CompositeIndex(true, nameof(AppMenuId), nameof(Role))]
// public class AppMenuRole
// {
//     [AutoIncrement]
//     public int Id { get; set; }  

//     [Required]
//     [References(typeof(AppMenu))]
//     public int AppMenuId { get; set;}

//     [Required]
//     public string Role { get; set;} = string.Empty;
// }

// [Tag("AppMenus")]
// public class QueryAppMenuRoles : QueryDb<AppMenuRole> {
//     public int? Id { get; set; } 
//     public string[]? Roles { get; set; }  
// }

// [RequiredRole("Admin")]
// [Tag("AppMenus")]
// public class CreateAppMenuRole : ICreateDb<AppMenuRole>, IReturn<CRUDResponse>
// {
//     public int? AppMenuId { get; set;}
//     public string Role { get; set; } = string.Empty;
// }

// [RequiredRole("Admin")]
// [Tag("AppMenus")]
// public class UpdateAppMenuRole : IPatchDb<AppMenuRole>, IReturn<CRUDResponse>
// {
//     public int? AppMenuId { get; set;}
//     public string Role { get; set; } = string.Empty;
// }

// [RequiredRole("Admin")]
// [Tag("AppMenus")]
// public class DeleteAppMenuRole : IDeleteDb<AppMenuRole>, IReturnVoid
// {
//     public int Id { get; set; }        
// }



// [CompositeIndex(true, nameof(AppMenuId), nameof(Sequence))]
// public class AppSubMenu : AuditBase
// {
//     [AutoIncrement]
//     public int Id { get; set; }

//     [Required]
//     [References(typeof(AppMenu))]
//     public int AppMenuId { get; set;}

//     [Required]   
//     public int Sequence { get; set; }

//     [Required]
//     [StringLength(100)]    
//     public string Name { get; set; } = string.Empty;
 
//     public string? To { get; set; }  
//     public string? Icon { get; set; }
//     public string? Roles { get; set; }

//     public bool IsActive { get; set; }

// }

// [RequiredRole("Admin")]
// [Tag("AppMenus")]
// [AutoApply(Behavior.AuditCreate)]
// public class CreateAppSubMenu : ICreateDb<AppSubMenu>, IReturn<CRUDResponse>
// {
//     public int AppMenuId { get; set;}
//     public int Sequence { get; set; }
//     public string Name { get; set; } = string.Empty;
//     public string To { get; set; } = string.Empty;
//     public string Icon { get; set; } = string.Empty;
//     public string? Roles { get; set; }
//     public bool? IsMain { get; set; }
// }

// [RequiredRole("Admin")]
// [Tag("AppMenus")]
// [AutoApply(Behavior.AuditModify)]
// public class UpdateAppSubMenu : IPatchDb<AppSubMenu>, IReturn<CRUDResponse>
// {
//     public int Id { get; set; } 
//     public int AppMenuId { get; set;}
//     public int? Sequence { get; set; }
//     public string? Name { get; set; }
//     public string? To { get; set; }
//     public string? Icon { get; set; }
    
//     [AutoDefault(Eval = null)]
//     public string? Roles { get; set; }
//     public bool? IsMain { get; set; }
// }

// [RequiredRole("Admin")]
// [Tag("AppMenus")]
// public class DeleteAppSubMenu : IDeleteDb<AppSubMenu>, IReturnVoid
// {
//     public int Id { get; set; }        
// }