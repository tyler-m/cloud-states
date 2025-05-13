using Microsoft.EntityFrameworkCore;
using CloudStates.API.Data;
using CloudStates.API.Exceptions;
using CloudStates.API.Extensions;
using CloudStates.API.Options;
using CloudStates.API.Repositories;
using CloudStates.API.Services;

namespace CloudStates.API
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            ConfigurationManager config = builder.Configuration;

            // exception
            builder.Services.AddExceptionHandler<CloudStatesExceptionHandler>();
            builder.Services.AddProblemDetails();

            // auth
            JwtOptions? jwtOptions = config.GetSection("Jwt").Get<JwtOptions>()
                ?? throw new InvalidOperationException("Missing or invalid JWT configuration.");
            builder.Services.AddJwtAuthentication(jwtOptions);

            // postgres
            string? postgresString = config.GetConnectionString("Postgres")
                ?? throw new InvalidOperationException("Postgres connection string is missing.");
            builder.Services.AddDbContext<CloudStatesDbContext>(options => options.UseNpgsql(postgresString));

            // repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISaveStateRepository, SaveStateRepository>();

            // services
            builder.Services.AddScoped<IAuthService, AuthService>();

            // controllers
            builder.Services.AddControllers();

            WebApplication app = builder.Build();

            app.UseExceptionHandler();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();
            app.MapGet("/", () => "Cloud States");
            
            app.Run();
        }
    }
}
