Feature: Addition
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers


Scenario: New User Registers
	Given I'm on /
	When I click on the Log On link
	And I click on the Register link
	And I fill in the following:
		| Name				| Value |
		| User name			| John  |
		| Email address		| Galt  |
		| Password			| 54    |
		| Confirm password  | 54    |
	Then I should see "The Password must be at least 6 characters long."

Scenario: Remember me
	Given I'm on /
	When I click on the Log On link
	And I check Remember me?
