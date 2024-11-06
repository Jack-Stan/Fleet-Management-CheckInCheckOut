using BL.Interfaces;
using BL.Services;
using DL.Data;
using DL.Repositories;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

internal class Program {
    private static void Main(string[] args) 
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        #region Databank config

        builder.Services.AddDbContext<FleetManagementDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Docker")));

        #endregion

        #region Services

        builder.Services.AddScoped<VoertuigService>();
        builder.Services.AddScoped<IVoertuigRepository, VoertuigRepository>();
        builder.Services.AddScoped<BestuurderService>();
        builder.Services.AddScoped<IBestuurderRepository, BestuurderRepository>();
        builder.Services.AddScoped<ReserveringService>();
        builder.Services.AddScoped<IReserveringRepository, ReserveringRepository>();

        #endregion

        #region CORS integratie

        var policyName = "AllowReactFrontend";

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: policyName,
                builder =>
                {
                    builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyHeader();
                });
        });

        #endregion

        #region Rate Limiter

        builder.Services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpcontext => RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: httpcontext.User.Identity?.Name ?? httpcontext.Request.Headers.Host.ToString(),
                factory: partition => new FixedWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    PermitLimit = 50,
                    QueueLimit = 0,
                    Window = TimeSpan.FromSeconds(1)
                }));
            options.RejectionStatusCode = 429;
            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = 429;
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    await context.HttpContext.Response.WriteAsync(
                        $"Too many requests", cancellationToken: token);
                }
                else
                {
                    await context.HttpContext.Response.WriteAsync(
                        $"Too many requests");
                }
            };
        });

        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
            {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseRateLimiter();

        app.UseCors(policyName);

        app.MapControllers();

        app.Run();
    }
}