using BL.Interfaces;
using BL.Services;
using DL.Data;
using DL.Repositories;
using Microsoft.EntityFrameworkCore;

internal class Program {
    private static void Main(string[] args) 
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        #region Databank config

        builder.Services.AddDbContext<FleetManagementDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Test")));

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

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
            {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCors(policyName);

        app.MapControllers();

        app.Run();
    }
}