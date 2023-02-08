Feature: ServiceStackHello

A short summary of the feature

@tag1
Scenario: Connect ke servicestack
	Given Exist url service stack  "http://localhost:5001/"
	And  User login as 
	| UserName        | Password |
	| admin@email.com | p@55wOrd |
	When User rquest hello world with name "Adam"
	Then the hello response should be "Hello, Adam!"