Feature: Weather forecast
Testing Weather forecast rest api
@mytag
Scenario: Get list of forecast
	Given Weather forecast is running
	And I am logged in as 'User A'
	When I called Get Weather forecast
	Then I should get list of forecast