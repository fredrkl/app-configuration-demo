# App Configuration Demo

Azure App Configuration Demo. The scenarios where Azure App Configuration can be used are:

1. Centralized Configuration
2. Feature Management
3. Dynamic Configuration
4. Labeling and Filtering
5. Key Vault Integration
6. Change Monitoring
7. Hierarchical Configuration
8. Multiple Environments

## The main problem it solves

- Centralized Configuration: Azure App Configuration provides a service to centrally manage application settings and feature flags. Modern applications, especially those built for the cloud, generally have many components that are distributed in nature. Spreading configuration settings across these components can lead to hard-to-troubleshoot errors during an application deployment. Use App Configuration to store all the settings for your application and secure their accesses in one place.

## Configuration Builder

The `ConfigurationBuilder` class in the `Microsoft.Extensions.Configuration` namespace provides a way to build a configuration in a more structured way. The `ConfigurationBuilder` class is used to build the configuration for an application. The configuration can be built from various sources like JSON, XML, INI, and environment variables.

The usual ones are:

- `AddJsonFile()`: Adds a JSON file as a configuration source.
- `AddXmlFile()`: Adds an XML file as a configuration source.
- `AddIniFile()`: Adds an INI file as a configuration source.
- `AddEnvironmentVariables()`: Adds the environment variables as a configuration source.
- `AddCommandLine()`: Adds the command line arguments as a configuration source.

In this example we will use the `AddAzureAppConfiguration()` method to add the Azure App Configuration as a configuration source. Also with a KeyVault for the secrets.

## Setup

You will need the connection string in the following environment variable: `AZURE_APP_CONFIGURATION_CONNECTION_STRING`. Get it from the Azure portal.

```
> export AZURE_APP_CONFIGURATION_CONNECTION_STRING="Endpoint=https://d...lfkjsdfk"
```

Additionaly, you will need to set the following environment variable for the enterprise application that access the KeyVaylt.

- AZURE_TENANT_ID
- AZURE_CLIENT_ID
- AZURE_CLIENT_SECRET

The _service principle_ needs the following KeyVault role:

- Key Vault Secrets User

It is tempting to think that the _managed identity_ that you assign as the _Azure App Configuration_ identity is automatically used for KeyVault access. However, you will need to spesify an _Enterprise Application_ that has KeyVault access or use the `DefaultAzureCredential`.

## Experiments

I created a simple console application that reads the configuration from multiple sources in this order:

- JSON file
- Azure App Configuration
- In-memory
- Environment variables

### Add complex structure

The `logging` part of this configuration is complicated with array and nested configuration:

```
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    },
    "Providers": [
      {
        "Name": "Serilog",
        "Using": [ "Serilog.Sinks.Console", "Serilog.Sinks.File" ],
        "MinimumLevel": "Information",
        "WriteTo": [
          {
            "Name": "Console"
          },
          {
            "Name": "File",
            "Args": {
              "path": "Logs/log-.txt",
              "rollingInterval": "Day"
            }
          }
        ],
        "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ],
        "Properties": {
          "Application": "SampleApp"
        }
      },
      {
        "Name": "Console",
        "LogLevel": {
          "Default": "Debug",
          "System": "Information",
          "Microsoft": "Information"
        }
      },
    ]
  }
```

This is a made up configuration that is only meant to show how to add a complex structure to the configuration.

The _Azure APP Configuration_ does also have the entry: `Logging:Providers:0:Name` with a value from a keyvault in effect changing the _name_. This showcases adding keys to spesific configuration parts.

## Lessons learned

- If you use configuration to get a complex object and try to write it out, then you will get nothing, e.g., `ConnectionStrings:DefaultConnection`. You will need to get a simple value, e.g., `ConnectionStrings:DefaultConnection:ConnectionString`.
- The key in the Azure App Configuration can be a path, e.g., `Logging:Providers:0:Name` and is what is being used in the `ConfigurationBuilder` to get the value
- You will need to either give the _Azure App Configuration_ a dedicated managed identity or use a system assigned. Then give that identity access to the _KeyVault_. One example is to give it the `Key Vault Secrets User` to be able to read secrets.

## Links

- [Azure App Configuration](https://docs.microsoft.com/en-us/azure/azure-app-configuration/)
