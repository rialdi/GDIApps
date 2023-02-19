Feature: CreateOvertime

A short summary of the feature

@tag1
Scenario: create claim data
	Given user "aldi" want create New Overtime data like following
	| UserName | Reason    | Task   | OvertimeDate |
	| aldi     | Emergency | Deploy | 14-FEB-2022  |
	When  User Submit The Overtime Data
	Then Exist Following Data in Database
	| OTNumber         | UserName | OvertimeDate | Status    | Task   |
	| OTS-0001-02-2022 | aldi     | 14-FEB-2022  | Submitted | Deploy |
	
