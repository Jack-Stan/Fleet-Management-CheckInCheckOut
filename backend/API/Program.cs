using API.Mappers;
using BL.Interfaces;
using BL.Services;
using DL.Data;
using DL.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;

internal class Program {
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        #region Services

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        builder.Services.AddScoped<VoertuigService>();
        builder.Services.AddScoped<IVoertuigRepository, VoertuigRepository>();
        builder.Services.AddScoped<BestuurderService>();
        builder.Services.AddScoped<IBestuurderRepository, BestuurderRepository>();
        builder.Services.AddScoped<ReserveringService>();
        builder.Services.AddScoped<IReserveringRepository, ReserveringRepository>();
        builder.Services.AddScoped<StorageService>();
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<GebruikerService>();
        builder.Services.AddScoped<IGebruikerRepository, GebruikerRepository>();


        #endregion

        #region Databank config

        builder.Services.AddDbContext<FleetManagementDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Docker")));

        #endregion

        #region Automapper config

        builder.Services.AddAutoMapper(typeof(MappingProfile));

        #endregion

        #region Azure storage config

        builder.Services.AddAzureClients(options => {
            options.AddBlobServiceClient(builder.Configuration.GetConnectionString("Storage"));
        });

        #endregion

        #region CORS integratie

        var policyName = "AllowReactFrontend";

        builder.Services.AddCors(options => {
            options.AddPolicy(name: policyName,
                builder => {
                    builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
        });

        #endregion

        #region Rate Limiter

        builder.Services.AddRateLimiter(options => {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpcontext => RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: httpcontext.User.Identity?.Name ?? httpcontext.Request.Headers.Host.ToString(),
                factory: partition => new FixedWindowRateLimiterOptions {
                    AutoReplenishment = true,
                    PermitLimit = 10,
                    QueueLimit = 0,
                    Window = TimeSpan.FromSeconds(1)
                }));
            options.RejectionStatusCode = 429;
            options.OnRejected = async (context, token) => {
                context.HttpContext.Response.StatusCode = 429;
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter)) {
                    context.HttpContext.Response.Headers["Retry-After"] = retryAfter.ToString();
                    await context.HttpContext.Response.WriteAsync(
                        $"Too many requests", cancellationToken: token);
                } else {
                    await context.HttpContext.Response.WriteAsync(
                        $"Too many requests");
                }
            };
        });

        #endregion

        #region JWT Config

        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
                };
            });

        builder.Services.AddAuthorization();

        #endregion

        var app = builder.Build();

        #region Headers

        var headers = new Dictionary<string, string>()
        {
            {"X-Frame-Options", "DENY" },
            {"X-Xss-Protection", "1; mode=block"},
            {"X-Content-Type-Options", "nosniff"},
            {"Referrer-Policy", "no-referrer"},
            {"X-Permitted-Cross-Domain-Policies", "none"},
            {"Permissions-Policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()"},
        };

        if (!builder.Environment.IsDevelopment()) {
            app.UseHsts();
        }

        app.Use(async (context, next) => {
            foreach (var keyValue in headers) {
                if (!context.Response.Headers.ContainsKey(keyValue.Key)) {
                    context.Response.Headers.Append(keyValue.Key, keyValue.Value);
                }
            }

            await next(context);
        });

        #endregion

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseRateLimiter();
        app.UseCors(policyName);

        app.MapControllers();

        app.Run();
    }
}
