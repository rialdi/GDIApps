using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using GDIApps.ServiceModel.Types;

namespace GDIApps.Migrations;

public class Migration1000 : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<Country>();
        Db.CreateTable<Province>();
        Db.CreateTable<City>();
        Db.CreateTable<District>();
        Db.CreateTable<Village>();
        Db.CreateTable<MasterBank>();
        Db.CreateTable<Lookup>();
        Db.CreateTable<ContactUs>();
        Db.CreateTable<Email>();
        Db.CreateTable<EmailTemplate>();
        Db.CreateTable<Client>();
        Db.CreateTable<CBank>();
        Db.CreateTable<CContract>();
        Db.CreateTable<CAddress>();
        Db.CreateTable<Project>();
        Db.CreateTable<Invoice>();
        Db.CreateTable<InvoiceDetail>();
        Db.CreateTable<InvoiceAttachment>();
        
        SeedData();
    }
    
    public override void Down()
    {
        Db.DropTable<MasterBank>();
        Db.DropTable<Village>();
        Db.DropTable<District>();
        Db.DropTable<City>();
        Db.DropTable<Province>();
        Db.DropTable<Country>();
        Db.DropTable<Lookup>();
        Db.DropTable<ContactUs>();
        Db.DropTable<Email>();
        Db.DropTable<EmailTemplate>();
        Db.DropTable<Client>();
        Db.DropTable<CBank>();
        Db.DropTable<CContract>();
        Db.DropTable<CAddress>();
        Db.DropTable<Project>();
        Db.DropTable<Invoice>();
        Db.DropTable<InvoiceDetail>();
        Db.DropTable<InvoiceAttachment>();
    }

    #region Private Methods

    private void SeedData() {
        #region Seed Data

        CreateEmailTemplate(
            EMAIL_TEMPLATE_CODE.APPUSER_EMAIL_CONFIRM, 
            "For AppUser Confirm Email",
            true,
            "-",
            "-"
        );

        #region Lookups
        long lookupId = 0;
        lookupId = CreateDataLookup(LOOKUPTYPE.PRIORITY, "LOW", "LOW", true);
        lookupId = CreateDataLookup(LOOKUPTYPE.PRIORITY, "MEDIUM", "MEDIUM", true);
        lookupId = CreateDataLookup(LOOKUPTYPE.PRIORITY, "HIGH", "HIGH", true);

        lookupId = CreateDataLookup(LOOKUPTYPE.STATUS, "DRAFT", "DRAFT", true);
        lookupId = CreateDataLookup(LOOKUPTYPE.STATUS, "INPROGRESS", "IN PROGRESS", true);
        lookupId = CreateDataLookup(LOOKUPTYPE.STATUS, "COMPLETED", "COMPLETED", true);
        #endregion

        int clientId = 0;
        int projectId = 0;
        int bankId = 0;
        int addressId = 0;
        int contractId = 0; 

        #region Client GDI
        clientId = (int) CreateDataClient("GDI", "GLOBAL DINAMIKA INFORMATIKA", "", true);

        CreateDataCBank(clientId, "BCA", "PT. Global Dinamika Informatika", "012 3016243", "IDR", null, false);
        CreateDataCBank(clientId, "BCA", "PT Global DInamika Informatika", "012 3014691", "USD", "CENAIDJA", false);
        CreateDataCBank(clientId, "Mandiri", "PT. Global DInamika Informatika", "115-0006059762", "IDR", null, false);
        bankId = (int) CreateDataCBank(clientId, "Mandiri", "PT. Global DInamika Informatika", "1150008888028", "USD", "BMRIIDJA", true);

        #endregion


        #region Client VALE

        clientId = (int) CreateDataClient("VALE", "PT VALE INDONESIA", "", true);

        addressId = (int) CreateDataCAddress(clientId, "VALE SOROWAKO", "Indonesia", "Sulawesi Selatan", "Luwu Timur", "Nuha", "Nuha", "Jln. Sumantri Brojonegoro Plant Site Sorowako", null, "92984", "+62215249100", true);
        

        var contractStartDate = new DateTime(2023,1,1);
        var contractEndDate = contractStartDate.AddMonths(12);
        
        contractId = (int) CreateDataCContract(clientId, "4600071270", "IT Application Support And Maintenance", contractStartDate, contractEndDate, 6570000000, "Heru Suprihanto", 6018000000, 60, "IDR", true);
        CreateDataInvoice((int)clientId, (int) contractId, (int) bankId, (int) addressId, "INV-0001", 60, contractStartDate, "Test INV", "098213123", "vAT", "WHT", 100000000, 0, "SUBMITTED");
        
        contractId = (int) CreateDataCContract(clientId, "4600060855", "On Call Custom Application Development Maintenance", contractStartDate, contractEndDate, 6570000000, "Heru Suprihanto", 6018000000, 120, "IDR", true);
        contractId = (int) CreateDataCContract(clientId, "VALE#4508962801", "InEight", contractStartDate, contractEndDate, 45399, "Mrs. Ratri Andaruresmi", null, 60, "USD", true);
        CreateDataInvoice((int)clientId, (int) contractId, (int) bankId, (int) addressId, "INV-0002", 60, contractStartDate.AddMonths(1), "Test INV", "098213123", "vAT", "WHT", 200000000, 0, "SUBMITTED");

        projectId = (int) CreateDataProject(clientId, "IMACSUI", "IMACS UI", "", true);

        
        #endregion

        #region Client POI

        clientId = (int) CreateDataClient("POI", "PREMIER OIL INDONESIA", "", true);
        CreateDataCAddress(clientId, "Premier Oil", "Indonesia", "DKI Jakarta", "Jakarta Selatan", "Pasar Minggu", null, "Gedung CIBIS NINE Lantai 19", "Jl. TB Simatupang No. 2", "12560", "+622150863000", true);
        CreateDataCContract(clientId, "POI#4508962801", "InEight", contractStartDate, contractEndDate, 45399, "Mrs. Ratri Andaruresmi", null, 60, "USD", true);
        
       
        projectId = (int) CreateDataProject(clientId, "POIBSFQR", "POI - Balance Sheet Recon", "", true);

        #endregion

        #region Client PMSI

        clientId = (int) CreateDataClient("PMSI", "PT. Philip Morris Sampoerna International Service Center", "", true);
        CreateDataCAddress(clientId, "Sampoerna", "Indonesia", "Jawa Timur", "Surabaya", "Gunung Anyar", null, "Jl. Rungkut Industri Raya No. 18", null, "60293", "+62318431699", true);
        
        #endregion

        #endregion
    }

    private void CreateEmailTemplate(
        EMAIL_TEMPLATE_CODE emailTemplateCode, string description, bool isActive,
        string emailTemplateText, string emailTemplateHtml
    ) =>
        Db.Insert(new EmailTemplate {
            Code = emailTemplateCode,
            Description = description,
            IsActive = isActive,
            EmailTemplateText = emailTemplateText,
            HtmlTemplate = emailTemplateHtml,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        });

    private long CreateDataLookup(
        LOOKUPTYPE lookupType, string lookupValue, string lookupText, bool isActive 
    ) =>
        Db.Insert(new Lookup {
            LookupType = lookupType,
            LookupValue = lookupValue,
            LookupText = lookupText,
            IsActive = isActive,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        }
    );

    private long CreateDataClient(
        string code, string name, string description, bool isActive
    ) =>
        Db.Insert(new Client {
            Code = code,
            Name = name,
            Description = description,
            IsActive = isActive,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        }, selectIdentity:true
    );

    private long CreateDataCBank(
        long clientId, string bankName, string accountName, string accountNo, string currency, string? swiftCode, bool isMain
    ) =>
        Db.Insert(new CBank {
            ClientId = (int)clientId,
            BankName = bankName,
            AccountName = accountName,
            AccountNo = accountNo,
            Currency = currency,
            SwiftCode = swiftCode,
            IsMain = isMain,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        }, selectIdentity:true
    );

    private long CreateDataCContract(
        int clientId, string contractNo, string description, DateTime startDate, DateTime endDate, 
        decimal totalAmount, string pic, decimal? remainingAmount, int paymentTermDays,
        string currency, bool isActive
    ) =>
        Db.Insert(new CContract {
            ClientId = clientId,
            ContractNo = contractNo,
            Description = description,
            StartDate = startDate,
            EndDate = endDate,
            TotalAmount = totalAmount,
            Currency = currency,
            IsActive = isActive,
            PIC = pic,
            RemainingAmount = remainingAmount,
            PaymentTermDays = paymentTermDays,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        }, selectIdentity:true
    );

    private long CreateDataCAddress(
        int clientId, string addressName, string? country, string? province, string? city, string? district, string? village, 
        string address1, string? address2, string postalCode, string phoneNo, bool isMain
    ) => 
        Db.Insert(new CAddress {
            ClientId = clientId,
            AddressName = addressName,
            Country = country,
            Province = province,
            City = city,
            District = district,
            Village = village,
            Address1 = address1,
            Address2 = address2,
            PostalCode = postalCode,
            PhoneNo = phoneNo,
            IsMain = isMain,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        }, selectIdentity:true
    );

    private long CreateDataInvoice(
        int clientId, int? contractId, int? bankId, int? addressId, string invoiceNo, 
        int? paymentTermDays, DateTime invoiceDate, string? description, string? poNumber,
        string? vat, string? wht, decimal totalAmount, decimal vatAmount, string invoiceStatus
    ) => 
        Db.Insert(new Invoice {
            ClientId = clientId,
            CContractId = contractId,
            CBankId = bankId,
            CAddressId = addressId,
            InvoiceNo = invoiceNo,
            PaymentTermDays = paymentTermDays,
            InvoiceDate = invoiceDate,
            Description = description,
            PONumber = poNumber,
            VAT = vat,
            WHT = wht,
            TotalAmount = totalAmount,
            VATAmount = vatAmount,
            InvoiceStatus = invoiceStatus,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        }, selectIdentity:true
    );

    private long CreateDataProject(
        int clientId, string code, string name, string description, bool isActive
    ) =>
        Db.Insert(new Project {
            ClientId = clientId,
            Code = code,
            Name = name,
            Description = description,
            IsActive = isActive,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        }, selectIdentity:true
    );

    #endregion
}
