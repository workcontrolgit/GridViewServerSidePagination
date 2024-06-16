This sample project contains source code for Medium blog post https://medium.com/scrum-and-coke/server-side-pagination-with-asp-net-web-forms-25ed8efa25fc

# GridView Server-Side Pagination

This project demonstrates a sample ASP.NET Web Forms application featuring a GridView with server-side pagination and CRUD (Create, Read, Update, Delete) functionality. The application leverages Bootstrap 4 for a responsive and modern UI/UX.

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## Features

- Server-side pagination for efficient data handling
- CRUD operations (Create, Read, Update, Delete)
- Bootstrap 4 integration for a responsive design
- Clean and maintainable code structure

## Installation

To set up this project locally, follow these steps:

1. Clone the repository:
    ```bash
    git clone https://github.com/workcontrolgit/GridViewServerSidePagination.git
    ```

2. Open the project in Visual Studio.

3. Restore the NuGet packages:
    ```bash
    Update-Package -reinstall
    ```

4. Update the database connection string in `Web.config` to match your database setup:
    ```xml
    <connectionStrings>
        <add name="DefaultConnection" connectionString="YourConnectionString" providerName="System.Data.SqlClient" />
    </connectionStrings>
    ```

5. Build and run the project.

## Usage

- Navigate to the main page to view the GridView with the list of profiles.
- Use the pagination controls at the bottom of the GridView to navigate through the records.
- Add, edit, or delete profiles using the corresponding buttons.
- The data updates in real-time, reflecting changes in the database.

## Project Structure

```plaintext
|-- GridViewServerSidePagination
    |-- Pages
        |-- Default.aspx
        |-- Default.aspx.cs
    |-- UserControls
        |-- ProfileControl.ascx
        |-- ProfileControl.ascx.cs
    |-- Services
        |-- ProfileService.cs
    |-- Repositories
        |-- IProfileRepository.cs
        |-- ProfileRepository.cs
    |-- Models
        |-- Profile.cs
    |-- DataAccess
        |-- ApplicationDbContext.cs
    |-- App_Code
        |-- Utility.cs
    |-- Web.config

