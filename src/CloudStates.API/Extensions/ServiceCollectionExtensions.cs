using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Amazon.Runtime;
using Amazon.S3;
using CloudStates.API.Options;

namespace CloudStates.API.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
        {
            SymmetricSecurityKey signingKey = new(Encoding.UTF8.GetBytes(jwtOptions.Secret));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Access", options =>
                {
                    options.MapInboundClaims = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.AccessAudience,
                        IssuerSigningKey = signingKey
                    };
                })
                .AddJwtBearer("Refresh", options =>
                {
                    options.MapInboundClaims = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.RefreshAudience,
                        IssuerSigningKey = signingKey
                    };
                });

            services.Configure<AuthenticationOptions>(options => {
                options.DefaultScheme = "Access";
                options.DefaultAuthenticateScheme = "Access";
                options.DefaultChallengeScheme = "Access";
            });

            return services.AddAuthorization();
        }

        public static IServiceCollection AddAmazonS3Client(this IServiceCollection services, S3Options s3Options)
        {
            services.AddSingleton(sp =>
            {
                return new AmazonS3Client(
                    new BasicAWSCredentials(s3Options.AccessKey, s3Options.SecretKey),
                    new AmazonS3Config
                    {
                        ServiceURL = s3Options.ServiceUrl,
                        ForcePathStyle = true
                    });
            });

            return services;
        }
    }
}
