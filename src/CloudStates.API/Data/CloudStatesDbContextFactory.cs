using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using CloudStates.API.Data;

/// <summary>
/// Intended for use by dotnet-ef to construct a database context.
/// </summary>
internal class CloudStatesDbContextFactory : IDesignTimeDbContextFactory<CloudStatesDbContext>
{
    public CloudStatesDbContext CreateDbContext(string[] args)
    {
        string connectionString = args.FirstOrDefault()
            ?? throw new ArgumentException("Missing connection string argument.");

        return new CloudStatesDbContext(
            new DbContextOptionsBuilder<CloudStatesDbContext>()
                .UseNpgsql(connectionString).Options);
    }
}