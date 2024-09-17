using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDbPractce.Managers
{
    public class CosmoServerSideProgrammingManager
    {
        private Container _container;

        public CosmoServerSideProgrammingManager(Container container)
        {
            _container = container;
        }

        public async Task CreateStoredProcedure()
        {
            string storedProcedureId = "spCreateToDoItems";
            // Stored procedures 
            StoredProcedureResponse storedProcedureResponse = await _container.Scripts.CreateStoredProcedureAsync(new StoredProcedureProperties
            {
                Id = storedProcedureId,
                // Procedure is loaded from a javascript file.
                Body = File.ReadAllText($@"..\js\{storedProcedureId}.js")
            });
        }

        public async Task CallStoredProcedure()
        {
            dynamic[] newItems = new dynamic[]
            {
                new {
                    category = "Personal",
                    name = "Groceries",
                    description = "Pick up strawberries",
                    isComplete = false
                },
                new {
                    category = "Personal",
                    name = "Doctor",
                    description = "Make appointment for check up",
                    isComplete = false
                }
            };

            var result = await _container.Scripts.ExecuteStoredProcedureAsync<string>("spCreateToDoItem", new PartitionKey("Personal"), new[] { newItems });
        }

        public async Task CreatePreTriggerAsync()
        {
            await _container.Scripts.CreateTriggerAsync(new TriggerProperties
            {
                Id = "trgPreValidateToDoItemTimestamp",
                Body = File.ReadAllText(@"..\js\trgPreValidateToDoItemTimestamp.js"),
                TriggerOperation = TriggerOperation.Create,
                TriggerType = TriggerType.Pre
            });
        }
        public async Task CallPreTriggerAsync()
        {
            dynamic newItem = new
            {
                category = "Personal",
                name = "Groceries",
                description = "Pick up strawberries",
                isComplete = false
            };

            await _container.CreateItemAsync(newItem, null, new ItemRequestOptions { PreTriggers = new List<string> { "trgPreValidateToDoItemTimestamp" } });
        }
        public async Task CreatePostTriggerAsync()
        {
            await _container.Scripts.CreateTriggerAsync(new TriggerProperties
            {
                Id = "trgPostUpdateMetadata",
                Body = File.ReadAllText(@"..\\js\\trgPostUpdateMetadata.js"),
                TriggerOperation = TriggerOperation.Create,
                TriggerType = TriggerType.Post
            });
        }
        public async Task CallPostTriggerAsync()
        {
            dynamic newItem = new {
                name = "artist_profile_1023",
                artist= "The Band",
                albums = new List<string>() {"Hellujah", "Rotators", "Spinning Top"}
            };
            await _container.CreateItemAsync(newItem, null, new ItemRequestOptions { PostTriggers = new List<string> { "trgPostUpdateMetadata" } });

        }
    }
}
