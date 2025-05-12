# cloud-states

A service and API for storing emulator [save states](https://en.wiktionary.org/wiki/savestate) remotely. Intended for personal use with my project [Atem](https://github.com/tyler-m/atem).

## Development
Docker Compose is used to construct the development environment. A local installation of the .NET SDK shouldn't be necessary for building.

### .env
In order to set up development services, Docker Compose needs to be provided information via a number of environment variables. A `.env` file in the repository's root is a simple means of doing this. Take a look at `.env.example` to see what's required.

### Windows with VS
Opening the solution file in Visual Studio should direct you to install Docker Desktop if Docker Compose isn't detected. Select the CloudStates.DockerCompose startup item in Visual Studio and start debugging.

### Linux, macOS, or Windows without VS
Install the Docker engine and Docker Compose on your system through whichever means suits you. Cloning the repository, setting relevant permissions, navigating to the project's root directory, and running `docker-compose -p cloudstates up` should be sufficient to build and run the project in development mode, making its services accessible locally. 

## Notes
### HTTP
Communication is done over HTTP. If you want the service accessible publicly, offload TLS to a reverse proxy.

### Adding migrations
Ensure dotnet-ef is installed

`dotnet tool install --global dotnet-ef`

Navigate to `src/CloudStates.API`

`dotnet ef migrations add MigrationName --output-dir Data/Migrations`

### Updating database
`dotnet ef database update --connection "ConnectionString"`