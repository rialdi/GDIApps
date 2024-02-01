/* Options:
Date: 2024-02-01 15:19:19
Version: 6.110
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: https://localhost:5005

//GlobalNamespace: 
MakePropertiesOptional: True
//AddServiceStackTypes: True
//AddResponseStatus: False
//AddImplicitVersion: 
//AddDescriptionAsComments: True
//IncludeTypes: 
//ExcludeTypes: 
//DefaultImports: 
*/


export interface IReturn<T>
{
    createResponse(): T;
}

export interface IReturnVoid
{
    createResponse(): void;
}

export interface IPost
{
}

export interface IHasSessionId
{
    sessionId?: string;
}

export interface IHasBearerToken
{
    bearerToken?: string;
}

export interface IDeleteDb<Table>
{
}

export interface ICreateDb<Table>
{
}

export interface IPatchDb<Table>
{
}

export enum EMAIL_TEMPLATE_CODE
{
    APPUSER_EMAIL_CONFIRM = 'APPUSER_EMAIL_CONFIRM',
    APPUSER_RESET_PASSWORD = 'APPUSER_RESET_PASSWORD',
    APPUSER_REGISTRATION = 'APPUSER_REGISTRATION',
}

// @DataContract
export class AuditBase
{
    // @DataMember(Order=1)
    public createdDate?: string;

    // @DataMember(Order=2)
    // @Required()
    public createdBy?: string;

    // @DataMember(Order=3)
    public modifiedDate?: string;

    // @DataMember(Order=4)
    // @Required()
    public modifiedBy?: string;

    // @DataMember(Order=5)
    public deletedDate?: string;

    // @DataMember(Order=6)
    public deletedBy?: string;

    public constructor(init?: Partial<AuditBase>) { (Object as any).assign(this, init); }
}

export class InvoiceAttachment
{
    public id?: number;
    // @References("typeof(GDIApps.ServiceModel.Types.Invoice)")
    public invoiceId?: number;

    // @Required()
    public fileName?: string;

    public attachmentUrl?: string;

    public constructor(init?: Partial<InvoiceAttachment>) { (Object as any).assign(this, init); }
}

export class Invoice extends AuditBase
{
    public id?: number;
    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.Client)")
    public clientId?: number;

    // @References("typeof(GDIApps.ServiceModel.Types.CContract)")
    public cContractId?: number;

    // @References("typeof(GDIApps.ServiceModel.Types.CBank)")
    public cBankId?: number;

    // @References("typeof(GDIApps.ServiceModel.Types.CAddress)")
    public cAddressId?: number;

    // @Required()
    // @StringLength(100)
    public invoiceNo?: string;

    // @StringLength(100)
    public paymentTermDays?: number;

    // @Required()
    public invoiceDate?: string;

    // @StringLength(1000)
    public description?: string;

    // @StringLength(100)
    public poNumber?: string;

    // @StringLength(100)
    public vat?: string;

    // @StringLength(100)
    public wht?: string;

    public totalAmount?: number;
    public vatAmount?: number;
    public invoiceStatus?: string;
    public attachments?: InvoiceAttachment[];

    public constructor(init?: Partial<Invoice>) { super(init); (Object as any).assign(this, init); }
}

export class ProjectPlan extends AuditBase
{
    public id?: number;
    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.Project)")
    public projectId?: number;

    // @Required()
    public versionNo?: number;

    // @Required()
    public taskLevel?: number;

    // @Required()
    public taskNo?: number;

    // @Required()
    public parentCode?: string;

    // @Required()
    public taskCode?: string;

    public dependecyTaskCode?: string;
    // @References("typeof(GDIApps.ServiceModel.Types.AppUser)")
    public appUserId?: number;

    // @Required()
    public taskTitle?: string;

    public durationDays?: number;
    public startDate?: string;
    public endDate?: string;
    public completedPercentage?: number;
    public resourceCost?: number;
    public hasChild?: boolean;

    public constructor(init?: Partial<ProjectPlan>) { super(init); (Object as any).assign(this, init); }
}

// @DataContract
export class QueryBase
{
    // @DataMember(Order=1)
    public skip?: number;

    // @DataMember(Order=2)
    public take?: number;

    // @DataMember(Order=3)
    public orderBy?: string;

    // @DataMember(Order=4)
    public orderByDesc?: string;

    // @DataMember(Order=5)
    public include?: string;

    // @DataMember(Order=6)
    public fields?: string;

    // @DataMember(Order=7)
    public meta?: { [index: string]: string; };

    public constructor(init?: Partial<QueryBase>) { (Object as any).assign(this, init); }
}

export class QueryDb_1<T> extends QueryBase
{

    public constructor(init?: Partial<QueryDb_1<T>>) { super(init); (Object as any).assign(this, init); }
}

export class AppMenu
{
    public id?: number;
    // @References("typeof(GDIApps.ServiceModel.Types.AppRole)")
    public appRoleId?: number;

    // @References("typeof(GDIApps.ServiceModel.Types.AppMenu)")
    public appMenuId?: number;

    // @Required()
    public sequence?: number;

    // @Required()
    // @StringLength(100)
    public name?: string;

    public to?: string;
    public icon?: string;
    public isActive?: boolean;
    public sub?: AppMenu[];

    public constructor(init?: Partial<AppMenu>) { (Object as any).assign(this, init); }
}

export class AppRole
{
    public id?: number;
    // @Required()
    // @StringLength(100)
    public roleName?: string;

    // @StringLength(400)
    public description?: string;

    public constructor(init?: Partial<AppRole>) { (Object as any).assign(this, init); }
}

export class Country
{
    public id?: number;
    public parent?: number;
    public name?: string;
    public latitude?: number;
    public longitude?: number;

    public constructor(init?: Partial<Country>) { (Object as any).assign(this, init); }
}

export class Province
{
    public id?: number;
    // @References("typeof(GDIApps.ServiceModel.Types.Country)")
    public countryId?: number;

    public name?: string;
    public latitude?: number;
    public longitude?: number;
    public postal?: string;

    public constructor(init?: Partial<Province>) { (Object as any).assign(this, init); }
}

export class City
{
    public id?: number;
    // @References("typeof(GDIApps.ServiceModel.Types.Province)")
    public provinceId?: number;

    public name?: string;
    public latitude?: number;
    public longitude?: number;
    public postal?: string;

    public constructor(init?: Partial<City>) { (Object as any).assign(this, init); }
}

export class District
{
    public id?: number;
    // @References("typeof(GDIApps.ServiceModel.Types.City)")
    public cityId?: number;

    public name?: string;
    public latitude?: number;
    public longitude?: number;
    public postal?: string;

    public constructor(init?: Partial<District>) { (Object as any).assign(this, init); }
}

export class Village
{
    public id?: number;
    // @References("typeof(GDIApps.ServiceModel.Types.District)")
    public districtId?: number;

    public name?: string;
    public latitude?: number;
    public longitude?: number;
    public postal?: string;

    public constructor(init?: Partial<Village>) { (Object as any).assign(this, init); }
}

export class MasterBank
{
    public id?: number;
    public bankName?: string;
    public swiftCode?: string;

    public constructor(init?: Partial<MasterBank>) { (Object as any).assign(this, init); }
}

export class QueryDb_2<From, Into> extends QueryBase
{

    public constructor(init?: Partial<QueryDb_2<From, Into>>) { super(init); (Object as any).assign(this, init); }
}

export class CAddress extends AuditBase
{
    public id?: number;
    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.Client)")
    public clientId?: number;

    // @Required()
    // @StringLength(100)
    public addressName?: string;

    // @StringLength(100)
    public country?: string;

    // @StringLength(100)
    public province?: string;

    // @StringLength(100)
    public city?: string;

    // @StringLength(100)
    public district?: string;

    // @StringLength(100)
    public village?: string;

    // @StringLength(300)
    public address1?: string;

    // @StringLength(300)
    public address2?: string;

    // @StringLength(100)
    public postalCode?: string;

    // @StringLength(100)
    public phoneNo?: string;

    public isMain?: boolean;

    public constructor(init?: Partial<CAddress>) { super(init); (Object as any).assign(this, init); }
}

export class CAddressView extends CAddress
{
    public clientCode?: string;
    public clientName?: string;

    public constructor(init?: Partial<CAddressView>) { super(init); (Object as any).assign(this, init); }
}

export class CBank extends AuditBase
{
    public id?: number;
    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.Client)")
    public clientId?: number;

    // @Required()
    // @StringLength(100)
    public bankName?: string;

    // @Required()
    // @StringLength(100)
    public accountName?: string;

    // @Required()
    // @StringLength(100)
    public accountNo?: string;

    // @Required()
    // @StringLength(100)
    public currency?: string;

    // @StringLength(100)
    public swiftCode?: string;

    public isMain?: boolean;

    public constructor(init?: Partial<CBank>) { super(init); (Object as any).assign(this, init); }
}

export class CBankView extends CBank
{
    public clientCode?: string;
    public clientName?: string;
    // @Ignore()
    public bankDisplay?: string;

    public constructor(init?: Partial<CBankView>) { super(init); (Object as any).assign(this, init); }
}

export class CContract extends AuditBase
{
    public id?: number;
    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.Client)")
    public clientId?: number;

    // @Required()
    // @StringLength(100)
    public contractNo?: string;

    // @Required()
    // @StringLength(1000)
    public description?: string;

    public startDate?: string;
    public endDate?: string;
    public totalAmount?: number;
    // @Required()
    // @StringLength(100)
    public currency?: string;

    public isActive?: boolean;
    // @StringLength(100)
    public pic?: string;

    public remainingAmount?: number;
    public paymentTermDays?: number;

    public constructor(init?: Partial<CContract>) { super(init); (Object as any).assign(this, init); }
}

export class CContractView extends CContract
{
    public clientCode?: string;
    public clientName?: string;

    public constructor(init?: Partial<CContractView>) { super(init); (Object as any).assign(this, init); }
}

export class Project extends AuditBase
{
    public id?: number;
    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.Client)")
    public clientId?: number;

    // @References("typeof(GDIApps.ServiceModel.Types.CContract)")
    public cContractId?: number;

    // @Required()
    // @StringLength(100)
    public code?: string;

    // @Required()
    // @StringLength(100)
    public name?: string;

    // @StringLength(1000)
    public description?: string;

    // @StringLength(100)
    public projectOwner?: string;

    // @StringLength(100)
    public projectManager?: string;

    public nominalValue?: number;
    public durationDays?: number;
    public estimatedStartDate?: string;
    public estimatedEndDate?: string;
    public actualtartDate?: string;
    public actualEndDate?: string;
    public isActive?: boolean;

    public constructor(init?: Partial<Project>) { super(init); (Object as any).assign(this, init); }
}

export class Client extends AuditBase
{
    public id?: number;
    // @Required()
    // @StringLength(100)
    public code?: string;

    // @Required()
    // @StringLength(100)
    public name?: string;

    // @Required()
    // @StringLength(15)
    public phoneNo?: string;

    // @Required()
    // @StringLength(100)
    public email?: string;

    // @Required()
    // @StringLength(1000)
    public description?: string;

    public isActive?: boolean;
    public projectList?: Project[];
    public contractList?: CContract[];

    public constructor(init?: Partial<Client>) { super(init); (Object as any).assign(this, init); }
}

export class ContactUs extends AuditBase
{
    public id?: number;
    // @Required()
    public name?: string;

    // @Required()
    public email?: string;

    // @Required()
    public phoneNumber?: string;

    // @Required()
    public message?: string;

    public constructor(init?: Partial<ContactUs>) { super(init); (Object as any).assign(this, init); }
}

export enum PROJECT_ROLE_NAME
{
    PROJECT_MANAGER = 'PROJECT_MANAGER',
    BUSINESS_ANALYST = 'BUSINESS_ANALYST',
    SYSTEM_ANALYST = 'SYSTEM_ANALYST',
    SENIOR_DEVELOPER = 'SENIOR_DEVELOPER',
    JUNIOR_DEVELOPER = 'JUNIOR_DEVELOPER',
    TECHNICAL_WRITER = 'TECHNICAL_WRITER',
}

export class CRoleRate extends AuditBase
{
    public id?: number;
    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.Client)")
    public clientId?: number;

    // @Required()
    // @StringLength(100)
    public projectRoleName?: PROJECT_ROLE_NAME;

    // @Required()
    // @StringLength(100)
    public currency?: string;

    // @Required()
    public rateInDay?: number;

    public isActive?: boolean;

    public constructor(init?: Partial<CRoleRate>) { super(init); (Object as any).assign(this, init); }
}

export class CRoleRateView extends CRoleRate
{
    public clientCode?: string;
    public clientName?: string;

    public constructor(init?: Partial<CRoleRateView>) { super(init); (Object as any).assign(this, init); }
}

export class DailyScrumMeeting extends AuditBase
{
    public id?: number;
    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.AppUser)")
    public appUserId?: number;

    // @Required()
    public meetingDate?: string;

    // @Required()
    // @StringLength(4000)
    public blockers?: string;

    // @Required()
    // @StringLength(4000)
    public whatDidYouDoYesterday?: string;

    // @Required()
    // @StringLength(4000)
    public whatGoalsToday?: string;

    public constructor(init?: Partial<DailyScrumMeeting>) { super(init); (Object as any).assign(this, init); }
}

/** @description Emails */
export class Email extends AuditBase
{
    // @Required()
    public id?: number;

    // @Required()
    public code?: EMAIL_TEMPLATE_CODE;

    public from?: string;
    // @Required()
    public to?: string;

    // @Required()
    public subject?: string;

    // @Required()
    // @StringLength(2147483647)
    public body?: string;

    public sendStatusMessage?: string;

    public constructor(init?: Partial<Email>) { super(init); (Object as any).assign(this, init); }
}

export class EmailTemplate extends AuditBase
{
    public id?: number;
    // @Required()
    // @StringLength(100)
    public code?: EMAIL_TEMPLATE_CODE;

    // @Required()
    // @StringLength(1000)
    public description?: string;

    public isActive?: boolean;
    // @Required()
    // @StringLength(2147483647)
    public emailTemplateText?: string;

    // @Required()
    // @StringLength(2147483647)
    public htmlTemplate?: string;

    public constructor(init?: Partial<EmailTemplate>) { super(init); (Object as any).assign(this, init); }
}

export enum REVIEW_TYPE
{
    TEAM_LEADER = 'TEAM_LEADER',
    QA = 'QA',
    TEAM_MEMBER = 'TEAM_MEMBER',
}

export enum REVIEW_QUESTION_CATEGORY
{
    EMPLOYEE_COMPETENCY = 'EMPLOYEE_COMPETENCY',
    KPI = 'KPI',
}

export class EmployeeReviewDetail
{
    public id?: number;
    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.EmployeeReview)")
    public employeeReviewId?: number;

    // @Required()
    public reviewType?: REVIEW_TYPE;

    // @Required()
    public category?: REVIEW_QUESTION_CATEGORY;

    // @Required()
    public no?: number;

    // @Required()
    // @StringLength(4000)
    public question?: string;

    // @StringLength(4000)
    public questionValue?: number;

    public constructor(init?: Partial<EmployeeReviewDetail>) { (Object as any).assign(this, init); }
}

export class EmployeeReview extends AuditBase
{
    public id?: number;
    // @Required()
    public periodYear?: number;

    // @Required()
    public periodQuarter?: string;

    // @Required()
    public reviewDate?: string;

    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.AppUser)")
    public reviewedEmployeeId?: number;

    public reviewedEmployeeUserName?: string;
    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.AppUser)")
    public reviewerId?: number;

    public reviewerUserName?: string;
    public reviewNotes?: string;
    public reviewDetailList?: EmployeeReviewDetail[];

    public constructor(init?: Partial<EmployeeReview>) { super(init); (Object as any).assign(this, init); }
}

export class InvoiceView
{
    public id?: number;
    public invoiceNo?: string;
    public paymentTermDays?: number;
    public invoiceDate?: string;
    public description?: string;
    public poNumber?: string;
    public vat?: string;
    public wht?: string;
    public totalAmount?: number;
    public vatAmount?: number;
    public invoiceStatus?: string;
    public clientId?: number;
    public clientCode?: string;
    public clientName?: string;
    public cContractId?: number;
    public contractNo?: string;
    public cContractDescription?: string;
    public cBankId?: number;
    public cBankBankName?: string;
    public cBankAccountName?: string;
    public cBankAccountNo?: string;
    public cAddressId?: number;
    public cAddressAddressName?: string;
    public cAddressCountry?: string;
    public cAddressProvince?: string;
    public cAddressCity?: string;
    public cAddressDistrict?: string;
    public cAddressVillage?: string;
    public cAddressAddress1?: string;
    public cAddressAddress2?: string;
    public cAddressPostalCode?: string;
    public cAddressPhoneNo?: string;

    public constructor(init?: Partial<InvoiceView>) { (Object as any).assign(this, init); }
}

export class InvoiceDetail extends AuditBase
{
    public id?: number;
    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.Invoice)")
    public invoiceId?: number;

    // @Required()
    // @StringLength(100)
    public no?: string;

    // @Required()
    // @StringLength(1000)
    public description?: string;

    // @Required()
    public amount?: number;

    public constructor(init?: Partial<InvoiceDetail>) { super(init); (Object as any).assign(this, init); }
}

export enum LOOKUPTYPE
{
    STATUS = 'STATUS',
    PRIORITY = 'PRIORITY',
    BANK = 'BANK',
}

export class Lookup extends AuditBase
{
    public id?: number;
    // @Required()
    // @StringLength(100)
    public lookupType?: LOOKUPTYPE;

    // @Required()
    // @StringLength(100)
    public lookupValue?: string;

    // @Required()
    // @StringLength(1000)
    public lookupText?: string;

    public isActive?: boolean;
    // @Ignore()
    public lookupDisplay?: string;

    public constructor(init?: Partial<Lookup>) { super(init); (Object as any).assign(this, init); }
}

export enum TASK_STATUS
{
    BACKLOG = 'BACKLOG',
    YET_TO_START = 'YET_TO_START',
    IN_PROGRESS = 'IN_PROGRESS',
    IN_REVIEW = 'IN_REVIEW',
    TESTING = 'TESTING',
    COMPLETED = 'COMPLETED',
}

export class OtherTask extends AuditBase
{
    public id?: number;
    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.AppUser)")
    public appUserId?: number;

    // @Required()
    // @StringLength(1000)
    public taskName?: string;

    // @StringLength(4000)
    public taskDesc?: string;

    // @Required()
    public durationDays?: number;

    public planStart?: string;
    public planEnd?: string;
    public actualStart?: string;
    public actualEnd?: string;
    // @Required()
    public completed?: number;

    // @Required()
    // @StringLength(100)
    public status?: TASK_STATUS;

    public constructor(init?: Partial<OtherTask>) { super(init); (Object as any).assign(this, init); }
}

export class ProjectView extends Project
{
    public clientCode?: string;
    public clientName?: string;
    public cContractContractNo?: string;

    public constructor(init?: Partial<ProjectView>) { super(init); (Object as any).assign(this, init); }
}

export class ProjectDoc
{
    public id?: number;
    // @References("typeof(GDIApps.ServiceModel.Types.Project)")
    public projectId?: number;

    // @Required()
    public fileName?: string;

    public attachmentUrl?: string;

    public constructor(init?: Partial<ProjectDoc>) { (Object as any).assign(this, init); }
}

export class ProjectPlanView extends ProjectPlan
{
    // @Ignore()
    public codeTitle?: string;

    public constructor(init?: Partial<ProjectPlanView>) { super(init); (Object as any).assign(this, init); }
}

export class ProjectPlanVersion
{
    public id?: number;
    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.Project)")
    public projectId?: number;

    // @Required()
    public versionNo?: number;

    public isActive?: boolean;

    public constructor(init?: Partial<ProjectPlanVersion>) { (Object as any).assign(this, init); }
}

export enum TASK_LABEL
{
    BUG = 'BUG',
    ENHANCEMENT = 'ENHANCEMENT',
    DOCUMENTATION = 'DOCUMENTATION',
    WONTFIX = 'WONTFIX',
    QUESTION = 'QUESTION',
    DUPLICATE = 'DUPLICATE',
    GOODFIRSTISSUE = 'GOODFIRSTISSUE',
    INVALID = 'INVALID',
}

export class ProjectTask extends AuditBase
{
    public id?: number;
    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.Project)")
    public projectId?: number;

    // @References("typeof(GDIApps.ServiceModel.Types.ProjectPlan)")
    public projectPlanId?: number;

    // @Required()
    public no?: number;

    // @Required()
    // @StringLength(100)
    public taskCode?: string;

    // @Required()
    // @StringLength(200)
    public taskName?: string;

    // @StringLength(1000)
    public description?: string;

    public durationDays?: number;
    public dueDate?: string;
    public planStart?: string;
    public planEnd?: string;
    public actualStart?: string;
    public actualEnd?: string;
    // @Required()
    public completed?: number;

    // @Required()
    // @StringLength(100)
    public status?: TASK_STATUS;

    // @StringLength(1000)
    public taskLabel?: TASK_LABEL;

    // @References("typeof(GDIApps.ServiceModel.Types.ProjectTeam)")
    public projectTeamId?: number;

    public isArchived?: boolean;

    public constructor(init?: Partial<ProjectTask>) { super(init); (Object as any).assign(this, init); }
}

export class ProjectTaskView extends ProjectTask
{
    public projectCode?: string;
    public projectName?: string;
    public projectIsActive?: boolean;

    public constructor(init?: Partial<ProjectTaskView>) { super(init); (Object as any).assign(this, init); }
}

export class ProjectTaskDoc
{
    public id?: number;
    // @References("typeof(GDIApps.ServiceModel.Types.ProjectTask)")
    public projectTaskId?: number;

    // @Required()
    public fileName?: string;

    public attachmentUrl?: string;

    public constructor(init?: Partial<ProjectTaskDoc>) { (Object as any).assign(this, init); }
}

export enum PROJECT_TEAM_ROLE
{
    DEVELOPER = 'DEVELOPER',
    QA = 'QA',
    TEAM_LEAD = 'TEAM_LEAD',
    PM = 'PM',
}

export enum CURRENCY_RATE
{
    USD = 'USD',
    IDR = 'IDR',
}

export class ProjectTeam
{
    public id?: number;
    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.Project)")
    public projectId?: number;

    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.AppUser)")
    public appUserId?: number;

    // @Required()
    // @StringLength(100)
    public projectTeamRole?: PROJECT_TEAM_ROLE;

    // @Required()
    // @StringLength(100)
    public projectTeamRoleCurrRate?: CURRENCY_RATE;

    // @Required()
    public projectTeamRoleRatePerDay?: number;

    public constructor(init?: Partial<ProjectTeam>) { (Object as any).assign(this, init); }
}

export class ProjectTeamView extends ProjectTeam
{
    public userName?: string;
    public firstName?: string;
    public lastName?: string;
    public email?: string;

    public constructor(init?: Partial<ProjectTeamView>) { super(init); (Object as any).assign(this, init); }
}

export class ReviewMasterQuestion extends AuditBase
{
    public id?: number;
    // @Required()
    public reviewType?: REVIEW_TYPE;

    // @Required()
    public category?: REVIEW_QUESTION_CATEGORY;

    // @Required()
    public no?: number;

    // @Required()
    // @StringLength(4000)
    public question?: string;

    // @StringLength(4000)
    public description?: string;

    public constructor(init?: Partial<ReviewMasterQuestion>) { super(init); (Object as any).assign(this, init); }
}

export class TimeSheet extends AuditBase
{
    public id?: number;
    // @Required()
    public tsDate?: string;

    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.AppUser)")
    public appUserId?: number;

    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.Client)")
    public clientId?: number;

    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.Project)")
    public projectId?: number;

    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.ProjectTask)")
    public projectTaskId?: number;

    // @Required()
    // @StringLength(1000)
    public notes?: string;

    public constructor(init?: Partial<TimeSheet>) { super(init); (Object as any).assign(this, init); }
}

export class TimeSheetView extends TimeSheet
{
    public appUserUserName?: string;
    public appUserFullName?: string;
    public clientCode?: string;
    public clientName?: string;
    public projectCode?: string;
    public projectName?: string;

    public constructor(init?: Partial<TimeSheetView>) { super(init); (Object as any).assign(this, init); }
}

// @DataContract
export class ResponseError
{
    // @DataMember(Order=1)
    public errorCode?: string;

    // @DataMember(Order=2)
    public fieldName?: string;

    // @DataMember(Order=3)
    public message?: string;

    // @DataMember(Order=4)
    public meta?: { [index: string]: string; };

    public constructor(init?: Partial<ResponseError>) { (Object as any).assign(this, init); }
}

export enum Department
{
    None = 'None',
    QualityAssurance = 'QualityAssurance',
    Developer = 'Developer',
    Management = 'Management',
}

export class UserAuth
{
    public id?: number;
    public userName?: string;
    public email?: string;
    public primaryEmail?: string;
    public phoneNumber?: string;
    public firstName?: string;
    public lastName?: string;
    public displayName?: string;
    public company?: string;
    public birthDate?: string;
    public birthDateRaw?: string;
    public address?: string;
    public address2?: string;
    public city?: string;
    public state?: string;
    public country?: string;
    public culture?: string;
    public fullName?: string;
    public gender?: string;
    public language?: string;
    public mailAddress?: string;
    public nickname?: string;
    public postalCode?: string;
    public timeZone?: string;
    public salt?: string;
    public passwordHash?: string;
    public digestHa1Hash?: string;
    public roles?: string[];
    public permissions?: string[];
    public createdDate?: string;
    public modifiedDate?: string;
    public invalidLoginAttempts?: number;
    public lastLoginAttempt?: string;
    public lockedDate?: string;
    public recoveryToken?: string;
    public refId?: number;
    public refIdStr?: string;
    public meta?: { [index: string]: string; };

    public constructor(init?: Partial<UserAuth>) { (Object as any).assign(this, init); }
}

// @DataContract
export class ResponseStatus
{
    // @DataMember(Order=1)
    public errorCode?: string;

    // @DataMember(Order=2)
    public message?: string;

    // @DataMember(Order=3)
    public stackTrace?: string;

    // @DataMember(Order=4)
    public errors?: ResponseError[];

    // @DataMember(Order=5)
    public meta?: { [index: string]: string; };

    public constructor(init?: Partial<ResponseStatus>) { (Object as any).assign(this, init); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class AppUser extends UserAuth
{
    public profileUrl?: string;
    public employeeId?: string;
    public department?: Department;
    public isArchived?: boolean;
    public archivedDate?: string;

    public constructor(init?: Partial<AppUser>) { super(init); (Object as any).assign(this, init); }
}

// @Route("/uploaduserprofile/{Id}")
// @ValidateRequest(Validator="IsAuthenticated")
export class UploadUserProfile implements IReturn<UploadUserProfile>, IPost
{
    public email?: string;
    public profileUrl?: string;

    public constructor(init?: Partial<UploadUserProfile>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UploadUserProfile'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new UploadUserProfile(); }
}

export class ContactUsEmailResponse
{
    public email?: string;
    public responseStatus?: ResponseStatus;

    public constructor(init?: Partial<ContactUsEmailResponse>) { (Object as any).assign(this, init); }
}

export class CRUDResponse
{
    public id?: number;
    public responseStatus?: ResponseStatus;

    public constructor(init?: Partial<CRUDResponse>) { (Object as any).assign(this, init); }
}

// @DataContract
export class RegisterResponse implements IHasSessionId, IHasBearerToken
{
    // @DataMember(Order=1)
    public userId?: string;

    // @DataMember(Order=2)
    public sessionId?: string;

    // @DataMember(Order=3)
    public userName?: string;

    // @DataMember(Order=4)
    public referrerUrl?: string;

    // @DataMember(Order=5)
    public bearerToken?: string;

    // @DataMember(Order=6)
    public refreshToken?: string;

    // @DataMember(Order=7)
    public roles?: string[];

    // @DataMember(Order=8)
    public permissions?: string[];

    // @DataMember(Order=9)
    public responseStatus?: ResponseStatus;

    // @DataMember(Order=10)
    public meta?: { [index: string]: string; };

    public constructor(init?: Partial<RegisterResponse>) { (Object as any).assign(this, init); }
}

// @DataContract
export class AuthenticateResponse implements IHasSessionId, IHasBearerToken
{
    // @DataMember(Order=1)
    public userId?: string;

    // @DataMember(Order=2)
    public sessionId?: string;

    // @DataMember(Order=3)
    public userName?: string;

    // @DataMember(Order=4)
    public displayName?: string;

    // @DataMember(Order=5)
    public referrerUrl?: string;

    // @DataMember(Order=6)
    public bearerToken?: string;

    // @DataMember(Order=7)
    public refreshToken?: string;

    // @DataMember(Order=8)
    public profileUrl?: string;

    // @DataMember(Order=9)
    public roles?: string[];

    // @DataMember(Order=10)
    public permissions?: string[];

    // @DataMember(Order=11)
    public responseStatus?: ResponseStatus;

    // @DataMember(Order=12)
    public meta?: { [index: string]: string; };

    public constructor(init?: Partial<AuthenticateResponse>) { (Object as any).assign(this, init); }
}

// @DataContract
export class AssignRolesResponse
{
    // @DataMember(Order=1)
    public allRoles?: string[];

    // @DataMember(Order=2)
    public allPermissions?: string[];

    // @DataMember(Order=3)
    public meta?: { [index: string]: string; };

    // @DataMember(Order=4)
    public responseStatus?: ResponseStatus;

    public constructor(init?: Partial<AssignRolesResponse>) { (Object as any).assign(this, init); }
}

// @DataContract
export class UnAssignRolesResponse
{
    // @DataMember(Order=1)
    public allRoles?: string[];

    // @DataMember(Order=2)
    public allPermissions?: string[];

    // @DataMember(Order=3)
    public meta?: { [index: string]: string; };

    // @DataMember(Order=4)
    public responseStatus?: ResponseStatus;

    public constructor(init?: Partial<UnAssignRolesResponse>) { (Object as any).assign(this, init); }
}

// @DataContract
export class QueryResponse<AppMenu>
{
    // @DataMember(Order=1)
    public offset?: number;

    // @DataMember(Order=2)
    public total?: number;

    // @DataMember(Order=3)
    public results?: AppMenu[];

    // @DataMember(Order=4)
    public meta?: { [index: string]: string; };

    // @DataMember(Order=5)
    public responseStatus?: ResponseStatus;

    public constructor(init?: Partial<QueryResponse<AppMenu>>) { (Object as any).assign(this, init); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdatePassword implements IReturn<ResponseStatus>
{
    public username?: string;
    public currPassword?: string;
    public newPassword?: string;

    public constructor(init?: Partial<UpdatePassword>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdatePassword'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new ResponseStatus(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateAppUser
{
    public email?: string;
    public gender?: string;
    public firstName?: string;
    public lastName?: string;
    public fullName?: string;
    public nickName?: string;
    public displayName?: string;
    public phoneNumber?: string;
    public birthDate?: string;
    public address?: string;
    public address2?: string;
    public city?: string;
    public state?: string;
    public country?: string;
    public postCode?: string;

    public constructor(init?: Partial<UpdateAppUser>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateAppUser'; }
    public getMethod() { return 'POST'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class GetUserInfoDetail implements IReturn<AppUser>
{
    public userNameOrEmail?: string;

    public constructor(init?: Partial<GetUserInfoDetail>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'GetUserInfoDetail'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new AppUser(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class GetUserList implements IReturn<AppUser[]>
{

    public constructor(init?: Partial<GetUserList>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'GetUserList'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new Array<AppUser>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class GetUserListByRoles implements IReturn<AppUser[]>
{
    public roleName?: string;

    public constructor(init?: Partial<GetUserListByRoles>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'GetUserListByRoles'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new Array<AppUser>(); }
}

// @Route("/contacts/email", "POST")
// @ValidateRequest(Validator="IsAuthenticated")
export class ContactUsEmail implements IReturn<ContactUsEmailResponse>
{
    public contactId?: number;
    public subject?: string;
    public body?: string;

    public constructor(init?: Partial<ContactUsEmail>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'ContactUsEmail'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new ContactUsEmailResponse(); }
}

export class AppUserConfirmEmail
{
    public userName?: string;
    public emailTemplateCode?: EMAIL_TEMPLATE_CODE;

    public constructor(init?: Partial<AppUserConfirmEmail>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'AppUserConfirmEmail'; }
    public getMethod() { return 'POST'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteInvoiceAttachment implements IReturnVoid, IDeleteDb<Invoice>
{
    public id?: number;

    public constructor(init?: Partial<DeleteInvoiceAttachment>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteInvoiceAttachment'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

export class GetAppMenuByRole implements IReturn<CRUDResponse>
{
    public roleName?: string;

    public constructor(init?: Partial<GetAppMenuByRole>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'GetAppMenuByRole'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

export class UpdateProjectPlanInBatch implements IReturn<CRUDResponse>
{
    public projectId?: number;
    public versionNo?: number;
    public projectPlanList?: ProjectPlan[];

    public constructor(init?: Partial<UpdateProjectPlanInBatch>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateProjectPlanInBatch'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

/** @description Sign Up */
// @Route("/register", "PUT,POST")
// @Api(Description="Sign Up")
// @DataContract
export class Register implements IReturn<RegisterResponse>, IPost
{
    // @DataMember(Order=1)
    public userName?: string;

    // @DataMember(Order=2)
    public firstName?: string;

    // @DataMember(Order=3)
    public lastName?: string;

    // @DataMember(Order=4)
    public displayName?: string;

    // @DataMember(Order=5)
    public email?: string;

    // @DataMember(Order=6)
    public password?: string;

    // @DataMember(Order=7)
    public confirmPassword?: string;

    // @DataMember(Order=8)
    public autoLogin?: boolean;

    // @DataMember(Order=10)
    public errorView?: string;

    // @DataMember(Order=11)
    public meta?: { [index: string]: string; };

    public constructor(init?: Partial<Register>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'Register'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new RegisterResponse(); }
}

/** @description Sign In */
// @Route("/auth", "OPTIONS,GET,POST,DELETE")
// @Route("/auth/{provider}", "OPTIONS,GET,POST,DELETE")
// @Api(Description="Sign In")
// @DataContract
export class Authenticate implements IReturn<AuthenticateResponse>, IPost
{
    /** @description AuthProvider, e.g. credentials */
    // @DataMember(Order=1)
    public provider?: string;

    // @DataMember(Order=2)
    public state?: string;

    // @DataMember(Order=3)
    public oauth_token?: string;

    // @DataMember(Order=4)
    public oauth_verifier?: string;

    // @DataMember(Order=5)
    public userName?: string;

    // @DataMember(Order=6)
    public password?: string;

    // @DataMember(Order=7)
    public rememberMe?: boolean;

    // @DataMember(Order=9)
    public errorView?: string;

    // @DataMember(Order=10)
    public nonce?: string;

    // @DataMember(Order=11)
    public uri?: string;

    // @DataMember(Order=12)
    public response?: string;

    // @DataMember(Order=13)
    public qop?: string;

    // @DataMember(Order=14)
    public nc?: string;

    // @DataMember(Order=15)
    public cnonce?: string;

    // @DataMember(Order=17)
    public accessToken?: string;

    // @DataMember(Order=18)
    public accessTokenSecret?: string;

    // @DataMember(Order=19)
    public scope?: string;

    // @DataMember(Order=20)
    public returnUrl?: string;

    // @DataMember(Order=21)
    public meta?: { [index: string]: string; };

    public constructor(init?: Partial<Authenticate>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'Authenticate'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new AuthenticateResponse(); }
}

// @Route("/assignroles", "POST")
// @DataContract
export class AssignRoles implements IReturn<AssignRolesResponse>, IPost
{
    // @DataMember(Order=1)
    public userName?: string;

    // @DataMember(Order=2)
    public permissions?: string[];

    // @DataMember(Order=3)
    public roles?: string[];

    // @DataMember(Order=4)
    public meta?: { [index: string]: string; };

    public constructor(init?: Partial<AssignRoles>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'AssignRoles'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new AssignRolesResponse(); }
}

// @Route("/unassignroles", "POST")
// @DataContract
export class UnAssignRoles implements IReturn<UnAssignRolesResponse>, IPost
{
    // @DataMember(Order=1)
    public userName?: string;

    // @DataMember(Order=2)
    public permissions?: string[];

    // @DataMember(Order=3)
    public roles?: string[];

    // @DataMember(Order=4)
    public meta?: { [index: string]: string; };

    public constructor(init?: Partial<UnAssignRoles>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UnAssignRoles'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new UnAssignRolesResponse(); }
}

export class QueryAppMenus extends QueryDb_1<AppMenu> implements IReturn<QueryResponse<AppMenu>>
{
    public ids?: number[];
    public names?: string[];
    public sequence?: number;

    public constructor(init?: Partial<QueryAppMenus>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryAppMenus'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<AppMenu>(); }
}

export class QueryAppRoles extends QueryDb_1<AppRole> implements IReturn<QueryResponse<AppRole>>
{

    public constructor(init?: Partial<QueryAppRoles>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryAppRoles'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<AppRole>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryAppUsers extends QueryDb_1<AppUser> implements IReturn<QueryResponse<AppUser>>
{

    public constructor(init?: Partial<QueryAppUsers>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryAppUsers'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<AppUser>(); }
}

export class QueryCountries extends QueryDb_1<Country> implements IReturn<QueryResponse<Country>>
{

    public constructor(init?: Partial<QueryCountries>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryCountries'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<Country>(); }
}

export class QueryProvinces extends QueryDb_1<Province> implements IReturn<QueryResponse<Province>>
{
    public countryId?: number;

    public constructor(init?: Partial<QueryProvinces>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryProvinces'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<Province>(); }
}

export class QueryCities extends QueryDb_1<City> implements IReturn<QueryResponse<City>>
{
    public provinceId?: number;

    public constructor(init?: Partial<QueryCities>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryCities'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<City>(); }
}

export class QueryDistricts extends QueryDb_1<District> implements IReturn<QueryResponse<District>>
{
    public cityId?: number;

    public constructor(init?: Partial<QueryDistricts>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryDistricts'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<District>(); }
}

export class QueryVillages extends QueryDb_1<Village> implements IReturn<QueryResponse<Village>>
{
    public districtId?: number;

    public constructor(init?: Partial<QueryVillages>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryVillages'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<Village>(); }
}

export class QueryMasterBanks extends QueryDb_1<MasterBank> implements IReturn<QueryResponse<MasterBank>>
{

    public constructor(init?: Partial<QueryMasterBanks>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryMasterBanks'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<MasterBank>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryCAddresss extends QueryDb_2<CAddress, CAddressView> implements IReturn<QueryResponse<CAddressView>>
{
    public code?: string;
    public clientId?: number;
    // @Validate(Validator="Null")
    public clientCodes?: string[];

    public clientCodeContains?: string;
    public clientNameContains?: string;
    public addressNameContains?: string;

    public constructor(init?: Partial<QueryCAddresss>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryCAddresss'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<CAddressView>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryCBanks extends QueryDb_2<CBank, CBankView> implements IReturn<QueryResponse<CBankView>>
{
    public clientId?: number;
    // @Validate(Validator="Null")
    public clientCodes?: string[];

    public clientCodeContains?: string;
    public clientNameContains?: string;
    public bankNameContains?: string;

    public constructor(init?: Partial<QueryCBanks>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryCBanks'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<CBankView>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryCContracts extends QueryDb_2<CContract, CContractView> implements IReturn<QueryResponse<CContractView>>
{
    public clientId?: number;
    // @Validate(Validator="Null")
    public clientCodes?: string[];

    public clientCodeContains?: string;
    public clientNameContains?: string;
    public contractNoContains?: string;

    public constructor(init?: Partial<QueryCContracts>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryCContracts'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<CContractView>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryClients extends QueryDb_1<Client> implements IReturn<QueryResponse<Client>>
{
    public ids?: number[];
    public codes?: string[];
    public codeEndsWith?: string;
    public name?: string;
    public isActive?: boolean;

    public constructor(init?: Partial<QueryClients>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryClients'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<Client>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryContactUses extends QueryDb_1<ContactUs> implements IReturn<QueryResponse<ContactUs>>
{

    public constructor(init?: Partial<QueryContactUses>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryContactUses'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<ContactUs>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryCRoleRates extends QueryDb_2<CRoleRate, CRoleRateView> implements IReturn<QueryResponse<CRoleRateView>>
{
    public clientId?: number;
    // @Validate(Validator="Null")
    public clientCodes?: string[];

    public clientCodeContains?: string;
    public clientNameContains?: string;
    public projectRoleNameContains?: string;

    public constructor(init?: Partial<QueryCRoleRates>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryCRoleRates'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<CRoleRateView>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryDailyScrumMeetings extends QueryDb_1<DailyScrumMeeting> implements IReturn<QueryResponse<DailyScrumMeeting>>
{

    public constructor(init?: Partial<QueryDailyScrumMeetings>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryDailyScrumMeetings'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<DailyScrumMeeting>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryEmails extends QueryDb_1<Email> implements IReturn<QueryResponse<Email>>
{
    public toContains?: string;
    public subjectContains?: string;

    public constructor(init?: Partial<QueryEmails>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryEmails'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<Email>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryEmailTemplates extends QueryDb_1<EmailTemplate> implements IReturn<QueryResponse<EmailTemplate>>
{
    public code?: EMAIL_TEMPLATE_CODE;

    public constructor(init?: Partial<QueryEmailTemplates>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryEmailTemplates'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<EmailTemplate>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryEmployeeReviews extends QueryDb_1<EmployeeReview> implements IReturn<QueryResponse<EmployeeReview>>
{
    public reviewerId?: number;
    public reviewedEmployeeUserName?: string;

    public constructor(init?: Partial<QueryEmployeeReviews>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryEmployeeReviews'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<EmployeeReview>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryEmployeeReviewDetails extends QueryDb_1<EmployeeReviewDetail> implements IReturn<QueryResponse<EmployeeReviewDetail>>
{

    public constructor(init?: Partial<QueryEmployeeReviewDetails>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryEmployeeReviewDetails'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<EmployeeReviewDetail>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryInvoices extends QueryDb_2<Invoice, InvoiceView> implements IReturn<QueryResponse<InvoiceView>>
{
    public clientId?: number;

    public constructor(init?: Partial<QueryInvoices>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryInvoices'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<InvoiceView>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryInvoiceAttachments extends QueryDb_1<InvoiceAttachment> implements IReturn<QueryResponse<InvoiceAttachment>>
{
    public invoiceId?: number;

    public constructor(init?: Partial<QueryInvoiceAttachments>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryInvoiceAttachments'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<InvoiceAttachment>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryInvoiceDetails extends QueryDb_1<InvoiceDetail> implements IReturn<QueryResponse<InvoiceDetail>>
{
    public ids?: number[];
    public codes?: string[];
    public codeEndsWith?: string;
    public name?: string;
    public isActive?: boolean;

    public constructor(init?: Partial<QueryInvoiceDetails>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryInvoiceDetails'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<InvoiceDetail>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryLookups extends QueryDb_1<Lookup> implements IReturn<QueryResponse<Lookup>>
{
    public lookupType?: LOOKUPTYPE;
    public lookupValues?: string[];

    public constructor(init?: Partial<QueryLookups>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryLookups'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<Lookup>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryOtherTasks extends QueryDb_1<OtherTask> implements IReturn<QueryResponse<OtherTask>>
{

    public constructor(init?: Partial<QueryOtherTasks>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryOtherTasks'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<OtherTask>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryProjects extends QueryDb_2<Project, ProjectView> implements IReturn<QueryResponse<ProjectView>>
{
    public code?: string;
    public clientId?: number;
    // @Validate(Validator="Null")
    public clientCodes?: string[];

    public clientCodeContains?: string;
    public clientNameContains?: string;
    public nameContains?: string;
    public codeContains?: string;
    public isActive?: boolean;

    public constructor(init?: Partial<QueryProjects>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryProjects'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<ProjectView>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryProjectDocs extends QueryDb_1<ProjectDoc> implements IReturn<QueryResponse<ProjectDoc>>
{
    public projectId?: number;

    public constructor(init?: Partial<QueryProjectDocs>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryProjectDocs'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<ProjectDoc>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryProjectPlans extends QueryDb_2<ProjectPlan, ProjectPlanView> implements IReturn<QueryResponse<ProjectPlanView>>
{
    public projectId?: number;
    public versionNo?: number;

    public constructor(init?: Partial<QueryProjectPlans>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryProjectPlans'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<ProjectPlanView>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryProjectPlanVersions extends QueryDb_1<ProjectPlanVersion> implements IReturn<QueryResponse<ProjectPlanVersion>>
{
    public projectId?: number;

    public constructor(init?: Partial<QueryProjectPlanVersions>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryProjectPlanVersions'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<ProjectPlanVersion>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryProjectTasks extends QueryDb_2<ProjectTask, ProjectTaskView> implements IReturn<QueryResponse<ProjectTaskView>>
{
    public projectId?: number;

    public constructor(init?: Partial<QueryProjectTasks>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryProjectTasks'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<ProjectTaskView>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryProjectTaskDocs extends QueryDb_1<ProjectTaskDoc> implements IReturn<QueryResponse<ProjectTaskDoc>>
{
    public projectTaskId?: number;

    public constructor(init?: Partial<QueryProjectTaskDocs>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryProjectTaskDocs'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<ProjectTaskDoc>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryProjectTeams extends QueryDb_2<ProjectTeam, ProjectTeamView> implements IReturn<QueryResponse<ProjectTeamView>>
{
    public projectId?: number;

    public constructor(init?: Partial<QueryProjectTeams>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryProjectTeams'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<ProjectTeamView>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryReviewMasterQuestions extends QueryDb_1<ReviewMasterQuestion> implements IReturn<QueryResponse<ReviewMasterQuestion>>
{

    public constructor(init?: Partial<QueryReviewMasterQuestions>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryReviewMasterQuestions'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<ReviewMasterQuestion>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryTimeSheets extends QueryDb_2<TimeSheet, TimeSheetView> implements IReturn<QueryResponse<TimeSheetView>>
{
    public appUserIds?: number[];
    public tsDateBetween?: string[];
    public clientIds?: number[];
    public projectIds?: number[];

    public constructor(init?: Partial<QueryTimeSheets>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryTimeSheets'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<TimeSheetView>(); }
}

export class CreateAppMenu implements IReturn<CRUDResponse>, ICreateDb<AppMenu>
{
    public appRoleId?: number;
    public appMenuId?: number;
    public sequence?: number;
    public name?: string;
    public to?: string;
    public icon?: string;
    public isActive?: boolean;

    public constructor(init?: Partial<CreateAppMenu>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateAppMenu'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

export class UpdateAppMenu implements IReturn<CRUDResponse>, IPatchDb<AppMenu>
{
    public id?: number;
    public appRoleId?: number;
    public appMenuId?: number;
    public sequence?: number;
    public name?: string;
    public to?: string;
    public icon?: string;
    public isActive?: boolean;

    public constructor(init?: Partial<UpdateAppMenu>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateAppMenu'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

export class DeleteAppMenu implements IReturnVoid, IDeleteDb<AppMenu>
{
    public id?: number;

    public constructor(init?: Partial<DeleteAppMenu>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteAppMenu'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

export class CreateAppRole implements IReturn<CRUDResponse>, ICreateDb<AppRole>
{
    public roleName?: string;
    public description?: string;

    public constructor(init?: Partial<CreateAppRole>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateAppRole'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

export class UpdateAppRole implements IReturn<CRUDResponse>, IPatchDb<AppRole>
{
    public id?: number;
    public roleName?: string;
    public description?: string;

    public constructor(init?: Partial<UpdateAppRole>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateAppRole'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

export class DeleteAppRole implements IReturnVoid, IDeleteDb<AppRole>
{
    public id?: number;

    public constructor(init?: Partial<DeleteAppRole>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteAppRole'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateCAddress implements IReturn<CRUDResponse>, ICreateDb<CAddress>
{
    public clientId?: number;
    public addressName?: string;
    public country?: string;
    public province?: string;
    public city?: string;
    public district?: string;
    public village?: string;
    public address1?: string;
    public address2?: string;
    public postalCode?: string;
    public phoneNo?: string;
    public isMain?: boolean;

    public constructor(init?: Partial<CreateCAddress>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateCAddress'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateCAddress implements IReturn<CRUDResponse>, IPatchDb<CAddress>
{
    public id?: number;
    public clientId?: number;
    public addressName?: string;
    public country?: string;
    public province?: string;
    public city?: string;
    public district?: string;
    public village?: string;
    public address1?: string;
    public address2?: string;
    public postalCode?: string;
    public phoneNo?: string;
    public isMain?: boolean;

    public constructor(init?: Partial<UpdateCAddress>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateCAddress'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteCAddress implements IReturnVoid, IDeleteDb<CAddress>
{
    public id?: number;

    public constructor(init?: Partial<DeleteCAddress>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteCAddress'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateCBank implements IReturn<CRUDResponse>, ICreateDb<CBank>
{
    public clientId?: number;
    public bankName?: string;
    public accountName?: string;
    public accountNo?: string;
    public currency?: string;
    public swiftCode?: string;
    public isMain?: boolean;

    public constructor(init?: Partial<CreateCBank>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateCBank'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateCBank implements IReturn<CRUDResponse>, IPatchDb<CBank>
{
    public id?: number;
    public clientId?: number;
    public bankName?: string;
    public accountName?: string;
    public accountNo?: string;
    public currency?: string;
    public swiftCode?: string;
    public isMain?: boolean;

    public constructor(init?: Partial<UpdateCBank>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateCBank'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteCBank implements IReturnVoid, IDeleteDb<CBank>
{
    public id?: number;

    public constructor(init?: Partial<DeleteCBank>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteCBank'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateCContract implements IReturn<CRUDResponse>, ICreateDb<CContract>
{
    public clientId?: number;
    public contractNo?: string;
    public description?: string;
    public startDate?: string;
    public endDate?: string;
    public currency?: string;
    public totalAmount?: number;
    public isActive?: boolean;

    public constructor(init?: Partial<CreateCContract>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateCContract'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateCContract implements IReturn<CRUDResponse>, IPatchDb<CContract>
{
    public id?: number;
    public clientId?: number;
    public contractNo?: string;
    public description?: string;
    public startDate?: string;
    public endDate?: string;
    public currency?: string;
    public totalAmount?: number;
    public isActive?: boolean;

    public constructor(init?: Partial<UpdateCContract>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateCContract'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteCContract implements IReturnVoid, IDeleteDb<CContract>
{
    public id?: number;

    public constructor(init?: Partial<DeleteCContract>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteCContract'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateClient implements IReturn<CRUDResponse>, ICreateDb<Client>
{
    public code?: string;
    public name?: string;
    public phoneNo?: string;
    public email?: string;
    public description?: string;
    public isActive?: boolean;

    public constructor(init?: Partial<CreateClient>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateClient'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateClient implements IReturn<CRUDResponse>, IPatchDb<Client>
{
    public id?: number;
    public code?: string;
    public name?: string;
    public phoneNo?: string;
    public email?: string;
    public description?: string;
    public isActive?: boolean;

    public constructor(init?: Partial<UpdateClient>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateClient'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteClient implements IReturnVoid, IDeleteDb<Client>
{
    public id?: number;

    public constructor(init?: Partial<DeleteClient>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteClient'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateCRoleRate implements IReturn<CRUDResponse>, ICreateDb<CRoleRate>
{
    public clientId?: number;
    public projectRoleName?: PROJECT_TEAM_ROLE;
    public currency?: string;
    public rateInDay?: number;
    public isActive?: boolean;

    public constructor(init?: Partial<CreateCRoleRate>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateCRoleRate'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateCRoleRate implements IReturn<CRUDResponse>, IPatchDb<CRoleRate>
{
    public id?: number;
    public clientId?: number;
    public projectRoleName?: PROJECT_TEAM_ROLE;
    public currency?: string;
    public rateInDay?: number;
    public isActive?: boolean;

    public constructor(init?: Partial<UpdateCRoleRate>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateCRoleRate'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteCRoleRate implements IReturnVoid, IDeleteDb<CRoleRate>
{
    public id?: number;

    public constructor(init?: Partial<DeleteCRoleRate>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteCRoleRate'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateDailyScrumMeeting implements IReturn<CRUDResponse>, ICreateDb<DailyScrumMeeting>
{
    public appUserId?: number;
    public meetingDate?: string;
    public blockers?: string;
    public whatDidYouDoYesterday?: string;
    public whatGoalsToday?: string;

    public constructor(init?: Partial<CreateDailyScrumMeeting>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateDailyScrumMeeting'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateDailyScrumMeeting implements IReturn<CRUDResponse>, IPatchDb<DailyScrumMeeting>
{
    public id?: number;
    public appUserId?: number;
    public meetingDate?: string;
    public blockers?: string;
    public whatDidYouDoYesterday?: string;
    public whatGoalsToday?: string;

    public constructor(init?: Partial<UpdateDailyScrumMeeting>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateDailyScrumMeeting'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteDailyScrumMeeting implements IReturnVoid, IDeleteDb<DailyScrumMeeting>
{
    public id?: number;

    public constructor(init?: Partial<DeleteDailyScrumMeeting>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteDailyScrumMeeting'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateEmail implements IReturn<CRUDResponse>, ICreateDb<Email>
{
    public code?: EMAIL_TEMPLATE_CODE;
    public from?: string;
    public to?: string;
    public subject?: string;
    public body?: string;
    public sendStatusMessage?: string;

    public constructor(init?: Partial<CreateEmail>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateEmail'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateEmail implements IReturn<CRUDResponse>, IPatchDb<Email>
{
    public id?: number;
    public code?: EMAIL_TEMPLATE_CODE;
    public from?: string;
    public to?: string;
    public subject?: string;
    public body?: string;
    public sendStatusMessage?: string;

    public constructor(init?: Partial<UpdateEmail>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateEmail'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteEmail implements IReturnVoid, IDeleteDb<Project>
{
    public id?: number;

    public constructor(init?: Partial<DeleteEmail>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteEmail'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateEmailTemplate implements IReturn<CRUDResponse>, ICreateDb<EmailTemplate>
{
    public code?: EMAIL_TEMPLATE_CODE;
    public description?: string;
    public emailTemplateText?: string;
    public htmlTemplate?: string;
    public isActive?: boolean;

    public constructor(init?: Partial<CreateEmailTemplate>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateEmailTemplate'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateEmailTemplate implements IReturn<CRUDResponse>, IPatchDb<EmailTemplate>
{
    public id?: number;
    public code?: EMAIL_TEMPLATE_CODE;
    public description?: string;
    public emailTemplateText?: string;
    public isActive?: boolean;

    public constructor(init?: Partial<UpdateEmailTemplate>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateEmailTemplate'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteEmailTemplate implements IReturnVoid, IDeleteDb<EmailTemplate>
{
    public id?: number;

    public constructor(init?: Partial<DeleteEmailTemplate>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteEmailTemplate'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateEmployeeReview implements IReturn<CRUDResponse>, ICreateDb<EmployeeReview>
{
    public periodYear?: number;
    public periodQuarter?: string;
    public reviewDate?: string;
    // @References("typeof(GDIApps.ServiceModel.Types.AppUser)")
    public appUserId?: number;

    public reviewNotes?: string;

    public constructor(init?: Partial<CreateEmployeeReview>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateEmployeeReview'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateEmployeeReview implements IReturn<CRUDResponse>, IPatchDb<EmployeeReview>
{
    public id?: number;
    public periodYear?: number;
    public periodQuarter?: string;
    public reviewDate?: string;
    // @References("typeof(GDIApps.ServiceModel.Types.AppUser)")
    public reviewerId?: number;

    // @References("typeof(GDIApps.ServiceModel.Types.AppUser)")
    public reviewedEmployeeId?: number;

    public reviewNotes?: string;

    public constructor(init?: Partial<UpdateEmployeeReview>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateEmployeeReview'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteEmployeeReview implements IReturnVoid, IDeleteDb<EmployeeReview>
{
    public id?: number;

    public constructor(init?: Partial<DeleteEmployeeReview>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteEmployeeReview'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateEmployeeReviewDetail implements IReturn<CRUDResponse>, ICreateDb<EmployeeReviewDetail>
{
    public employeeReviewId?: number;
    public reviewType?: REVIEW_TYPE;
    public category?: REVIEW_QUESTION_CATEGORY;
    public no?: number;
    public question?: string;
    public questionValue?: number;

    public constructor(init?: Partial<CreateEmployeeReviewDetail>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateEmployeeReviewDetail'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateEmployeeReviewDetail implements IReturn<CRUDResponse>, IPatchDb<EmployeeReviewDetail>
{
    public id?: number;
    public employeeReviewId?: number;
    public reviewType?: REVIEW_TYPE;
    public category?: REVIEW_QUESTION_CATEGORY;
    public no?: number;
    public question?: string;
    public questionValue?: number;

    public constructor(init?: Partial<UpdateEmployeeReviewDetail>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateEmployeeReviewDetail'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteEmployeeReviewDetail implements IReturnVoid, IDeleteDb<EmployeeReviewDetail>
{
    public id?: number;

    public constructor(init?: Partial<DeleteEmployeeReviewDetail>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteEmployeeReviewDetail'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateInvoice implements IReturn<CRUDResponse>, ICreateDb<Invoice>
{
    public clientId?: number;
    public cContractId?: number;
    public cBankId?: number;
    public cAddressId?: number;
    public invoiceNo?: string;
    public paymentTermDays?: number;
    public invoiceDate?: string;
    public description?: string;
    public poNumber?: string;
    public vat?: string;
    public wht?: string;
    public totalAmount?: number;
    public vatAmount?: number;
    public invoiceStatus?: string;

    public constructor(init?: Partial<CreateInvoice>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateInvoice'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateInvoice implements IReturn<CRUDResponse>, IPatchDb<Invoice>
{
    public id?: number;
    public clientId?: number;
    public cContractId?: number;
    public cBankId?: number;
    public cAddressId?: number;
    public invoiceNo?: string;
    public paymentTermDays?: number;
    public invoiceDate?: string;
    public description?: string;
    public poNumber?: string;
    public vat?: string;
    public wht?: string;
    public totalAmount?: number;
    public vatAmount?: number;
    public invoiceStatus?: string;

    public constructor(init?: Partial<UpdateInvoice>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateInvoice'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteInvoice implements IReturnVoid, IDeleteDb<Invoice>
{
    public id?: number;

    public constructor(init?: Partial<DeleteInvoice>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteInvoice'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateInvoiceAttachment implements IReturn<CRUDResponse>, ICreateDb<InvoiceAttachment>
{
    public invoiceId?: number;
    public fileName?: string;
    public attachmentUrl?: string;

    public constructor(init?: Partial<CreateInvoiceAttachment>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateInvoiceAttachment'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateInvoiceAttachment implements IReturn<CRUDResponse>, IPatchDb<InvoiceAttachment>
{
    public id?: number;
    public fileName?: string;

    public constructor(init?: Partial<UpdateInvoiceAttachment>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateInvoiceAttachment'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateInvoiceDetail implements IReturn<CRUDResponse>, ICreateDb<InvoiceDetail>
{
    public invoiceId?: number;
    public no?: string;
    public description?: string;
    public amount?: number;

    public constructor(init?: Partial<CreateInvoiceDetail>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateInvoiceDetail'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateInvoiceDetail implements IReturn<CRUDResponse>, IPatchDb<InvoiceDetail>
{
    public id?: number;
    public invoiceId?: number;
    public no?: string;
    public description?: string;
    public amount?: number;

    public constructor(init?: Partial<UpdateInvoiceDetail>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateInvoiceDetail'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteInvoiceDetail implements IReturnVoid, IDeleteDb<InvoiceDetail>
{
    public id?: number;

    public constructor(init?: Partial<DeleteInvoiceDetail>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteInvoiceDetail'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateLookup implements IReturn<CRUDResponse>, ICreateDb<Lookup>
{
    public lookupType?: LOOKUPTYPE;
    public lookupValue?: string;
    public lookupText?: string;
    public isActive?: boolean;

    public constructor(init?: Partial<CreateLookup>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateLookup'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateLookup implements IReturn<CRUDResponse>, IPatchDb<Lookup>
{
    public id?: number;
    public lookupType?: LOOKUPTYPE;
    public lookupValue?: string;
    public lookupText?: string;
    public isActive?: boolean;

    public constructor(init?: Partial<UpdateLookup>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateLookup'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteLookup implements IReturnVoid, IDeleteDb<Lookup>
{
    public id?: number;

    public constructor(init?: Partial<DeleteLookup>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteLookup'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateOtherTask implements IReturn<CRUDResponse>, ICreateDb<OtherTask>
{
    public appUserId?: number;
    public taskName?: string;
    public taskDesc?: string;
    public durationDays?: number;
    public planStart?: string;
    public planEnd?: string;
    public actualStart?: string;
    public actualEnd?: string;
    public completed?: number;
    public status?: TASK_STATUS;

    public constructor(init?: Partial<CreateOtherTask>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateOtherTask'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateOtherTask implements IReturn<CRUDResponse>, IPatchDb<OtherTask>
{
    public appUserId?: number;
    public taskName?: string;
    public taskDesc?: string;
    public durationDays?: number;
    public planStart?: string;
    public planEnd?: string;
    public actualStart?: string;
    public actualEnd?: string;
    public completed?: number;
    public status?: TASK_STATUS;

    public constructor(init?: Partial<UpdateOtherTask>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateOtherTask'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteOtherTask implements IReturnVoid, IDeleteDb<OtherTask>
{
    public id?: number;

    public constructor(init?: Partial<DeleteOtherTask>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteOtherTask'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateProject implements IReturn<CRUDResponse>, ICreateDb<Project>
{
    public clientId?: number;
    public cContractId?: number;
    public code?: string;
    public name?: string;
    public description?: string;
    public projectOwner?: string;
    public projectManager?: string;
    public nominalValue?: number;
    public durationDays?: number;
    public estimatedStartDate?: string;
    public estimatedEndDate?: string;
    public actualtartDate?: string;
    public actualEndDate?: string;
    public isActive?: boolean;

    public constructor(init?: Partial<CreateProject>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateProject'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateProject implements IReturn<CRUDResponse>, IPatchDb<Project>
{
    public id?: number;
    public clientId?: number;
    public cContractId?: number;
    public code?: string;
    public name?: string;
    public description?: string;
    public projectOwner?: string;
    public projectManager?: string;
    public nominalValue?: number;
    public durationDays?: number;
    public estimatedStartDate?: string;
    public estimatedEndDate?: string;
    public actualtartDate?: string;
    public actualEndDate?: string;
    public isActive?: boolean;

    public constructor(init?: Partial<UpdateProject>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateProject'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteProject implements IReturnVoid, IDeleteDb<Project>
{
    public id?: number;

    public constructor(init?: Partial<DeleteProject>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteProject'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateProjectDoc implements IReturn<CRUDResponse>, ICreateDb<ProjectDoc>
{
    public projectId?: number;
    public fileName?: string;
    public attachmentUrl?: string;

    public constructor(init?: Partial<CreateProjectDoc>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateProjectDoc'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateProjectDoc implements IReturn<CRUDResponse>, IPatchDb<ProjectDoc>
{
    public id?: number;
    public fileName?: string;

    public constructor(init?: Partial<UpdateProjectDoc>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateProjectDoc'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteProjectDoc implements IReturnVoid, IDeleteDb<Project>
{
    public id?: number;

    public constructor(init?: Partial<DeleteProjectDoc>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteProjectDoc'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateProjectPlan implements IReturn<CRUDResponse>, ICreateDb<ProjectPlan>
{
    public projectId?: number;
    public versionNo?: number;
    public taskLevel?: number;
    public taskNo?: number;
    public parentCode?: string;
    public taskCode?: string;
    public dependecyTaskCode?: string;
    public appUserId?: number;
    public taskTitle?: string;
    public durationDays?: number;
    public startDate?: string;
    public endDate?: string;
    public completedPercentage?: number;
    public resourceCost?: number;

    public constructor(init?: Partial<CreateProjectPlan>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateProjectPlan'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateProjectPlan implements IReturn<CRUDResponse>, IPatchDb<ProjectPlan>
{
    public id?: number;
    public projectId?: number;
    public versionNo?: number;
    public taskLevel?: number;
    public taskNo?: number;
    public parentCode?: string;
    public taskCode?: string;
    public dependecyTaskCode?: string;
    public appUserId?: number;
    public taskTitle?: string;
    public durationDays?: number;
    public startDate?: string;
    public endDate?: string;
    public completedPercentage?: number;
    public resourceCost?: number;

    public constructor(init?: Partial<UpdateProjectPlan>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateProjectPlan'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteProjectPlan implements IReturnVoid, IDeleteDb<Project>
{
    public id?: number;

    public constructor(init?: Partial<DeleteProjectPlan>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteProjectPlan'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateProjectPlanVersion implements IReturn<CRUDResponse>, ICreateDb<ProjectPlanVersion>
{
    public projectId?: number;
    public appUserId?: number;
    public versionNo?: number;
    public isActive?: boolean;

    public constructor(init?: Partial<CreateProjectPlanVersion>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateProjectPlanVersion'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateProjectPlanVersion implements IReturn<CRUDResponse>, IPatchDb<ProjectPlanVersion>
{
    public id?: number;
    public projectId?: number;
    public appUserId?: number;
    public versionNo?: number;
    public isActive?: boolean;

    public constructor(init?: Partial<UpdateProjectPlanVersion>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateProjectPlanVersion'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteProjectPlanVersion implements IReturnVoid, IDeleteDb<Project>
{
    public id?: number;

    public constructor(init?: Partial<DeleteProjectPlanVersion>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteProjectPlanVersion'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateProjectTask implements IReturn<CRUDResponse>, ICreateDb<ProjectTask>
{
    public projectId?: number;
    public projectPlanId?: number;
    public no?: number;
    public taskCode?: string;
    public taskName?: string;
    public description?: string;
    public durationDays?: number;
    public dueDate?: string;
    public planStart?: string;
    public planEnd?: string;
    public actualStart?: string;
    public actualEnd?: string;
    public completed?: number;
    public status?: TASK_STATUS;
    public taskLabel?: TASK_LABEL;
    public projectTeamId?: number;
    public isArchived?: boolean;

    public constructor(init?: Partial<CreateProjectTask>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateProjectTask'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateProjectTask implements IReturn<CRUDResponse>, IPatchDb<ProjectTask>
{
    public id?: number;
    public projectId?: number;
    public projectPlanId?: number;
    public no?: number;
    public taskCode?: string;
    public taskName?: string;
    public description?: string;
    public durationDays?: number;
    public dueDate?: string;
    public planStart?: string;
    public planEnd?: string;
    public actualStart?: string;
    public actualEnd?: string;
    public completed?: number;
    public status?: TASK_STATUS;
    public taskLabel?: TASK_LABEL;
    public projectTeamId?: number;
    public isArchived?: boolean;

    public constructor(init?: Partial<UpdateProjectTask>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateProjectTask'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteProjectTask implements IReturnVoid, IDeleteDb<ProjectTask>
{
    public id?: number;

    public constructor(init?: Partial<DeleteProjectTask>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteProjectTask'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateProjectTaskDoc implements IReturn<CRUDResponse>, ICreateDb<ProjectTaskDoc>
{
    public projectTaskId?: number;
    public fileName?: string;
    public attachmentUrl?: string;

    public constructor(init?: Partial<CreateProjectTaskDoc>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateProjectTaskDoc'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateProjectTaskDoc implements IReturn<CRUDResponse>, IPatchDb<ProjectTaskDoc>
{
    public id?: number;
    public projectTaskId?: number;
    public fileName?: string;

    public constructor(init?: Partial<UpdateProjectTaskDoc>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateProjectTaskDoc'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteProjectTaskDoc implements IReturnVoid, IDeleteDb<Project>
{
    public id?: number;

    public constructor(init?: Partial<DeleteProjectTaskDoc>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteProjectTaskDoc'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateProjectTeam implements IReturn<CRUDResponse>, ICreateDb<ProjectTeam>
{
    public projectId?: number;
    public appUserId?: number;
    public projectTeamRole?: PROJECT_TEAM_ROLE;
    public projectTeamRoleCurrRate?: CURRENCY_RATE;
    public projectTeamRoleRatePerDay?: number;

    public constructor(init?: Partial<CreateProjectTeam>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateProjectTeam'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateProjectTeam implements IReturn<CRUDResponse>, IPatchDb<ProjectTeam>
{
    public id?: number;
    public projectId?: number;
    public appUserId?: number;
    public projectTeamRole?: PROJECT_TEAM_ROLE;
    public projectTeamRoleCurrRate?: CURRENCY_RATE;
    public projectTeamRoleRatePerDay?: number;

    public constructor(init?: Partial<UpdateProjectTeam>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateProjectTeam'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteProjectTeam implements IReturnVoid, IDeleteDb<Project>
{
    public id?: number;

    public constructor(init?: Partial<DeleteProjectTeam>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteProjectTeam'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateReviewMasterQuestion implements IReturn<CRUDResponse>, ICreateDb<ReviewMasterQuestion>
{
    public reviewType?: REVIEW_TYPE;
    public category?: REVIEW_QUESTION_CATEGORY;
    public no?: number;
    public question?: string;
    public description?: string;

    public constructor(init?: Partial<CreateReviewMasterQuestion>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateReviewMasterQuestion'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateReviewMasterQuestion implements IReturn<CRUDResponse>, IPatchDb<ReviewMasterQuestion>
{
    public id?: number;
    public reviewType?: REVIEW_TYPE;
    public category?: REVIEW_QUESTION_CATEGORY;
    public no?: number;
    public question?: string;
    public description?: string;

    public constructor(init?: Partial<UpdateReviewMasterQuestion>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateReviewMasterQuestion'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteReviewMasterQuestion implements IReturnVoid, IDeleteDb<ReviewMasterQuestion>
{
    public id?: number;

    public constructor(init?: Partial<DeleteReviewMasterQuestion>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteReviewMasterQuestion'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateTimeSheet implements IReturn<CRUDResponse>, ICreateDb<TimeSheet>
{
    public tsDate?: string;
    public appUserId?: number;
    public clientId?: number;
    public projectId?: number;
    public projectTaskId?: number;
    public notes?: string;

    public constructor(init?: Partial<CreateTimeSheet>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateTimeSheet'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdateTimeSheet implements IReturn<CRUDResponse>, IPatchDb<TimeSheet>
{
    public id?: number;
    public tsDate?: string;
    public appUserId?: number;
    public clientId?: number;
    public projectId?: number;
    public projectTaskId?: number;
    public notes?: string;

    public constructor(init?: Partial<UpdateTimeSheet>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateTimeSheet'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new CRUDResponse(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class DeleteTimeSheet implements IReturnVoid, IDeleteDb<TimeSheet>
{
    public id?: number;

    public constructor(init?: Partial<DeleteTimeSheet>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteTimeSheet'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

