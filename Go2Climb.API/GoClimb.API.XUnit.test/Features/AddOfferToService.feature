Feature: AddOfferToService
	As Agency I want to add my services so that my clients can see them
	
	Background:
		Given The Endpoint https://localhost:5001/api/v1/services/1 is available
		And A agency is already stored
		  |Id |Name      |Email            |PhoneNumber |Description |Location |Ruc      |Photo |Score |
		  |1  |Climbling |Climbling@go.com |987654321   |funny       |calle 2  |12345678 |none  |5     |
    	And A Service is already stored
          | Id | Name        | Score | Price | Location | CreationDate | Description                         | AgencyId |
          | 1  | New Service |    0  | 420   | Ancash   | 06-11-2021   | This is a new service for my agency | 1        |

@offer-adding
Scenario: Add Offer to Service
	When A Service Request is Sent with complete information for a upgrade of price
	| Id | Name        | Price | Location | CreationDate | Description                         | AgencyId |
	| 1  | New Service | 420   | Ancash   | 06-11-2021   | This is a new service for my agency | 1        |
   Then A response with status 200 is received