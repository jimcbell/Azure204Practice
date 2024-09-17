using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Scripts;

namespace CosmoDbPractice.Managers
{
    public class CosmoContainerManager
    {
        private Database _database;

        public CosmoContainerManager(Database database)
        {
            _database = database;
        }

        // Create Containers
        public async Task<ContainerResponse> CreateContainerAsync(string containerId, string partitionKey, int throughput)
            => await _database.CreateContainerIfNotExistsAsync(containerId, partitionKey, throughput);
        // Create with container / throughput properties
        public async Task<ContainerResponse> CreateContainerAsync(string containerId, string partitionKey) => await _database.CreateContainerIfNotExistsAsync(
            new ContainerProperties()
            {
                Id = containerId,
                PartitionKeyPaths = ["/property1", "/property2"]
            },
            ThroughputProperties.CreateManualThroughput(100));

        // Get Containers
        public Container GetContainer(string containerId) => _database.GetContainer(containerId);
        public async Task<ContainerProperties> GetContainerProperties(string containerId) => await _database.GetContainer(containerId).ReadContainerAsync();
        // Delete Containers
        public async Task DeleteContainer(string containerId) => await _database.GetContainer(containerId).DeleteContainerAsync();

        
    }
}
