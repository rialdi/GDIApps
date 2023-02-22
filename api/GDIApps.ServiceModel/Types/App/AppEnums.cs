using ServiceStack;
namespace GDIApps.ServiceModel.Types;

public enum Department
{
    None,
    QualityAssurance,
    Developer,
    Management,
}
public enum EMAIL_TEMPLATE_CODE
{
    APPUSER_EMAIL_CONFIRM,
    APPUSER_RESET_PASSWORD,
    APPUSER_REGISTRATION
}

public enum LOOKUPTYPE
{
    STATUS, PRIORITY, BANK
}

public enum INVOICE_STATUS
{
    DRAFT, SUBMITTED, INPROGRESS, COMPLETED, REJECTED
}

public enum GENDER { MALE, FEMALE }