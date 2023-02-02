# Development Guidance

# Add Dotnet Trust HTTPS
```bash
dotnet dev-certs https --check --trust
```

# Reference Docs
- https://docs.servicestack.net/ormlite/reference-support#merge-disconnected-poco-result-sets

## Create New Table
- Go to folder /api/GDIApps.ServiceModel/Types/Tables
- Right Click on folder Tables and click New File
- Type Table name ending with ".cs" (Example: Client.cs)
- Copy template from Lookup.cs
- Modify the field property
- if the field data type is an Enum type please refer the Application Enum Section

## Add New Application Enum
- Open file /api/GDIApps.ServiceModel/Types/App/AppEnums.cs
- Add new Enum like "public enum LOOKUPTYPE"

## Create DB Migrations
- go to folder /api/GDIApps/Migrations
- Copy and Paste the last Migration.cs
- Rename the copy file into (last + 1) Migration name
- Open & edit the last migration file
- Change "public class Migration1003 : MigrationBase" into new migration name (example: "public class Migration1004: MigrationBase")
- Change public override void Up & down to create new table
- Run the new migrations by 

## Run DB Migrations
- Open package.json file in folder /ui/
- Right click on => "scripts" => "migrate"
- Click "Run Script"
- to ensure new table is created, go to https://localhost:5005/admin-ui/database (login as administrator). new table Lookup si created

## Create standard CRUD API
- go to /api/GDIApps.ServiceModel
- Copy & Paste EmailTemplates.cs
- Rename "EmailTemplate copy.cs" to new TableName api to be created end with "s" (Example: Lookups.cs)
- Open & edit Lookups.cs
- Replace All "EmailTemplates" with "Lookups"
- Replace All "EmailTemplate" with "Lookup"
- Modify public class QueryLookups
- Modify public class CreateLookup
- Modify public class UpdateLookup
- Rebuild the project
- to ensure new APIs is created, go to https://localhost:5005/ui-admin

## Update dtos.ts for client script function
- Open package.json file in folder /ui/
- Right click on => "scripts" => "dtos"
- Click "Run Script"
- to ensure New APIs existing in dtos.ts, open dtos.ts inside folder /api/src/