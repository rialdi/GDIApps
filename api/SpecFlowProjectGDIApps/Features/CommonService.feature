Feature: CommonService

test penggunaan common service, koneksi VPN diperlukan
Background: Config param
Given BaseAPIUrl is "http://sorappsrvp04/PTVI.CommonServices/api/externaldata/"
@tag1
Scenario: Get Employee from Common Service
	When calling common service with code "QRY_EMP_APPROVAL" and parameter "$top=10"
	Then the result is contains 10 data

Scenario: Get Employee By badge number
	When calling common service employee with BN "0000007710"
	Then the result is contains 1 data

@mockCommonService
Scenario: Mocking Common Service
	Given Exist Following Employees Data From Common Service
	| EMPLOYEE_ID | NAME       | POSITION_ID | POS_DEPT        | SUPERIOR_NAME              | SUPERIOR_POS                   | MOR_NAME                         | MOR_POS                      |
	| 0000007142  | NASARUDDIN | FPOCR4      | SMELTER FURNACE | 0000005664 - MUJAHID LAOLA | E00124 - SUPV, FURNACE SHIFT D | 0000008989 - REZA PRIBADI UMBARA | DPPGM5 - MGR, SMELTER FURNAC |
	When calling common service via injection to get employee with BN "0000007142"
	Then the result is contains 1 data



