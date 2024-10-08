﻿using Az204Common.Extensions;
using Microsoft.Extensions.Configuration;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

IConfiguration configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .AddJsonFile("appsettings.json")
    .Build();
// Get name of the container
string srcContainer = configuration.GetAppSetting("SourceContainerName");
string destContainer = configuration.GetAppSetting("DestinationContainerName");
string srcFile = configuration.GetAppSetting("SourceFile");
string destFile = configuration.GetAppSetting("DestinationFile");

// Get my storage account connection string.
string connectionString = configuration["StorageAccountConnectionString"]
    ?? throw new Exception("Secret Management Improperly Set Up");

// Partial program method to list out the storage account urls.
//EnumerateSasUrls(srcContainer, connectionString);

// Partial method to copy blob contents
//await CopyFromBlob(connectionString, srcContainer, destContainer, srcFile, destFile);

// Partial method to set blob metadata
await SetMetadata(connectionString, srcContainer, srcFile);

Console.WriteLine("Done!");

