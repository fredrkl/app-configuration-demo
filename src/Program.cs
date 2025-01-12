using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
  .AddAzureAppConfiguration(Environment.GetEnvironmentVariable("AZURE_APP_CONFIGURATION_CONNECTION_STRING"))
  .AddInMemoryCollection(new Dictionary<string, string?>()
    {
    ["key1"] = "value1",
    ["key2"] = "value2"
    })
  .AddEnvironmentVariables();

IConfiguration configuration = builder.Build();

Console.WriteLine(configuration["fruit"]);
Console.WriteLine(configuration["key1"]);
Console.WriteLine(configuration["AppSettings:SettingKey"]);
Console.WriteLine(configuration["ConnectionStrings:DefaultConnection:ConnectionString"]);
