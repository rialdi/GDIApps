Feature: SimpleOvertime

A short summary of the feature
***Mockup:***
![Create Claim Mockup](./Mockups/CreateClaim.png)
Background: 
Given Exist Following Employees Data From Common Service
| EMPLOYEE_ID | NAME       | POSITION_ID | POS_DEPT        | SUPERIOR_NAME              | SUPERIOR_POS                   | MOR_NAME                         | MOR_POS                      |
| 0000007142  | NASARUDDIN | FPOCR4      | SMELTER FURNACE | 0000005664 - MUJAHID LAOLA | E00124 - SUPV, FURNACE SHIFT D | 0000008989 - REZA PRIBADI UMBARA | DPPGM5 - MGR, SMELTER FURNAC |
And Exist Employee AD user information From Common Service
| EMPLOYEE_ID | FULL_NAME           | USERNAME | EMAIL                  |
| 0000007142  | NASARUDIN           | nzr150   | nasarudin@vale.com     |
| 0000005664  | MUJAHID LAOLA       | mu3210   | mujahid.laola@vale.com |
| 0000008989  | REZA PRIBADI UMBARA | rzp145   | rezaumbara@vale.com    |
And Exist following overtime reasons
| Reason    |
| Training  |
| Emergency |
| Leave     |

And Todays date is "19-FEB-2023"

@tag1 @mockCommonService
Scenario: 1.Create Draft Claim by admin
	Given following user is logged in as admin
		| UserName        | Password |
		| admin@email.com | p@55wOrd |
	And user able to select date from 30 days ago until today
	And user able to select these following employee data
	| EMPLOYEE_ID | NAME       | POS_DEPT        |
	| 0000007142  | NASARUDDIN | SMELTER FURNACE |
	When User Create new Claim with date "19-FEB-2023" and selected employees are
	| EMPLOYEE_ID | NAME       |
	| 0000007142  | NASARUDDIN |
	Then Exist following overtime data with status "DRAFT"
	| OT_NUMBER             | OT_DATE    | OT_HOUR | OT_REASON | EMPLOYEE_ID | NAME       | POS_DEPT        | POSITION_ID |
	| OTS-0000-02-2023-7142 | 19-02-2023 | <null>  | <null>    | 0000007142  | NASARUDDIN | SMELTER FURNACE | FPOCR4      |
	When User submit an overtime with ot number "OTS-0000-02-2023-7142" with following entries
		| Label     | Input    |
		| OT_HOUR   | 4        |
		| OT_REASON | Training |

	Then Exist following overtime data with status "WAITING FOR FIRST APPROVAL"
	| OT_NUMBER           | OT_DATE    | OT_HOUR | OT_REASON | EMPLOYEE_ID | NAME       | POS_DEPT        | POSITION_ID | FIRST_APPROVER_ID | FIRST_APPROVER_NAME | SECONDARY_APPROVER_ID | SECONDARY_APPROVER_NAME |
	| OTS-0000-02-2023-7142 | 19-02-2023 | 4       | Training      | 0000007142  | NASARUDDIN | SMELTER FURNACE | FPOCR4      | 0000005664        | MUJAHID LAOLA       | 0000008989            | REZA PRIBADI UMBARA     |









