using ServiceStack;
namespace GDIApps.ServiceModel.Types;
public class CRUDResponse
{
    public int Id { get; set; } // Id is auto populated with RDBMS generated Id
    public ResponseStatus ResponseStatus { get; set; } = new ResponseStatus();
}

public class ContactUsEmailResponse
{
    public string Email { get; set; } = string.Empty;
    public ResponseStatus ResponseStatus { get; set; } = new ResponseStatus();
}