# UserManagement

**Test 1 - Web App**  
A simple ASP.NET MVC web application that allows for adding, editing, and managing user data, which is stored in XML format.

## Features

- Capture user data: Name, Email, Role
- Edit and delete user entries
- XML-based data storage
- Separation of concerns using Onion Architecture
- Unit Tests included

## Technologies

- ASP.NET MVC
- C#
- XML for data persistence
- MSTest or xUnit (unit testing)

## Project Structure

- `UserManagement.Core`: Contains domain models
- `UserManagement.Infrastructure`: Handles XML data operations
- `UserManagement.Application`: Contains services for business logic
- `UserManagement`: Web frontend (MVC)

## How to Run

1. Clone this repository
2. Open solution in Visual Studio
3. Run the web project (`UserManagement`)
4. Test the XML storage by adding users

## Assumptions

- Users are uniquely identified by an ID
- XML is stored locally in the application directory

## Unit Tests

Basic unit tests are available in the UserManagement.Tests folder (see UserServiceTests.cs) on the masterV2 branch.

## Time Taken

Approximately 6 hours

