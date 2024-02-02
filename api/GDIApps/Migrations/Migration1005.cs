using ServiceStack;
using ServiceStack.DataAnnotations;
using System.Collections;
using ServiceStack.OrmLite;
using GDIApps.ServiceModel.Types;

namespace GDIApps.Migrations;

public class Migration1005 : MigrationBase
{
    private int CurrAppUserId { get; set; }
    public override void Up()
    {
        Db.CreateTable<EmpLeave>();
        Db.CreateTable<EmpReimbursement>();
        Db.CreateTable<EmpFamilyMember>();
        Db.CreateTable<EmpResume>();
        Db.CreateTable<IdentitiesNumber>();

        SeedData();
    }

    public override void Down()
    {
        Db.DropTable<IdentitiesNumber>();
        Db.DropTable<EmpResume>();
        Db.DropTable<EmpFamilyMember>();
        Db.DropTable<EmpReimbursement>();
        Db.DropTable<EmpLeave>();
    }

    #region Private Methods

    private void SeedData() {
        CurrAppUserId = Db.Select<AppUser>(w => w.Email == "rialdi@ptgdi.com").Select(s => s.Id).FirstOrDefault(0);

        if(CurrAppUserId == 0)
            return;

        #region CreateEmpResume

        #region FORMAL_EDUCATION
        CreateEmpResume(
            EMP_RESUME_TYPE.FORMAL_EDUCATION,
            1,
            new DateTime(1987, 7, 1),
            new DateTime(1993, 6, 30),
            "ELEMENTARY SCHOOL",
            "SD Mathalul Anwar, Bandung",
            "",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.FORMAL_EDUCATION,
            2,
            new DateTime(1993, 7, 1),
            new DateTime(1996, 6, 30),
            "JUNIOR HIGH SCHOOL",
            "SLTP Negeri 8, Bandung",
            "",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.FORMAL_EDUCATION,
            3,
            new DateTime(1996, 7, 1),
            new DateTime(1999, 6, 30),
            "SENIOR HIGH SCHOOL",
            "SMU Negeri 8, Bandung",
            "",
            null,
            null
        );

        #endregion

        #region PROFESIONAL_EDUCATION
        CreateEmpResume(
            EMP_RESUME_TYPE.PROFESIONAL_EDUCATION,
            1,
            new DateTime(1999, 9, 1),
            new DateTime(2002, 6, 30),
            "DIPLOMA DEGREE COMPUTER ENGINEERING",
            "",
            "Politeknik Negeri Bandung,  With GPA 2.6",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROFESIONAL_EDUCATION,
            2,
            new DateTime(2005, 9, 1),
            new DateTime(2008, 6, 30),
            "BACHELOR DEGREE IN INFORMATION TECHNOLOGY MAJOR",
            "",
            "Universitas Mercu Buana,  with GPA 3.5",
            null,
            null
        );
        #endregion

        #region PROFESIONAL_EXPERIENCE
        CreateEmpResume(
            EMP_RESUME_TYPE.PROFESIONAL_EXPERIENCE,
            1,
            new DateTime(2002, 9, 1),
            new DateTime(2003, 6, 30),
            "PROGRAMMER",
            "",
            "PT. ARSINTEGRASI",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROFESIONAL_EXPERIENCE,
            2,
            new DateTime(2003, 9, 1),
            DateTime.MaxValue,
            "SENIOR SYSTEM ANALYST & DEVELOPER",
            "",
            "PT. GLOBAL DINAMIKA INFORMATIKA",
            null,
            null
        );
        #endregion

        #region  PROFESIONAL_TRAINING
        CreateEmpResume(
            EMP_RESUME_TYPE.PROFESIONAL_TRAINING,
            1,
            null,
            null,
            "K2 BLACKPEARL",
            "",
            "",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROFESIONAL_TRAINING,
            2,
            null,
            null,
            "CERTIFIED SCRUM MASTER",
            "",
            "",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROFESIONAL_TRAINING,
            3,
            null,
            null,
            "QLIKVIEW DEVELOPER",
            "",
            "",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROFESIONAL_TRAINING,
            4,
            null,
            null,
            "IBM COGNOS TM1",
            "",
            "",
            null,
            null
        );
        #endregion

        #region PROJECT_EXPERIENCE
        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            1,
            new DateTime(2023, 10, 1),
            new DateTime(2024, 1, 1),
            "Hospital Management System Product",
            "PT ARSIntegrasi",
            "Develop a Hospital Management System Product. The system is written in Borland’s Delphi.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            2,
            new DateTime(2002, 9, 1),
            new DateTime(2003, 10, 1),
            "Product Bag Scale Data Acquisition application using Visual Studio .NET",
            "PT INCO",
            "The application enables acquisition of weight data automatically from the scale and submit the product weight and other information to Product Bag application which is a system that produces product information to accompany finished nickel product during shipment.  The Scale Data Acquisition application is used by the Packaging section from Process Technology Department. While the Product Bag application is used by three departments: Process Technology, Logistics, and Accounting. The product information, e.g. bag weight, chemical assay, and screen test result, originates from packaging and metals accounting. Shipment information, e.g. weight statement, comes from logistics (for shipping document) and invoicing information is from accounting. The system is written in Microsoft Visual C# .NET in 3-tier architecture. Front-tier using ASP.NET, Middle-tier using Web Services, Data-tier using Enterprise Services.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            3,
            new DateTime(2003, 3, 1),
            new DateTime(2005, 5, 1),
            "Developing INCO’s IMAcS (Integrated Metallurgical Accounting System) application using Visual Studio .NET.",
            "PT INCO",
            "The application supports the Metal Accounting section in in processing the material data, assay data, and bulk consumption data all through the flow sheet for analysis and to produce daily report, monthly report, quarterly report, and yearly report. The system is written in Microsoft Visual C# .NET in 3-tier architecture. Front-tier using ASP.NET, Middle-tier using Web Services, Data-tier using Enterprise Services.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            4,
            new DateTime(2005, 7, 1),
            new DateTime(2005, 12, 1),
            "Developing INCO’s HR Intranet using Visual Studio .NET.",
            "PT INCO",
            "The application enables updating the content of HR Intranet conveniently using the web browser. Technology: Microsoft Visual C# .NET using Rainbow Portal framework.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            5,
            new DateTime(2006, 1, 1),
            new DateTime(2006, 11, 1),
            "Developing SAKBI (Sistem Administrasi Kredit Bank Indonesia) system.",
            "Bank Indonesia",
            "The application enables the administration of all loans from Bank Indonesia. The rule are very complex and specific to the loan type. The system is written in C# using ECO (Enterprise Core Objects) framework. Technology: Windows .Net, Windows Scheduler, Oracle, XtraReport",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            6,
            new DateTime(2006, 12, 1),
            new DateTime(2006, 12, 1),
            "Conducting Microsoft Reporting Services, Microsoft Office SharePoint Server and Dot Net Nuke (DNN) training",
            "Premier Oil",
            "The custom training that Premier Oil decided training syllabus. Technology: ASP.Net 1.1, DotNetNuke, MOSS, MS SQL 2005",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            7,
            new DateTime(2007, 1, 1),
            new DateTime(2007, 9, 1),
            "Converting plant production module regarding Microsoft Dynamics Axapta implementation",
            "Capsugel (Pfizer Group)",
            "The new module had to interface data between plant production module and Axapta. The converting had been written in VB .Net. Developing reports using Microsoft Reporting Services. Data source is Axapta. Technology: ASP.Net 1.1, Windows Scheduler, SQL Server 2000, Reporting Services",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            8,
            new DateTime(2007, 10, 1),
            new DateTime(2008, 3, 1),
            "Developing Equipment, Logistic, Maintenance, Finance & SCM reports using Microsoft Reporting Services",
            "Premier Oil",
            "Data source is MP2. Technology: SQL Server 2005, Reporting Services",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            9,
            new DateTime(2008, 3, 1),
            new DateTime(2008, 6, 1),
            "Developing ITS (Invoice Tracking System)",
            "PT Bukit Asam",
            "The application enables PT BA to monitor the Invoice documents flow. The system is written in Microsoft Visual C# .NET with DevExpress component. Technology: ASP.Net 2.0, Devexpress, Windows Scheduler, Oracle, XtraReport",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            10,
            new DateTime(2008, 7, 1),
            new DateTime(2010, 7, 1),
            "Developing custom applications such as Performance Management System",
            "PT Freeport McMoran",
            "Online Requisition, Leave Travel Management System using Microsoft Visual C#. Net. Technology: ASP.Net 2.0, Jquery, SQL Server 2008, Oracle",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            11,
            new DateTime(2008, 7, 1),
            new DateTime(2010, 7, 1),
            "Developing custom applications integrated to SAP system",
            "PT INCO",
            "Since PT.INCO moving they ERP system from Ellipse to SAP, there some application such as IMAcS, PBIS, EDS needs to re integrate from Ellipse to SAP. Technology: ASP.Net 2.0, Web Service, Windows Scheduler, Oracle 11g, TIBCO",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            12,
            new DateTime(2011, 3, 1),
            new DateTime(2011, 6, 1),
            "Developing iPOS (integrated Point Of Sales) application that integrated with Netsuite CRM system",
            "Insight Unlimited",
            "iPOS is windows based application build in .NET 3.5. it support barcode, cash drawer & PoS printer. iPOS is a standalone application but it can be synchronize to Netsuite CRM system. Technology: Windows .Net 3.5, Devexpress, Windows Scheduler, SQL Server 2012",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            13,
            new DateTime(2011, 10, 1),
            new DateTime(2011, 12, 1),
            "Developing enhancement of existing application iMOC (integrated Management Of Change)",
            "PT PHE",
            "iMOC build in ASP.NET with oracle 10g database for tracking submitted request of change. In this enhancement user can download request template form & uploaded that form to application with xlsm files. Technology: ASP.Net 2.0, Devexpress, Jquery, SQL Server 2012, Excel Macro",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            14,
            new DateTime(2011, 12, 1),
            new DateTime(2012, 6, 1),
            "IMAcS Reborn – ASP.Net Framework 4.0, Jquery, FarPoint Excel web, Windows Scheduler, Oracle11g, SQL Server 2012, Microsoft SQL Reporting Services",
            "PT Vale Indonesia",
            "Develop rewrite application of Integrated Metal Accounting System (IMAcS). IMAcS is a configurable formula application to calculate Metal Accounting process. With IMAcS application, PT Vale Indonesia can predict how many nickel will be produce every month. IMAcS is a web application build in ASP.NET with oracle 11g database that integrated to SAP system. PT Vale Indonesia has been using this IMAcS application from 2005 until now. Technology: ASP.Net 4, Jquery, FarPoint Excel web, Windows Scheduler, Oracle11g, SQL Server 2012, Reporting Services",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            15,
            new DateTime(2012, 8, 1),
            new DateTime(2013, 2, 1),
            "Service Acceptance Notes – ASP.Net Framework 4.0, Devexpress, Jquery, Windows Scheduler, SQL Server 2012, Microsoft SQL Reporting Services, Sun System",
            "Premier Oil",
            "Develop Service Acceptance Notes (SAN) application. SAN build in ASP.Net and SQL Server 2012 database. SAN can allow Vendors or Supplier to submit their Service Notes to Premier Oil. SAN is integrated to JDE system so every vendor can actually know their remaining contract value. there is Online Approval for submitted Service Notes Involving contract owner and other responsible person.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            16,
            new DateTime(2013, 3, 1),
            new DateTime(2013, 10, 1),
            "JDE Posting – JDE, .Net Windows Scheduler, Oracle",
            "Premier Oil",
            "Develop custom application to automate the JD Edwards Journal Posting process and interfacing into Sun System Financial Applications.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            17,
            new DateTime(2013, 10, 1),
            new DateTime(2014, 5, 1),
            "SunSET V.1 – ASP.Net Framework 4.0, Devexpress, Jquery, Windows Scheduler, SQL Server 2012, Microsoft SQL Reporting Service, Microsoft SQL SSIS",
            "Premier Oil",
            "Develop custom application named Sun System Extended Tools (SunSET). SunSET application is a calculation application where the formula is configurable; this is designed for Finance Department such as calculate Joint Venture Billing, PCO & Gross to Net value, etc. SunSET is build based on ASP.Net platform with SQL Server 2012 database and Sun System DB as primary data source.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            18,
            new DateTime(2013, 12, 1),
            new DateTime(2013, 12, 1),
            "Maxi Sun Interface Development – Maxi Sun, Sun System, SQL Server 2000, Oracle JDE, Microsoft SQL SSIS",
            "Premier Oil",
            "Develop Maxi SUN interface for Payroll & Time Writing of JD Edwards modules. Maxi SUN interface is an application designed for interfacing JDE data to Sun System application or vice versa. It will transfer JDE journal data and post it to Sun System applications.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            19,
            new DateTime(2013, 12, 1),
            new DateTime(2014, 1, 1),
            "JVBL Reports – Sun System, SQL Server 2000, Microsoft SQL Reporting Services, Microsoft SQL SSIS",
            "Premier Oil",
            "Develop Joint Venture Billing Report for Finance Department with Sun System database as data source. Join Venture Billing Report is SunSET (Sun System Extended Tools) report modules build in Microsoft SQL Reporting Services 2012 to displayed in a web browser and automatically generate report in excel format.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            20,
            new DateTime(2014, 1, 1),
            new DateTime(2014, 2, 1),
            "FQR Reports – Sun System, SQL Server 2000, Microsoft SQL Reporting Services, Microsoft SQL SSIS",
            "Premier Oil",
            "Develop FQR reports for Finance Department using Microsoft SQL Reporting Services 2012 with Sun System database as data source. FQR Reports is SunSET (Sun System Extended Tools) report modules to be displayed in a web browser and automatically generate report in excel format.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            21,
            new DateTime(2012, 8, 1),
            new DateTime(2014, 2, 1),
            "Finance Support – SQL Server 2000, Oracle 11g, JDE, Maxi SUN & Sun System",
            "Premier Oil",
            "Do problem support every end of month for Finance Closing and Develop reconciliation report between JDE and Sun System.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            22,
            new DateTime(2014, 2, 1),
            new DateTime(2014, 6, 1),
            "Business Intelligence Report Development – Qlikview, SQL Server 2012, Oracle, Sharepoint",
            "Premier Oil",
            "Analyse transactional report from Finance & SCM Department and develop Business Intelligence reports as an analysing reports for Management Level. The BI reports include Budget vs Actual Reports, Blanket Contract Utilization, Contract Analysis, Cost Saving, Lead Time, etc.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            23,
            new DateTime(2014, 7, 1),
            new DateTime(2014, 12, 1),
            "Sun System Extended Tools (SunSET)",
            "Premier Oil",
            "Develop web based Calculation tools to support Sun System Financial Application Software. SunSET is a configurable application that configured by Financial Accountant. SunSET can Calculate formula, export value to Sun System & generate JVBL Reports automatically.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            24,
            new DateTime(2015, 1, 1),
            new DateTime(2015, 5, 1),
            "Sun System Extended Tools (SunSET) For FQR Reports",
            "Premier Oil",
            "Enhances SunSET to accommodate FQR Calculations. Develop FQR Report Templates.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            25,
            new DateTime(2015, 6, 1),
            new DateTime(2016, 1, 1),
            "K2 Blackpearl – Prototype for HR Department",
            "Premier Oil",
            "Develop 4 prototype workflow applications using K2 Blackpearl workflow engine. HR Medical Reimbursement, Business Card Requisition, Travel Request & Handphone Simcard Request. Create Coding convention for further developing K2 Workflow Projects at Premier Oil",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            26,
            new DateTime(2016, 1, 1),
            new DateTime(2016, 3, 1),
            "Create Coding convention for further developing K2 Workflow Projects",
            "Premier Oil",
            "Setup K2 Environment, Project Template, Design and develop K2 Workflow, K2 SmartObject that using external database, and k2 smartforms to be used for approval process for Leave Cash Out, Travel Authorization Leave Cash Out, Education Allowance modules.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            27,
            new DateTime(2016, 3, 1),
            new DateTime(2016, 10, 1),
            "SunSET FQR Module",
            "Premier Oil",
            "Enhance SunSET calculation engine to provide FQR data & submitting to SKK Migas.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            28,
            new DateTime(2016, 10, 1),
            new DateTime(2016, 12, 1),
            "SOT FQR ",
            "Premier Oil",
            "Develop Sistem Operasi Terpadu FQR for submitting Permier Oil Finance report to SKK Migas automatically. This system builded with Microsoft Reporting Services and SQL Server Integration Services.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            29,
            new DateTime(2017, 1, 1),
            new DateTime(2017, 12, 1),
            "Enhance FQR with SKK Migas 2017 new Template ",
            "Premier Oil",
            "Modify FQR to adjust SKK Migas 2017 new Tempalte ",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            30,
            new DateTime(2018, 1, 1),
            new DateTime(2018, 6, 1),
            "ATSAR (Automated Tracking System and Reporting)",
            "Vale Indonesia",
            "Create Automated Calculation Engine and configurable Statistical chart and Reporting email ",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            31,
            new DateTime(2018, 6, 1),
            new DateTime(2018, 12, 1),
            "EWR (Electronic Work Request) K2 Workflow",
            "Vale Indonesia",
            "Create K2 Workflow, smartform and smart object within two database EWRDB for transaction and HRDB to get Vale employee data for approval.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            32,
            new DateTime(2018, 6, 1),
            new DateTime(2019, 10, 1),
            "Create SOT FQR Automation Report & Create Cashcall Report",
            "Premier Oil",
            "FQR Automation Report is a configurable report from SunSystem to push automatically data to SKK Migas Server based on scheduler. Cash Call report is a configurable Report build based on C# WebApp and Reporting Services 2016. ",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            33,
            new DateTime(2019, 10, 1),
            new DateTime(2019, 10, 1),
            "K2 Server Migration",
            "Premier Oil",
            "Migrate old K2 Server from Windows Server 2008 and SQL 2012 to new Server 2016 & SQL Server 2016",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            34,
            new DateTime(2019, 11, 1),
            new DateTime(2020, 6, 1),
            "SunQuest (SunSystem Query & Extended Tools)",
            "Premier Oil",
            "New MaxiSUN Interface application is a configurable tools for creating scheduler ETL data between SunSystem and JDEdwards erp application. This application build in C# contains MVC5 web app for configure ETL and console application for Loading data between those two ERP system",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            35,
            new DateTime(2020, 3, 1),
            new DateTime(2020, 11, 1),
            "Develop Finance Cash Cost Reporting",
            "Vale Indonesia",
            "Design and develop Datawarehouse cube store to Microsoft Analysis Services for Finance reporting.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            36,
            new DateTime(2021, 3, 1),
            new DateTime(2021, 11, 1),
            "Develop VAMA Asset Management Application V.1",
            "Vale Indonesia",
            "Design and develop VAMA Asset Management Application for Vale Maintenance Department using ASP.NET & Kendo.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            37,
            new DateTime(2021, 12, 1),
            new DateTime(2022, 7, 1),
            "Develop VAMA Asset Management Application V.2",
            "Vale Indonesia",
            "Design and develop VAMA Asset Management Application Phase 2 for Vale Maintenance Department using ASP.NET, Kendo & MS Reporting Services.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            38,
            new DateTime(2022, 7, 1),
            new DateTime(2023, 1, 1),
            "Develop Balance Sheet Power Apps Application V.1",
            "Harbour Energy",
            "Design and develop Balance Sheet & Reconciliation data with Approval Flow using Microsoft Power Platform (Power Bi, Power Apps, Power Automate, SharePoint, SSIS & Excel).",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            39,
            new DateTime(2023, 1, 1),
            new DateTime(2023, 6, 1),
            "Develop VAMA Asset Management Application V.3",
            "Vale Indonesia",
            "Design and develop VAMA Asset Management Application Phase 3 for Vale Maintenance Department using ASP.NET, Kendo & MS Reporting Services.",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            40,
            new DateTime(2023, 7, 1),
            new DateTime(2023, 12, 1),
            "Develop Balance Sheet Power Apps Application V.2",
            "Harbour Energy",
            "Add Finpack Approval Module",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            41,
            new DateTime(2024, 1, 1),
            null,
            "Develop Balance Sheet Power Apps Application V.3",
            "Harbour Energy",
            "Merge Monthly, Quarterly & Finpack Approval PowerApps into Single Balance Sheet Approval System",
            null,
            null
        );

        CreateEmpResume(
            EMP_RESUME_TYPE.PROJECT_EXPERIENCE,
            42,
            new DateTime(2024, 1, 1),
            null,
            "Develop GDI Apps for Internal GDI",
            "PT.GDI Product",
            "Cloud based web application using Servicestak, Kendo UI, Typescript, Vue3, ViteJS, SQL Server.",
            null,
            null
        );

        #endregion
    
        #endregion
    
        #region CreateFamilyMember

        // CreateFamilyMember(FAMILY_MEMBER_TYPE.HUSBAND, 1, LIVING_STATUS.ALIVE, GENDER.MALE, "RIALDI KURNIAWAN", "Rialdi", new DateTime(1981,6,7), "JAKARTA", "6281382910888");
        CreateFamilyMember(FAMILY_MEMBER_TYPE.WIFE, 1, LIVING_STATUS.ALIVE, GENDER.FEMALE, "Ira Pramitasari", "Ira", new DateTime(1985,3,20), "BANDUNG", "62817438568");
        CreateFamilyMember(FAMILY_MEMBER_TYPE.CHILD, 1, LIVING_STATUS.ALIVE, GENDER.FEMALE, "Belinda Bilqis Ziraili", "Bilqis", new DateTime(2009,5,21), "BANDUNG", "");
        CreateFamilyMember(FAMILY_MEMBER_TYPE.CHILD, 2, LIVING_STATUS.ALIVE, GENDER.FEMALE, "Leticia Latifa Ziraili", "Lea", new DateTime(2012,2,23), "BANDUNG", "");
        CreateFamilyMember(FAMILY_MEMBER_TYPE.CHILD, 3, LIVING_STATUS.ALIVE, GENDER.FEMALE, "Haziel Hatim Zufaro", "Hatim", new DateTime(2019,10,8), "DEPOK", "");
        
        #endregion

    }

    private long CreateEmpResume(
        EMP_RESUME_TYPE resumeType, int resumeNo, DateTime? startDate, DateTime? endDate, string title, string subTitle, string description, 
        DateTime? certificationPublishedDate, DateTime? certificationExpiredDate
    ) =>
        Db.Insert(new EmpResume {
            AppUserId = CurrAppUserId,
            ResumeType = resumeType,
            ResumeNo = resumeNo,
            StartDate = startDate,
            EndDate = endDate,
            Title = title,
            SubTitle = subTitle,
            Description = description,
            CertificationPublishedDate = certificationPublishedDate,
            CertificationExpiredDate = certificationExpiredDate,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        }, selectIdentity:true
    );

    private long CreateFamilyMember(
        FAMILY_MEMBER_TYPE memberType, int memberNo, LIVING_STATUS livingStatus, GENDER gender, string fullName, string nickName, 
        DateTime birthDate, string placeOfBirth, string phoneNo
    ) =>
        Db.Insert(new EmpFamilyMember {
            AppUserId = CurrAppUserId,
            MemberType = memberType,
            MemberNo = memberNo,
            LivingStatus = livingStatus,
            Gender = gender,
            FullName = fullName,
            NickName = nickName,
            BirthDate = birthDate,
            PlaceOfBirth = placeOfBirth,
            PhoneNo = phoneNo,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        }, selectIdentity:true
    );

    #endregion
}