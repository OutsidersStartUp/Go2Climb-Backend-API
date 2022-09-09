Feature: AddServiceToAgency
	As Agency I want to add my services so that my clients can see them
	
@service-adding
Scenario: Add new service to my agency
	Given the agency wants to add on service endpoint
	When owner add a new service
	| Name        | Price | Location | CreationDate | Description                         | AgencyId
	| New Service | 420   | Ancash   | 06-11-2021   | This is a new service for my agency | 1
	Then the service will be added successfully