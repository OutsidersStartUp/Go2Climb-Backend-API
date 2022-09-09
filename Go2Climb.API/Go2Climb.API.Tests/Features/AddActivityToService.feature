Feature: AddActivityToService
	As Agency I want to add activities to my services so that my clients can know what they will be doing in the service
@activity-adding
Scenario: Add new Activity to my service
	Given the agency wants to add on activity endpoint
	When owner add a new activity
	| Name       | Description          | ServiceID
	| Activity1 | DescriptionActivity1 | 1
	Then the activity will be added successfully