## Technologies
* .NET Core 3.1
* ASP .NET Core 3.1
* Entity Framework Core 3.1
* React
* FluentValidation
* NUnit

## Getting Started

### Database Configuration

The solution is configured to use an in-memory database by default. This ensures that all users will be able to run the solution without needing to set up a physical database.

If you would like to use SQL Server, you will need to update **API/appsettings.json** as follows:

```json
  "UseInMemoryDatabase": false,
```

Verify that the **DefaultConnection** connection string within **appsettings.json** points to a valid SQL Server instance. 

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.