# cloud-states

An API for storing emulator [save states](https://en.wiktionary.org/wiki/savestate) remotely. Intended for personal use with my project [Atem](https://github.com/tyler-m/atem).

## Notes
### Adding migrations
Ensure dotnet-ef is installed

`dotnet tool install --global dotnet-ef`

Navigate to `src/CloudStates.API`

`dotnet ef migrations add MigrationName --output-dir Data/Migrations`

### Updating database
`dotnet ef database update --connection "ConnectionString"`