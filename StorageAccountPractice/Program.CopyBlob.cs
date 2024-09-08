using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;

partial class Program
{
    static async Task CopyFromBlob(string connectionString, 
        string sourceContainer, 
        string destContainer, 
        string sourceFile, 
        string destFile)
    {
        // Create the destination container if it does not exist.
        BlobContainerClient client = new(connectionString, destContainer);
        await client.CreateIfNotExistsAsync();

        BlockBlobClient sourceClient = new(connectionString, sourceContainer, sourceFile);
        BlockBlobClient destClient = new(connectionString, destContainer, destFile);
        await destClient.StartCopyFromUriAsync(sourceClient.Uri);
    }
}
