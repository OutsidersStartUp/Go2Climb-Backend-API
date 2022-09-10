Feature: AddHiredService
    As an agency I want to have control of the status of the service offered to my clients to indicate when a service has been satisfactorily completed.

    Background: 
        Given the Endpoint https://localhost:5001/api/v1/hiredservice is available
        And A Service already exists
          | Name         | Price | Location | CreationDate| Description                         | AgencyId |
          | New Service  | 500   | Ancash   | 06-11-2021  | This is a new service for my agency | 1        |
        
        And A Customer hired that service
          | Id | Name | LastName | Email            | Password    | PhoneNumber |
          | 1  | Luis | Perez    | lperez@gmail.com | mypass12345 | 999888777   |

    @service-adding
    Scenario: Add new hired service to the agency
        When A HiredService Request is Sent
          | CustomerId | ServiceId | Amount | Price | ScheduledDate | Status  |
          | 1          | 1         | 1000   | 500   | 05-05-2022    | pending |
        Then A Response With Status 200 is received