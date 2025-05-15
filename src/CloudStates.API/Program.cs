using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

            // options
            builder.Services.AddOptions<JwtOptions>().Bind(config.GetSection("Jwt"))
                .ValidateDataAnnotations().ValidateOnStart();
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtOptions>>().Value);
            builder.Services.AddOptions<S3Options>().Bind(config.GetSection("S3"))
                .ValidateDataAnnotations().ValidateOnStart();
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<S3Options>>().Value);

            // auth
            JwtOptions? jwtOptions = config.GetSection("Jwt").Get<JwtOptions>()
                ?? throw new InvalidOperationException("Missing or invalid JWT configuration.");
            builder.Services.AddJwtAuthentication(jwtOptions);

            // postgres
            string? postgresString = config.GetConnectionString("Postgres")
                ?? throw new InvalidOperationException("Postgres connection string is missing.");
            builder.Services.AddDbContext<CloudStatesDbContext>(o => o.UseNpgsql(postgresString));

            // s3
            S3Options s3Options = config.GetSection("S3").Get<S3Options>()
                ?? throw new InvalidOperationException("Missing or invalid S3 configuration.");
            builder.Services.AddAmazonS3Client(s3Options);

            // repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISaveStateRepository, SaveStateRepository>();

            // services
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

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
