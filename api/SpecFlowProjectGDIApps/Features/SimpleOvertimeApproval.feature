Feature: SimpleOvertimeApproval

A short summary of the feature
Background: 
Given Exist Following Employees Data From Common Service
| EMPLOYEE_ID | NAME       | POSITION_ID | POS_DEPT        | SUPERIOR_NAME              | SUPERIOR_POS                   | MOR_NAME                         | MOR_POS                      |
| 0000007142  | NASARUDDIN | FPOCR4      | SMELTER FURNACE | 0000005664 - MUJAHID LAOLA | E00124 - SUPV, FURNACE SHIFT D | 0000008989 - REZA PRIBADI UMBARA | DPPGM5 - MGR, SMELTER FURNAC |
And Exist Employee AD user information From Common Service
| EMPLOYEE_ID | FULL_NAME           | USERNAME | EMAIL                  |
| 0000007142  | NASARUDIN           | nzr150   | nasarudin@vale.com     |
| 0000005664  | MUJAHID LAOLA       | mu3210   | mujahid.laola@vale.com |
| 0000008989  | REZA PRIBADI UMBARA | rzp145   | rezaumbara@vale.com    |
@tag1
	Scenario: 2.Approval by First Approver
	Given Following user is logged in as "TEAMLEADER" at Screen Approval
	| EMPLOYEE_ID | FULL_NAME     | USERNAME | EMAIL                  |
	| 0000005664  | MUJAHID LAOLA | mu3210   | mujahid.laola@vale.com |
	And Exist following overtime data with status "WAITING FOR FIRST APPROVAL"
	| OT_NUMBER           | OT_DATE    | OT_HOUR | OT_REASON | EMPLOYEE_ID | NAME       | POS_DEPT        | POSITION_ID | FIRST_APPROVER_ID | FIRST_APPROVER_NAME | SECONDARY_APPROVER_ID | SECONDARY_APPROVER_NAME |
	| OTS-0000-01-22-7142 | 19-01-2022 | 4       | 01        | 0000007142  | NASARUDDIN | SMELTER FURNACE | FPOCR4      | 0000005664        | MUJAHID LAOLA       | 0000008989            | REZA PRIBADI UMBARA     |
	When User Approve overtime with OT Number "OTS-0000-01-22-7142"
	Then Exist following overtime data with status "WAITING FOR SECOND APPROVAL"
	| OT_NUMBER           | OT_DATE    | OT_HOUR | OT_REASON | EMPLOYEE_ID | NAME       | POS_DEPT        | POSITION_ID | FIRST_APPROVER_ID | FIRST_APPROVER_NAME | SECONDARY_APPROVER_ID | SECONDARY_APPROVER_NAME |
	| OTS-0000-01-22-7142 | 19-01-2022 | 4       | 01        | 0000007142  | NASARUDDIN | SMELTER FURNACE | FPOCR4      | 0000005664        | MUJAHID LAOLA       | 0000008989            | REZA PRIBADI UMBARA     |

	Scenario: 3.Approval by Second approver
	Given Following user is logged in as "TEAMLEADER" at Screen Approval
	| EMPLOYEE_ID | FULL_NAME           | USERNAME | EMAIL               |
	| 0000008989  | REZA PRIBADI UMBARA | rzp145   | rezaumbara@vale.com |
	And Exist following overtime data with status "WAITING FOR SECOND APPROVAL"
	| OT_NUMBER           | OT_DATE    | OT_HOUR | OT_REASON | EMPLOYEE_ID | NAME       | POS_DEPT        | POSITION_ID | FIRST_APPROVER_ID | FIRST_APPROVER_NAME | SECONDARY_APPROVER_ID | SECONDARY_APPROVER_NAME |
	| OTS-0000-01-22-7142 | 19-01-2022 | 4       | 01        | 0000007142  | NASARUDDIN | SMELTER FURNACE | FPOCR4      | 0000005664        | MUJAHID LAOLA       | 0000008989            | REZA PRIBADI UMBARA     |
	When User Approve overtime with OT Number "OTS-0000-01-22-7142"
	Then Exist following overtime data with status "APPROVED"
	| OT_NUMBER           | OT_DATE    | OT_HOUR | OT_REASON | EMPLOYEE_ID | NAME       | POS_DEPT        | POSITION_ID | FIRST_APPROVER_ID | FIRST_APPROVER_NAME | SECONDARY_APPROVER_ID | SECONDARY_APPROVER_NAME |
	| OTS-0000-01-22-7142 | 19-01-2022 | 4       | 01        | 0000007142  | NASARUDDIN | SMELTER FURNACE | FPOCR4      | 0000005664        | MUJAHID LAOLA       | 0000008989            | REZA PRIBADI UMBARA     |
