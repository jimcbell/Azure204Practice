using Az204Common.Extensions;
using CosmoDbPractice.Managers;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

IConfiguration configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .AddJsonFile("appsettings.json")
    .Build();

// Get name of the container
string connectionString = configuration.GetAppSetting("CosmoDbConnection");
string primaryKey = configuration.GetAppSetting("CosmoPrimaryKey");

CosmoManager manager = new CosmoManager(connectionString);
await manager.CreateDatabaseIfNotExistAsync("testdatabase1");


Console.WriteLine("Done");