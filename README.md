# Samurai-API

## Overview

Personligportfloie is a web API project that provides endpoints for managing contactinformation, skills, and related entities. It is built using ASP.NET Core and Entity Framework Core.

## Getting Started

1. Clone the repository:
   ```sh
   git clone https://github.com/Sebe112/PersonligPortfolie
   ```
2. Navigate to the project directory:
   ```sh
   cd MyApi
   ```
3. Build the project:
   ```sh
   dotnet build
   ```
4. Run the project:
   ```sh
   dotnet run --project MyApi
   ```

## API Documentation

You can access the API documentation and test the endpoints using Swagger UI at the following URL:
[Swagger UI](http://localhost:5031/swagger/index.html)

## Appsettings.json

### Example

```json
{
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
      }
    },
    "AllowedHosts": "*",
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=MyApiDB;Trusted_Connection=True;Encrypt=False"
    },
    "Jwt": {
      "Key": "MySuperSecureLongKeyThatIsAtLeast32Characters!",
      "Issuer": "MyApi",
      "Audience": "MyApiUsers"
    }
}
```
