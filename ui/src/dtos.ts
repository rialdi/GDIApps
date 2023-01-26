/* Options:
Date: 2023-01-26 18:55:24
Version: 6.50
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

export interface IHasSessionId
{
    sessionId?: string;
}

export interface IHasBearerToken
{
    bearerToken?: string;
}

export interface IPost
{
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

export enum Department
{
    None = 'None',
    QualityAssurance = 'QualityAssurance',
    Developer = 'Developer',
    Management = 'Management',
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

export class QueryDb<T> extends QueryBase
{

    public constructor(init?: Partial<QueryDb<T>>) { super(init); (Object as any).assign(this, init); }
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

export enum RoomType
{
    Single = 'Single',
    Double = 'Double',
    Queen = 'Queen',
    Twin = 'Twin',
    Suite = 'Suite',
}

/**
* Booking Details
*/
export class Booking extends AuditBase
{
    public id?: number;
    public name?: string;
    public roomType?: RoomType;
    public roomNumber?: number;
    public bookingStartDate?: string;
    public bookingEndDate?: string;
    public cost?: number;
    public notes?: string;
    public cancelled?: boolean;

    public constructor(init?: Partial<Booking>) { super(init); (Object as any).assign(this, init); }
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
    // @StringLength(1000)
    public description?: string;

    public isActive?: boolean;
    public projectList?: Project[];

    public constructor(init?: Partial<Client>) { super(init); (Object as any).assign(this, init); }
}

export class Project extends AuditBase
{
    public id?: number;
    // @Required()
    // @References("typeof(GDIApps.ServiceModel.Types.Client)")
    public clientId?: number;

    public projectClient?: Client;
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

/**
* Emails
*/
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

export enum LOOKUPTYPE
{
    STATUS = 'STATUS',
    PRIORITY = 'PRIORITY',
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
    public lastLoginIp?: string;
    public lastLoginDate?: string;
    public employeeId?: string;
    public department?: Department;
    public isArchived?: boolean;
    public archivedDate?: string;

    public constructor(init?: Partial<AppUser>) { super(init); (Object as any).assign(this, init); }
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
export class IdResponse
{
    // @DataMember(Order=1)
    public id?: string;

    // @DataMember(Order=2)
    public responseStatus?: ResponseStatus;

    public constructor(init?: Partial<IdResponse>) { (Object as any).assign(this, init); }
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
export class UpdateAppUser extends AppUser
{

    public constructor(init?: Partial<UpdateAppUser>) { super(init); (Object as any).assign(this, init); }
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

/**
* Sign In
*/
// @Route("/auth", "OPTIONS,GET,POST,DELETE")
// @Route("/auth/{provider}", "OPTIONS,GET,POST,DELETE")
// @Api(Description="Sign In")
// @DataContract
export class Authenticate implements IReturn<AuthenticateResponse>, IPost
{
    /**
    * AuthProvider, e.g. credentials
    */
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

/**
* Sign Up
*/
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

/**
* Find Bookings
*/
// @Route("/bookings", "GET")
// @Route("/bookings/{Id}", "GET")
export class QueryBookings extends QueryDb<Booking> implements IReturn<QueryResponse<Booking>>
{
    public id?: number;

    public constructor(init?: Partial<QueryBookings>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryBookings'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<Booking>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryClients extends QueryDb<Client> implements IReturn<QueryResponse<Client>>
{
    public code?: string;

    public constructor(init?: Partial<QueryClients>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryClients'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<Client>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryContactUses extends QueryDb<ContactUs> implements IReturn<QueryResponse<ContactUs>>
{

    public constructor(init?: Partial<QueryContactUses>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryContactUses'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<ContactUs>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryEmails extends QueryDb<Email> implements IReturn<QueryResponse<Email>>
{
    public toContains?: string;
    public subjectContains?: string;

    public constructor(init?: Partial<QueryEmails>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryEmails'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<Email>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryEmailTemplates extends QueryDb<EmailTemplate> implements IReturn<QueryResponse<EmailTemplate>>
{
    public code?: EMAIL_TEMPLATE_CODE;

    public constructor(init?: Partial<QueryEmailTemplates>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryEmailTemplates'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<EmailTemplate>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryLookups extends QueryDb<Lookup> implements IReturn<QueryResponse<Lookup>>
{
    public lookuptype?: LOOKUPTYPE;

    public constructor(init?: Partial<QueryLookups>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryLookups'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<Lookup>(); }
}

// @ValidateRequest(Validator="IsAuthenticated")
export class QueryProjects extends QueryDb<Project> implements IReturn<QueryResponse<Project>>
{
    public code?: string;
    public clientId?: string;

    public constructor(init?: Partial<QueryProjects>) { super(init); (Object as any).assign(this, init); }
    public getTypeName() { return 'QueryProjects'; }
    public getMethod() { return 'GET'; }
    public createResponse() { return new QueryResponse<Project>(); }
}

/**
* Create a new Booking
*/
// @Route("/bookings", "POST")
// @ValidateRequest(Validator="HasRole(`Employee`)")
export class CreateBooking implements IReturn<IdResponse>, ICreateDb<Booking>
{
    /**
    * Name this Booking is for
    */
    // @Validate(Validator="NotEmpty")
    public name?: string;

    public roomType?: RoomType;
    // @Validate(Validator="GreaterThan(0)")
    public roomNumber?: number;

    // @Validate(Validator="GreaterThan(0)")
    public cost?: number;

    public bookingStartDate?: string;
    public bookingEndDate?: string;
    // @Input(Type="textarea")
    public notes?: string;

    public constructor(init?: Partial<CreateBooking>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'CreateBooking'; }
    public getMethod() { return 'POST'; }
    public createResponse() { return new IdResponse(); }
}

/**
* Update an existing Booking
*/
// @Route("/booking/{Id}", "PATCH")
// @ValidateRequest(Validator="HasRole(`Employee`)")
export class UpdateBooking implements IReturn<IdResponse>, IPatchDb<Booking>
{
    public id?: number;
    public name?: string;
    public roomType?: RoomType;
    // @Validate(Validator="GreaterThan(0)")
    public roomNumber?: number;

    // @Validate(Validator="GreaterThan(0)")
    public cost?: number;

    public bookingStartDate?: string;
    public bookingEndDate?: string;
    public notes?: string;
    public cancelled?: boolean;

    public constructor(init?: Partial<UpdateBooking>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'UpdateBooking'; }
    public getMethod() { return 'PATCH'; }
    public createResponse() { return new IdResponse(); }
}

/**
* Delete a Booking
*/
// @Route("/booking/{Id}", "DELETE")
// @ValidateRequest(Validator="HasRole(`Manager`)")
export class DeleteBooking implements IReturnVoid, IDeleteDb<Booking>
{
    public id?: number;

    public constructor(init?: Partial<DeleteBooking>) { (Object as any).assign(this, init); }
    public getTypeName() { return 'DeleteBooking'; }
    public getMethod() { return 'DELETE'; }
    public createResponse() {}
}

// @ValidateRequest(Validator="IsAuthenticated")
export class CreateClient implements IReturn<CRUDResponse>, ICreateDb<Client>
{
    public code?: string;
    public name?: string;
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
export class CreateLookup implements IReturn<CRUDResponse>, ICreateDb<Lookup>
{
    public lookuptype?: LOOKUPTYPE;
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
    public lookuptype?: LOOKUPTYPE;
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

