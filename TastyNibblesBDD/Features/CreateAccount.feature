Feature: CreateAccount

A new user is creating an Account
@End_to_End_1
Scenario Outline: User creates an account
	Given User is on the home page
	When User clicks on Account button
	Then User will be redirected to Account page
	When User clicks on Create Account
	Then User will be redirected to Create Account Page
	When User enters first name '<firstName>'
	* User enters last name '<lastName>'
	* User enters email '<email>'
	* User enters password '<password>'
	Then User can create an account
Examples: 
	| firstName | lastName | email             | password |
	| john      | doe      | johndoe@gmail.com | 111222@@ |