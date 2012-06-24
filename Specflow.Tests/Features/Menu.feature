Feature: Addition
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers


Scenario: New User Registers
	Given I'm on /Account/Register
	When I fill in the following:
		|Name				|Value                          |
		|User name			|Analysis Patterns              |
		|Email address		|Bridging the Communication Gap |
		|Password			|Analysis Patterns              |
		|Confirm password	|Bridging the Communication Gap |
	And I select Blah from Test
	And I click on the Register button
	Then I should see "The password and confirmation password do not match."

