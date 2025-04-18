﻿using Microsoft.Extensions.Configuration;
using Azure.Identity;

ClientSecretCredential credential = new ClientSecretCredential(Environment.GetEnvironmentVariable("AZURE_TENANT_ID"), Environment.GetEnvironmentVariable("AZURE_CLIENT_ID"),Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET"));

var builder = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
  .AddAzureAppConfiguration(options => {
      options.Connect(Environment.GetEnvironmentVariable("AZURE_APP_CONFIGURATION_CONNECTION_STRING"));
      options.ConfigureKeyVault(kv => {
          kv.SetCredential(credential);
      });
    })
  .AddInMemoryCollection(new Dictionary<string, string?>()
    {
    ["key1"] = "value1",
    ["key2"] = "value2"
    })
  .AddEnvironmentVariables();

IConfiguration configuration = builder.Build();

Console.WriteLine(configuration["fruit"]);
Console.WriteLine(configuration["key1"]);
Console.WriteLine(configuration["AppSettings:SettingKey:a"]);
Console.WriteLine(configuration["ConnectionStrings:DefaultConnection:ConnectionString"]);
Console.WriteLine(configuration["keyvaultsecret"]);
Console.WriteLine(configuration["remove:DefaultConnection:ConnectionString"]);
Console.WriteLine(configuration["logging:Providers:0:Name"]);
