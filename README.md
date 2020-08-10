# Stock Performance

<br />

This is an application that allows comparing stocking performance of any stocking symbol with S&P 500 Index (SPY).
Just provide any stocking symbol (i.e. MSFT), specify weekly or monthly result as well as daily or intraday comparison, and get the results.

## Technologies
* .NET Core 3.1
* ASP .NET Core 3.1
* Entity Framework Core 3.1
* EFCore.BulkExtensions
* MediatR
* FluentValidation
* Angular
* Kendo Chart Component (trial)

## Getting Started

### Database Configuration

The solution is configured to use an SQL Server database by default. This ensures that an instance of Microsoft SQL Server needs to be installed.
Prior to run the application, please exceute the **Update-Database** command in Package Manager Console.

If you would like to use a different database than SQL Server, you will need to implement your own class library, implementing repository interfaces from
**StockPerformance.Persistence.Contracts** project. 

Verify that the **DefaultConnection** connection string within **appsettings.json** points to a valid database instance. 

## Overview

### StockPerformance.Domain

This layer contains all entities, enums, interfaces, types and logic specific to the domain layer.

### StockPerformance.ExternalServices.*

**StockPerformance.ExternalServices.Contracts** layer contains interfaces for working with any remote financial APIs. The current solution contains the following implementations:
**StockPerformance.ExternalServices.AplphaVantage** is an implementation for working with AlphaVantage API, and is used in the solution by default.
**StockPerformance.ExternalServices.YahooFinance** is an implementation for working with Yahoo Finance API (rakuten). This API does not support intraday results, that's why it
was switched to AlphaVantage.

### StockPerformance.Infrastructure

This layer contains configuration options, base classes and extension methods.

### StockPerformance.Persistence.*

**StockPerformance.Persistence.Contracts** layer contains interfaces for working with database repositories to store stocking history results. **StockPerformance.Persistence.SQLServer** layer contains repository
implementations for SQL Server, as well as data context and migrations.

### StockPerformance.API

This layer contains REST Web API that can be used to query history comparison.

### StockPerformance.Web

this layer is an Angular web application that can be used to query history comparison and display the result as a chart.