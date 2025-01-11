using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
  .AddAzureAppConfiguration(Environment.GetEnvironmentVariable("AZURE_APP_CONFIGURATION_CONNECTION_STRING"))
  .AddInMemoryCollection(new Dictionary<string, string?>()
    {
    ["key1"] = "value1",
    ["key2"] = "value2"
    });

IConfiguration configuration = builder.Build();

Console.WriteLine(configuration["fruit"]);
