using System.Reflection;
using Fleet.Application.Common.Interfaces;
using Fleet.Infrastructure;
using Fleet.Infrastructure.Persistence;
using Fleet.API.Hubs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Fleet.API.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.Load("Fleet.Application")));

builder.Services.AddSignalR();

builder.Services.AddHostedService<SignalRSimulator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true);
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FleetDbContext>();
    await db.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("default");

app.UseAuthorization();

app.MapControllers();

app.MapHub<TrackingHub>("/hubs/tracking");
app.MapHub<TripsHub>("/hubs/trips");
app.MapHub<DriversHub>("/hubs/drivers");
app.MapHub<AlertsHub>("/hubs/alerts");

app.Run();
