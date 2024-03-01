# WatchWorld
A Sample N-Layered .NET Core Project Demonstrating Clean Architecture
and the Generic Repository Pattern

## Packages

### ApplicationCore
- Ardalis.Specification

### Infrastructure
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Tools
- Npgsql.EntityFrameworkCore.PostgreSQL
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Ardalis.Specification.EntityFrameworkCore

## Web
- Microsoft.EntityFrameworkCore.Design

## UnitTests
...

## Migrations
Before running the following commands, ensure that "Web" is set as the
startup project. Run the following commands in the "Infrastructure"
project.

### Infrastructure - Migrations
```
Add-Migration InitialCreate -Context WatchWorldContext -OutputDir Data/Migrations
Update-Database -Context WatchWorldContext

Add-Migration InitialIdentity -Context AppIdentityDbContext -OutputDir Identity/Migrations
Update-Database -Context AppIdentityDbContext

```