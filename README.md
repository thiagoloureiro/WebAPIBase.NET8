# WebAPIBase.NET8
.NET 8 API + EFCore + Sentry + Azure AD Auth + TestContainers

## Local Environment Configuration

1. appsettings.Local.json file should be created and used for configuration on local development (file is gitignored and should remain that way). <br>
  Sample configuration:

    ```json
    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Information"
        }
      },
      "ConnectionStrings": {
        "Database": "Server=.;Database=Web_Api_Base;Integrated Security=True;Encrypt=False",
        "Test-Db": "Server=.;Integrated Security=True;Encrypt=False" // remove this if you want to run integration tests in docker
      }
    }
    ```
    
2. Db initialization
    - SQL Server is used as the db engine
    - EFCore migrations can be found in Infrastructure/Database/Migrations. To apply them follow one of the methods described here https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying

3. Sentry
    - Provide `Sentry:Dsn` variable in your configuration.

4. Azure AD
    - Provide variables: `AzureAd:Domain`, `AzureAd:TenantId` and `AzureAd:ClientId`, in your configuration.

5. Integration Tests <br>
   Tests which are using Db can be run locally either in docker container or on local db engine. <br>
   - In order to run tests in docker don't add ConnectionStrings:Test-Db in your env variables/configuration.
   - If you want to run tests on local db engine then provide:
      - `ConnectionStrings:Test-Db`(sample value: `"Server=.;Integrated Security=True;Encrypt=False"`) variable in your configuration (appsettings.Local.json / env variables).
