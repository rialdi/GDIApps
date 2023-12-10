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

public enum APP_MENU_TYPE {
    ROOT,
    EMPLOYEE_DASHBOARD,
    GUEST_DASHBOARD
}

public enum PROJECT_TEAM_ROLE {
    DEVELOPER,
    QA,
    TEAM_LEAD,
    PM
}

public enum CURRENCY_RATE {
    USD,
    IDR
}

// public enum PROJECT_TASK_STATUS {
//     YET_TO_START,
//     IN_PROGRESS,
//     DEVELOPMENT,
//     TESTING,
//     COMPLETED,
// }

public enum TASK_STATUS {
    YET_TO_START,
    IN_PROGRESS,
    COMPLETED,
}

public enum REVIEW_TYPE {
    TEAM_LEADER,
    QA,
    TEAM_MEMBER
}
public enum REVIEW_QUESTION_CATEGORY {
    EMPLOYEE_COMPETENCY,
    KPI
}


public enum PROJECT_ROLE_NAME {
    PROJECT_MANAGER,
    BUSINESS_ANALYST,
    SYSTEM_ANALYST,
    SENIOR_DEVELOPER,
    JUNIOR_DEVELOPER,
    TECHNICAL_WRITER
}