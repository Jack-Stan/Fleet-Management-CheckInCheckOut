using BL.Interfaces;
using BL.Services;
using DL.Data;
using DL.Repositories;
using Microsoft.EntityFrameworkCore;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<FleetManagementDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Test")));

        builder.Services.AddScoped<VoertuigService>();
        builder.Services.AddScoped<IVoertuigRepository, VoertuigRepository>();
        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        //app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}