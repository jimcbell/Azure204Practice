using Az204Common.Extensions;
using CosmoDbPractice.Managers;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

IConfiguration configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

// Get name of the container
string connectionString = configuration.GetAppSetting("CosmoDbConnection");
string primaryKey = configuration.GetAppSetting("CosmoPrimaryKey");

CosmoDatabaseManager manager = new CosmoDatabaseManager(connectionString);
Database database = await manager.CreateDatabaseIfNotExistAsync("db1");

CosmoContainerManager cosmoContainerManager = new(database);
await cosmoContainerManager.CreateContainerAsync("plane", "/manufacturer", 400);



Console.WriteLine("Done");
Console.ReadKey();  