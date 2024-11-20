# QuickbooksConnector# QuickBooks Connector API

This project provides a connector for integrating with QuickBooks, including a REST API to interact with QuickBooks data. Below are the instructions to set up, build, and run the project locally.

## Prerequisites

Before running the application, make sure the following software is installed on your local machine:

1. **.NET 8.0 SDK**  
   You need to have the .NET 8.0 SDK installed. You can download it from the official [.NET download page](https://dotnet.microsoft.com/download/dotnet/8.0).

2. **QuickBooks SDK**  
   The QuickBooks SDK must be installed. You can download it from the [QuickBooks Developer website](https://developer.intuit.com/).

3. **MSBuild for .NET Framework**  
   MSBuild for .NET Framework is required to build the project. You can verify if it’s installed by running the following command in PowerShell:

   ```powershell
   msbuild /version

## Setup Instructions

1. **Clone the Repository**
    Start by cloning the repository from the main branch.

2. **Build the Project**
    Navigate to the **src\QuickbooksConnector** directory & build the project in the bin\Release directory by running the following command in PowerShell:

    ```powershell
    msbuild QuickbooksConnector.sln /p:Configuration=Release

3. **Running the Project**
    Navigate to the **src\QuickbooksConnector\QuickbooksConnector.Api\bin\Release\net8.0** directory & run the **QuickbooksConnector.Api.exe** file.

4. **Access Swagger Documentation**
    Once the application is running, you can access the Swagger documentation for the API by opening a browser and navigating to:

    https://localhost:7142/swagger/index.html