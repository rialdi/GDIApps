/* Options:
Date: 2023-02-27 11:42:42
Version: 6.60
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

export interface IPut
{
}

export interface IDelete
{
}

export interface ICreateDb<Table>
{
}

export interface IPatchDb<Table>
{
}

export interface IDeleteDb<Table>
{
}

export enum EMAIL_TEMPLATE_CODE
{
    APPUSER_EMAIL_CONFIRM = 'APPUSER_EMAIL_CONFIRM',
    APPUSER_RESET_PASSWORD = 'APPUSER_RESET_PASSWORD',
    APPUSER_REGISTRATION = 'APPUSER_REGISTRATION',
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

export class QueryData<T> extends QueryBase
{

    public constructor(init?: Partial<QueryData<T>>) { super(init); (Object as any).assign(this, init); }
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
    public districtId?: number;
    public name?: string;
    public latitude?: number;
    public longitude?: number;
    public postal?: string;

    public constructor(init?: Partial<Village>) { (Object as any).assign(this, init); }
}

export class Bank
{
    public id?: number;
    public bankName?: string;
    public swiftCode?: string;

    public constructor(init?: Partial<Bank>) { (Object as any).assign(this, init); }
}

export class QueryDb_2<From, Into> extends QueryBase
{

    public constructor(init?: Partial<QueryDb_2<From, Into>>) { super(init); (Object as any).assign(this, init); }
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

export class QueryDb_1<T> extends QueryBase
{

    public constructor(init?: Partial<QueryDb_1<T>>) { super(init); (Object as any).assign(this, init); }
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

    public constructor(init?: Partial<Invoice>) { super(init); (Object as any).assign(this, init); }
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

export class ProjectView extends Project
{
    public clientCode?: string;
    public clientName?: string;
    public cContractContractNo?: string;

    public constructor(init?: Partial<ProjectView>) { super(init); (Object as any).assign(this, init); }
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
    // @Input(Type="file")
    public profileUrl?: string;

    public lastLoginIp?: string;
    public lastLoginDate?: string;
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
    // @Input(Type="file")
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

export class HelloResponse
{
    public result?: string;

    public constructor(init?: Partial<HelloResponse>) { (Object as any).assign(this, init); }
}

export class Todo
{
    public id?: number;
    public text?: string;
    public isFinished?: boolean;

    public constructor(init?: Partial<Todo>) { (Object as any).assign(this, init); }
}

// @DataContract
export class QueryResponse<Todo>
{
    // @DataMember(Order=1)
    public offset?: number;

    // @DataMember(Order=2)
    public total?: number;

    // @DataMember(Order=3)
    public results?: Todo[];

    // @DataMember(Order=4)
    public meta?: { [index: string]: string; };

    // @DataMember(Order=5)
    public responseStatus?: ResponseStatus;

    public constructor(init?: Partial<QueryResponse<Todo>>) { (Object as any).assign(this, init); }
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

export class CRUDResponse
{
    public id?: number;
    public responseStatus?: ResponseStatus;

    public constructor(init?: Partial<CRUDResponse>) { (Object as any).assign(this, init); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class UpdatePassword implements IReturn<ResponseStatus>
{
    public username?: string;
    public password?: string;

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

// @Route("/hello")
// @Route("/hello/{Name}")
export class Hello implements IReturn<HelloResponse>
{
    public name?: string;

    public constructor(init?: Partial<Hello>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'Hello'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new HelloResponse(); }
}

// @Route("/todos", "GET")
export class QueryTodos extends QueryData<Todo> implements IReturn<QueryResponse<Todo>>
{
    public id?: number;
    public ids?: number[];
    public textContains?: string;

    public constructor(init?: Partial<QueryTodos>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryTodos'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<Todo>(); }
}

// @Route("/todos", "POST")
export class CreateTodo implements IReturn<Todo>, IPost
{
    // @Validate(Validator="NotEmpty")
    public text?: string;

    public constructor(init?: Partial<CreateTodo>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateTodo'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new Todo(); }
}

// @Route("/todos/{Id}", "PUT")
export class UpdateTodo implements IReturn<Todo>, IPut
{
    public id?: number;
    // @Validate(Validator="NotEmpty")
    public text?: string;

    public isFinished?: boolean;

    public constructor(init?: Partial<UpdateTodo>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateTodo'; }
    public getMethod() { return 'PUT'; }
    public createResponse() { return new Todo(); }
}

// @Route("/todos/{Id}", "DELETE")
export class DeleteTodo implements IReturnVoid, IDelete
{
    public id?: number;

    public constructor(init?: Partial<DeleteTodo>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteTodo'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @Route("/todos", "DELETE")
export class DeleteTodos implements IReturnVoid, IDelete
{
    public ids?: number[];

    public constructor(init?: Partial<DeleteTodos>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteTodos'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
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

export class QueryCountries extends QueryData<Country> implements IReturn<QueryResponse<Country>>
{

    public constructor(init?: Partial<QueryCountries>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryCountries'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<Country>(); }
}

export class QueryProvinces extends QueryData<Province> implements IReturn<QueryResponse<Province>>
{
    public countryId?: number;

    public constructor(init?: Partial<QueryProvinces>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryProvinces'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<Province>(); }
}

export class QueryCities extends QueryData<City> implements IReturn<QueryResponse<City>>
{
    public provinceId?: number;

    public constructor(init?: Partial<QueryCities>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryCities'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<City>(); }
}

export class QueryDistricts extends QueryData<District> implements IReturn<QueryResponse<District>>
{
    public cityId?: number;

    public constructor(init?: Partial<QueryDistricts>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryDistricts'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<District>(); }
}

export class QueryVillages extends QueryData<Village> implements IReturn<QueryResponse<Village>>
{
    public districtId?: number;

    public constructor(init?: Partial<QueryVillages>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryVillages'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<Village>(); }
}

export class QueryBanks extends QueryData<Bank> implements IReturn<QueryResponse<Bank>>
{

    public constructor(init?: Partial<QueryBanks>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryBanks'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<Bank>(); }
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
export class QueryInvoices extends QueryDb_2<Invoice, InvoiceView> implements IReturn<QueryResponse<InvoiceView>>
{
    public clientId?: number;

    public constructor(init?: Partial<QueryInvoices>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryInvoices'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<InvoiceView>(); }
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

    public constructor(init?: Partial<QueryProjects>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryProjects'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<ProjectView>(); }
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
export class CreateProject implements IReturn<CRUDResponse>, ICreateDb<Project>
{
    public clientId?: number;
    public cContractId?: number;
    public code?: string;
    public name?: string;
    public description?: string;
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

