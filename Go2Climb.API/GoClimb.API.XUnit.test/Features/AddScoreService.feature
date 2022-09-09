Feature: Add Score Service
As tourist I want to rate the service that I hired for show my pleasure or dislike and more users know about himself.

	Background:
		Given This Endpoint https://localhost:5001/api/v1/services/1 is available
		And A Agency is already stored
		  |Id |Name      |Email            |PhoneNumber |Description |Location |Ruc      |Photo |Score |
		  |1  |Climbling |Climbling@go.com |987654321   |funny       |calle 2  |12345678 |none  |5     |
    	And A Customer is already store
          | Id | Name | LastName | Email            | Password    | PhoneNumber |
          | 1  | Luis | Perez    | lperez@gmail.com | mypass12345 | 999888777   |
		And A service is already stored
		  | Id | Name        | Score | Price | Location | CreationDate | Description                         | AgencyId |
		  | 1  | New Service |0		 | 420   | Ancash   | 06-11-2021   | This is a new service for my agency | 1        |
@ScoreAdding
Scenario: Add Review to Services
	When A Service review is Sent with complete information for add review
	| ServiceId | CustomerId | Date       | Comment             | Score |
	| 1         | 1          | 21/05/2022 | is the best service |5      |
	Then A Response with status 200 is received
	