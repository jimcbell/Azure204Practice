using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;

partial class Program
{
    static async Task SetMetadata(string connectionString, string sourceContainer, string sourceFile)
    {
        BlockBlobClient sourceClient = new(connectionString, sourceContainer, sourceFile);
        BlobProperties properties = await sourceClient.GetPropertiesAsync();

        Console.WriteLine(properties.AccessTier);

        IDictionary<string, string> metadata = new Dictionary<string, string>();
        metadata.Add("CreatedBy", "James Campbell");
        metadata["environment"] = "development";

        sourceClient.SetMetadata(metadata);
    }

}
