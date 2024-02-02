using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true,  nameof(Owner), nameof(OwnerId), nameof(IdentityType))]
public class IdentitiesNumber : AuditBase
{
    #nullable disable
    [AutoIncrement]
    public int Id { get; set; }  

    public IDENTITY_OWNER Owner { get; set; }
    public int OwnerId { get; set; }
    public IDENTITY_TYPE IdentityType { get; set; }
    public string IdentityNUmber { get; set; }
    public DateTime? PublishedDate { get; set; }
    public DateTime? ExpiredDate { get; set; }

    [Input(Type = "file"), UploadTo("identitiesNumber")]
    public string IdentityFile { get; set; }
    #nullable restore
}


[ValidateIsAuthenticated]
[Tag("IdentitiesNumbers")]
[AutoApply(Behavior.AuditQuery)]
public class QueryIdentitiesNumbers : QueryDb<IdentitiesNumber> 
{    
    #nullable disable
    public int OwnerId { get; set; }
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("IdentitiesNumbers")]
[AutoApply(Behavior.AuditCreate)]
public class CreateIdentitiesNumber : ICreateDb<IdentitiesNumber>, IReturn<CRUDResponse>
{  
    #nullable disable

    public int Id { get; set; }  

    [ApiAllowableValues(typeof(IDENTITY_OWNER))]
    public IDENTITY_OWNER Owner { get; set; }
    public int OwnerId { get; set; }
    [ApiAllowableValues(typeof(IDENTITY_TYPE))]
    public IDENTITY_TYPE IdentityType { get; set; }
    public string IdentityNUmber { get; set; }
    public DateTime? PublishedDate { get; set; }
    public DateTime? ExpiredDate { get; set; }

    [Input(Type = "file"), UploadTo("identitiesNumber")]
    public string IdentityFile { get; set; }
    
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("IdentitiesNumbers")]
[AutoApply(Behavior.AuditModify)]
public class UpdateIdentitiesNumber : IPatchDb<IdentitiesNumber>, IReturn<CRUDResponse>
{
    #nullable disable
    public int Id { get; set; } 
    [ApiAllowableValues(typeof(IDENTITY_OWNER))]
    public IDENTITY_OWNER Owner { get; set; }
    public int OwnerId { get; set; }
    [ApiAllowableValues(typeof(IDENTITY_TYPE))]
    public IDENTITY_TYPE IdentityType { get; set; }
    public string IdentityNUmber { get; set; }
    public DateTime? PublishedDate { get; set; }
    public DateTime? ExpiredDate { get; set; }

    [Input(Type = "file"), UploadTo("identitiesNumber")]
    public string IdentityFile { get; set; }
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("IdentitiesNumbers")]
public class DeleteIdentitiesNumber : IDeleteDb<IdentitiesNumber>, IReturnVoid
{
    public int Id { get; set; }        
}

