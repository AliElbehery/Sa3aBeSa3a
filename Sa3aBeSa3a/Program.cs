using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Sa3aBeSa3a.Application;
using Sa3aBeSa3a.Domain.Dtos;
using Sa3aBeSa3a.Infrastructure.Data;
using Sa3aBeSa3a.Infrastructure;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Sa3aBeSa3a.Application.Auth;

namespace Sa3aBeSa3a
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(
            builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Missing ConnectionStrings:DefaultConnection"));

            // ---------------------------------------------------------------- JWT auth
            var jwt = builder.Configuration.GetSection(JwtOptions.Section).Get<JwtOptions>() ?? new JwtOptions();
            if (string.IsNullOrWhiteSpace(jwt.Key) || jwt.Key.Length < 32)
                throw new InvalidOperationException(
                    "Jwt:Key is missing or shorter than 32 characters. " +
                    "Set it with: dotnet user-secrets set \"Jwt:Key\" \"<random 32+ chars>\" (or an environment variable Jwt__Key).");
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.Section));
            builder.Services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.MapInboundClaims = false;
                // Reject already-issued API tokens when an account is disabled or its role changes.
                o.Events.OnTokenValidated = async context =>
                {
                    var userIdValue = context.Principal?.FindFirst(ClaimNames.MemberId)?.Value;
                    var role = context.Principal?.FindFirst(ClaimNames.Role)?.Value;
                    if (!long.TryParse(userIdValue, out var userId) || string.IsNullOrWhiteSpace(role))
                    {
                        context.Fail("Invalid account claims.");
                        return;
                    }

                    var db = context.HttpContext.RequestServices.GetRequiredService<AppDbContext>();
                    var isValid = await db.Members.AsNoTracking().AnyAsync(
                        user => user.Id == userId && user.IsActive && user.Role.ToString() == role);
                    if (!isValid) context.Fail("Account is inactive or its role has changed.");
                };
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwt.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(30),
                    NameClaimType = ClaimNames.Name,
                    RoleClaimType = ClaimNames.Role
                };
            });

            builder.Services.AddAuthorization(o =>
            {
                o.AddPolicy("EmployeeOnly", p => p.RequireRole("Employee"));
                o.AddPolicy("PassengerOnly", p => p.RequireRole("User"));
                o.AddPolicy("AdminOnly", p => p.RequireRole("Admin"));
            });

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.Section));

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
