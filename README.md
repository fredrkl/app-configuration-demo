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

## Links

- [Azure App Configuration](https://docs.microsoft.com/en-us/azure/azure-app-configuration/)
