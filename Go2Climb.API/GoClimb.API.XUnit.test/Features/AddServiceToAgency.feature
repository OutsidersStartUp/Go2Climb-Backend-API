Feature: AddServiceToAgency
  As Agency I want to add my services so that my clients can see them

  Background:
    Given the Endpoint https://localhost:5001/api/v1/services is available
    And A Agency Is Already Stored
      |Id|Name     |Email           |PhoneNumber|Description|Location|Ruc     |Photo|Score|
      |1 |Climbling|Climbling@go.com|987654321  |funny      |calle 2 |12345678|none |5    |

  @service-adding
  Scenario: Add new service to my agency
    When A Service Request is Sent
      | Name        | Price | Location | CreationDate | Description                         | AgencyId
      | New Service | 420   | Ancash   | 06-11-2021   | This is a new service for my agency | 1
    Then A Response with status 200 is Received