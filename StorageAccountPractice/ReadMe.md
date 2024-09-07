** Overview
This console app will attempt to read all of your blobs from a specified container (appsettings.json)
and create Sas Urls for them, outputting to the console.
The connection string is read from your user secrets (see setup below).

Scripts ran at the creation of the project. For reference:

// To add the package (got this from the Microsoft Learn page: https://learn.microsoft.com/en-us/dotnet/api/overview/azure/storage.blobs-readme?view=azure-dotnet)
dotnet add package Azure.Storage.Blobs

Add Configuration to not expose my Storage Account connection string :)

```dotnet add package Microsoft.Extensions.Configuration```

Add Secret Manager to the project. This is a good way of storing secrets s.t. you do not need 
an appsettings.json, app.Config, etc. and the runtime will manage user secrets during development.

```dotnet user-secrets init```

Then to add user secrets right click the project and select "Manage User Secrets", add a
"ConnectionString" : "YOUR_CONNECTION_STRING"

