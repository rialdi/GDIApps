using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(RoleName))]
public class AppRole 
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [StringLength(100)]    
    public string RoleName { get; set; } = string.Empty;

    [StringLength(400)]    
    public string? Description { get; set; }
}

[Tag("Apps")]
public class QueryAppRoles : QueryDb<AppRole> {
}

[RequiredRole("Admin")]
[Tag("Apps")]
public class CreateAppRole : ICreateDb<AppRole>, IReturn<CRUDResponse>
{
    public string RoleName { get; set; } = string.Empty;
    public string? Description { get; set; }
}

[RequiredRole("Admin")]
[Tag("Apps")]
public class UpdateAppRole : IPatchDb<AppRole>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public string RoleName { get; set; } = string.Empty;
    public string? Description { get; set; }
}

[RequiredRole("Admin")]
[Tag("Apps")]
public class DeleteAppRole : IDeleteDb<AppRole>, IReturnVoid
{
    public int Id { get; set; }
}
