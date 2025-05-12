using Microsoft.EntityFrameworkCore;
using CloudStates.API.Data;

namespace CloudStates.API
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            ConfigurationManager config = builder.Configuration;

            // postgres
            string? postgresString = config.GetConnectionString("Postgres")
                ?? throw new InvalidOperationException("Postgres connection string is missing.");
            builder.Services.AddDbContext<CloudStatesDbContext>(options => options.UseNpgsql(postgresString));

            WebApplication app = builder.Build();
            app.MapGet("/", () => "Cloud States");
            app.Run();
        }
    }
}
