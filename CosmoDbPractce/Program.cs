// See https://aka.ms/new-console-template for more information
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.Configuration;

Console.WriteLine("Hello, World!");

IConfiguration configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .AddJsonFile("appsettings.json")
    .Build();
// Get name of the container
string srcContainer = configuration.GetAppSetting("SourceContainerName");

CosmosClient client = new 