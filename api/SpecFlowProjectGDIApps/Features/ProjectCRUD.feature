Feature: ProjectCRUD

A short summary of the feature
Background: There are already clients data in database
Given Exist Active Client data in following
| Code | Name        | Description            |
| PTVI | Vale        | PT Vale Indonesia      |
| SMI  | Sampoerna   | PT Sampoerna Indonesia |
| POI  | Premier Oil | PT Premier             |
@manual
Scenario: See Client project List
	When User pick client with code "PTVI"
	Then User can see projects belong to "PTVI" Projects

@manual
Scenario:Add new Project
When User pick client with code "PTVI"
And Add following project input
| Label              | Input              |
| Code               | ODS                |
| Name               | ODS                |
| Description        | untuk monitor data |
| ProjectOwner       | Aan                |
| ProjectManager     | Aminullah          |
| NominalValue       | 345000000          |
| DurationDays       | 120                |
| EstimatedStartDate | 12-Jan-2022        |
| EstimatedEndDate   | 14-Mar-2022        |
| ActualStartDate    | 15-Jan-2022        |
| ActualEndDate      | 9-Mar-2022         |
Then Exist the inputed data projects with code "PTVI"
| Code | Name | ClientCode | ClientName | Description        | ProjectOwner | ProjectManager | NominalValue | DurationDays | EstimatedStartDate | EstimatedEndDate | ActualStartDate | ActualEndDate |
| ODS  | ODS  | PTVI       | Vale       | untuk monitor data | Aan          | Aminullah      | 345000000    | 120          | 12-Jan-2022        | 14-Mar-2022      | 15-Jan-2022     | 9-Mar-2022    |
