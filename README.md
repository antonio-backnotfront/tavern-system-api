# TavernSystem
To succesfully launch the application, the appsettings.json file should look like this:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultDatabase": "Server=your server;User ID=your user id;Password=your password;TrustServerCertificate=True;Database=your database"
  }
}

```

