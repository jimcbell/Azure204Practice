Scripts ran at the creation of the project. For reference:

// To add the package (got this from the Microsoft Learn page: https://learn.microsoft.com/en-us/dotnet/api/overview/azure/storage.blobs-readme?view=azure-dotnet)
dotnet add package Azure.Storage.Blobs

Add Configuration to not expose my Storage Account connection string :)

```dotnet add package Microsoft.Extensions.Configuration```

Add Secret Manager to the project. This is a good way of storing secrets s.t. you do not need 
an appsettings.json, app.Config, etc. and the runtime will manage user secrets during development.

```dotnet user-secrets init```

