using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;

partial class Program
{
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

    static void EnumerateSasUrls(string containerName, string connectionString)
    {
        // Not disposable.
        BlobContainerClient client = new(connectionString, containerName);
        if (client.CanGenerateSasUri)
        {
            foreach (BlobItem item in client.GetBlobs())
            {
                Console.WriteLine(item.Name);
                Uri sasUri = createSasUri(client, item);
                Console.WriteLine($"SAS Url: {sasUri}");
            }
        }
    }
}
