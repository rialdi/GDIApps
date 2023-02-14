Feature: LookupCRUD

Background: User Admin logged in
Given following user is logged in as admin
| UserName        | Password |
| admin@email.com | p@55wOrd |

@tag1 @adam @screen_CreateClaim.aspx
Scenario: Create Lookup
	When User Input new lookup
	| LookupType | LookupValue | LookupText |
	| PRIORITY   | HIGH        | HIGH       |
	| PRIORITY   | MED         | MEDIUM     |
	| PRIORITY   | LOW         | LOW        |
	Then The new lookup saved

Scenario: Update Lookup
	Given Exist Lookup data in following
	| LookupType | LookupValue | LookupText |
	| PRIORITY   | HIGH        | HIGH       |
	| PRIORITY   | MED         | MEDIUM     |
	| PRIORITY   | LOW         | LOW        |
	And User Change lookup with LookupType "PRIORITY" and value "MED" into
	| LookupType | LookupValue | LookupText |
	| PRIORITY   | NORMAL      | NORMAL     |
	When User Save Lookup
	Then The Lookup is changed successfully

Scenario:  Delete Lookup
	Given Exist Lookup data in following
	| LookupType | LookupValue | LookupText |
	| PRIORITY   | HIGH        | HIGH       |
	| PRIORITY   | MED         | MEDIUM     |
	| PRIORITY   | LOW         | LOW        |
	When User Delete lookup data with type "PRIORITY" and value "LOW"
	Then following lookup is not exists
	| LookupType | LookupValue | LookupText |
	| PRIORITY   | LOW         | LOW        |