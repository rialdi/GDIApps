using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using GDIApps.ServiceModel.Types;

namespace GDIApps.Migrations;

public class Migration1001 : MigrationBase
{
    // https://docs.servicestack.net/ormlite/db-migrations#declarative-code-first-migrations
    public override void Up()
    {
        // Db.RenameColumn<MyTable>("FullName", "Name");
        // Db.AddColumn<Client>(c => c.PhoneNo);
        // Db.AddColumn<Client>(c => c.Email);
        Db.CreateTable<CBank>();
        Db.CreateTable<CContract>();

        Db.CreateTable<CAddress>();
        Db.CreateTable<Invoice>();
        Db.CreateTable<InvoiceDetail>();

        CreateDataCBank(1, "BCA", "PT. Global Dinamika Informatika", "012 3016243", "IDR", null, true);
        CreateDataCBank(1, "BCA", "PT Global DInamika Informatika", "012 3014691", "USD", "CENAIDJA", false);
        CreateDataCBank(1, "Mandiri", "PT. Global DInamika Informatika", "115-0006059762", "IDR", null, false);
        CreateDataCBank(1, "Mandiri", "PT. Global DInamika Informatika", "1150008888028", "USD", "BMRIIDJA", false);

        var contractStartDate = new DateTime(2023,1,1);
        var contractEndDate = contractStartDate.AddMonths(12);
        CreateDataCContract(2, "4600071270", "IT Application Support And Maintenance", contractStartDate, contractEndDate, 6570000000, "Heru Suprihanto", 6018000000, 60, "IDR", true);
        CreateDataCContract(2, "4600060855", "On Call Custom Application Development Maintenance", contractStartDate, contractEndDate, 6570000000, "Heru Suprihanto", 6018000000, 120, "IDR", true);
        CreateDataCContract(2, "PO#4508962801", "InEight", contractStartDate, contractEndDate, 45399, "Mrs. Ratri Andaruresmi", null, 60, "USD", true);
        
        CreateDataCContract(4, "PO#4508962801", "InEight", contractStartDate, contractEndDate, 45399, "Mrs. Ratri Andaruresmi", null, 60, "USD", true);
        
    
        CreateDataCAddress(2, "VALE SOROWAKO", "Indonesia", "Sulawesi Selatan", "Luwu Timur", "Nuha", "Nuha", "Jln. Sumantri Brojonegoro Plant Site Sorowako", null, "92984", "+62215249100", true);
        CreateDataCAddress(3, "Sampoerna", "Indonesia", "Jawa Timur", "Surabaya", "Gunung Anyar", null, "Jl. Rungkut Industri Raya No. 18", null, "60293", "+62318431699", true);
        CreateDataCAddress(4, "Premier Oil", "Indonesia", "DKI Jakarta", "Jakarta Selatan", "Pasar Minggu", null, "Gedung CIBIS NINE Lantai 19", "Jl. TB Simatupang No. 2", "12560", "+622150863000", true);
    
        CreateDataInvoice(1, 1, 1, 1, "INV-0001", 60, contractStartDate, "Test INV", "098213123", "vAT", "WHT", 100000000, 0, "SUBMITTED");
        CreateDataInvoice(1, 1, 1, 1, "INV-0002", 60, contractStartDate.AddMonths(1), "Test INV", "098213123", "vAT", "WHT", 200000000, 0, "SUBMITTED");
    }
    
    public override void Down()
    {
        Db.DropColumn<Client>(c => c.PhoneNo);
        Db.DropColumn<Client>(c => c.Email);
        Db.DropTable<CBank>();
        Db.DropTable<CContract>();

        Db.DropTable<CAddress>();
        Db.DropTable<Invoice>();
        Db.DropTable<InvoiceDetail>();
    }

    #region Private Methods

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
        }
    );

    private long CreateDataCContract(
        long clientId, string contractNo, string description, DateTime startDate, DateTime endDate, 
        decimal totalAmount, string pic, decimal? remainingAmount, int paymentTermDays,
        string currency, bool isActive
    ) =>
        Db.Insert(new CContract {
            ClientId = (int)clientId,
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
        }
    );

    private long CreateDataCAddress(
        long clientId, string addressName, string? country, string? province, string? city, string? district, string? village, 
        string address1, string? address2, string postalCode, string phoneNo, bool isMain
    ) => 
        Db.Insert(new CAddress {
            ClientId = (int)clientId,
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
        }
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
        }
    );

    #endregion

}
