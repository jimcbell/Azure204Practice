﻿using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDbPractice.Managers
{
    public class CosmoDatabaseManager
    {
        public CosmosClient _client;

        #region Client Creation

        public CosmoDatabaseManager(string connectionString)
        {
            _client = new CosmosClient(connectionString);
        }
        public CosmoDatabaseManager(string endpoint, string primaryKey)
        {
            _client = new CosmosClient(endpoint, primaryKey);
        }

        #endregion Client Creation

        #region Get Database

        public Database GetDatabase(string databaseId) => _client.GetDatabase(databaseId);

        #endregion Get Database


        #region Database Creation

        public async Task<Database> CreateDatabaseIfNotExistAsync(string databaseId) => await _client.CreateDatabaseIfNotExistsAsync(databaseId);
        public async Task<Database> CreateDatabaseAsync(string databaseId) => await _client.CreateDatabaseIfNotExistsAsync(databaseId);
        
        #endregion Database Creation

        #region Database Deletion

        public async Task DeleteDatabase(string databaseId)
        {
            Database database = _client.GetDatabase(databaseId);
            await database.DeleteAsync();

        }
       
        #endregion  Database Deletion

        #region Read Database

        public async Task<DatabaseResponse> ReadDatabaseAsync(string databaseId)
        {
            Database database = _client.GetDatabase(databaseId);
            DatabaseResponse response = await database.ReadAsync();
            return response;
        }

        #endregion Read Database
    }
}
