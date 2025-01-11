using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
.AddInMemoryCollection(new Dictionary<string, string?>()
    {
    ["key1"] = "value1",
    ["key2"] = "value2"
    });

IConfiguration configuration = builder.Build();

Console.WriteLine(configuration["key1"]);
