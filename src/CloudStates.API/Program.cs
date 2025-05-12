using Microsoft.EntityFrameworkCore;
using CloudStates.API.Data;
using CloudStates.API.Repositories;

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

            // repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISaveStateRepository, SaveStateRepository>();

            WebApplication app = builder.Build();
            app.MapGet("/", () => "Cloud States");
            app.Run();
        }
    }
}
