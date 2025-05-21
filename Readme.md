{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultDatabase": "Server=your_server;Database=your_db;User=placeholder;Password=placeholder;"
  }
}
##Reason for Using a Single Project Structure - I opted to keep the solution within a single project for the sake of convenience and efficiency. Working with multiple projects on my local setup can be cumbersome, especially when it comes to managing dependencies. By using a single project, I only need to install and configure external libraries once, which streamlines the setup process. It also makes development smoother, as accessing and referencing classes or methods across different parts of the application is faster and less complex.
