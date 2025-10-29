Feature: Login Functionality
	As a SauceDemo user
	I want to log in with valid credentials
	SO I can see the product inventory

	@Smoke
	Scenario: Valid login
		Given I am on the SauceDemo login page
		When I enter valid credentials
		Then I should see the inventory page

	@Negative
	Scenario: Invalid login
		Given I am on the SauceDemo login page
		When I enter invalid credentials
		Then I should see an error message