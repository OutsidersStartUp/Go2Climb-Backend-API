Feature: AddAgencyReview
	As a tourist I want to rate the service I hired to show my opinion so that more users know about it.

	Background:
		Given the Endpoint https://localhost:5001/api/v1/agencyreviews is available
		And A Agency already exists
		  | Id | Name      | Email            | Password | PhoneNumber | Description | Location | Ruc      | Photo | Score |
		  | 1  | Climbling | Climbling@go.com | 123      | 987654321   | funny       | calle 2  | 12345678 | none  | 5     |

		And A Customer already hire a service
		  | Id | Name | LastName | Email            | Password    | PhoneNumber |
		  | 1  | Luis | Perez    | lperez@gmail.com | mypass12345 | 999888777   |

	@service-adding
	Scenario: Add new review to the agency
		When A AgencyReview Request is Sent
		  | AgencyId | CustomerId | Date       | Comment                  | ProfessionalismScore | SecurityScore | QualityScore | CostScore |
		  | 1        | 1          | 25-04-2022 | The experience was great | 5                    | 5             | 5            | 4         |
		Then A Response With status 200 is received