using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.Extensions.Configuration;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

IConfiguration configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .AddJsonFile("appsettings.json")
    .Build();
// Get my storage account connection string.
string connectionString = configuration["StorageAccountConnectionString"]
    ?? throw new Exception("Secret Management Impoperly Set Up");
// Not disposable.
BlobContainerClient client = new(connectionString, "test");
if (client.CanGenerateSasUri)
{
    foreach (BlobItem item in client.GetBlobs())
    {
        Console.WriteLine(item.Name);
        Uri sasUri = createSasUri(client, item);
        Console.WriteLine($"SAS Url: {sasUri}");
    }
}

static BlobSasBuilder createBlobSasBuilder(BlobItem item)
{
    // Create a BlobSasBuilder that will give the SAS all permissions expiring in a day.
    DateTimeOffset expiresOn = DateTimeOffset.Now.AddDays(1);
    BlobSasBuilder builder = new BlobSasBuilder(permissions: BlobSasPermissions.All, expiresOn: expiresOn)
    {
        BlobName = item.Name,
        Resource = "b"
    };
    return builder;
}

static Uri createSasUri(BlobContainerClient client, BlobItem item)
{
    // Not disposable.
    BlobClient blobClient = client.GetBlobClient(item.Name);
    BlobSasBuilder builder = createBlobSasBuilder(item);
    Uri sasUri = blobClient.GenerateSasUri(builder);
    return sasUri;
}