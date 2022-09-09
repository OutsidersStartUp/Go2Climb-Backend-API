Feature: AddServiceReviewToService
	As User I want to review the service i went to

@servicereview-adding
Scenario: Add new ServiceReview to a Service
	Given the user wants to review a service
	When user add a new review
	| Date       | Comment      | Score | ServiceId
	| 06-11-2021 | Nice service | 5     |  1
	Then the review will be added successfully