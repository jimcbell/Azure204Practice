using CosmoDbPractce.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDbPractce
{
    public class CosmoItemManager
    {
        private Container _container;

        public CosmoItemManager(Container container)
        {
            _container = container;
        }

        // Create items - must have an id and partition key property
        public async Task<Airplane> CreateAirplaneAsync(Airplane plane) => await _container.CreateItemAsync(plane, new PartitionKey(plane.Manufacturer));

        public async Task<Airplane> ReadAirPlaneAsync(string airplaneId, string manufacturer) => await _container.ReadItemAsync<Airplane>(airplaneId, new PartitionKey(manufacturer));

        public async Task<List<Airplane>> QueryAirplanesAsync(string airplaneName, string manufacturer)
        {
            List<Airplane> results = new();
            QueryDefinition query = new QueryDefinition(
                "select * from airplanes a where a.Name = @NameInput")
                .WithParameter("@NameInput", airplaneName);

            FeedIterator<Airplane> resultSet = _container.GetItemQueryIterator<Airplane>(
                query,
                requestOptions: new QueryRequestOptions()
                {
                    PartitionKey = new PartitionKey(manufacturer)
                    //MaxItemCount = 1
                });
            return (await resultSet.ReadNextAsync()).ToList();

        }
    }
}
