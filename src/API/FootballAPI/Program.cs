using Football.Common.Application;
using Football.Common.Infrastructure;
using Football.Common.Presentation.Endpoints;
using Football.Common.Presentation.Hubs;
using Football.Modules.Leagues.Infrastructure;
using Football.Modules.Users.Infrastructure;
using FootballAPI.Extensions;
using Hangfire;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddSignalR();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
});

builder.Services.AddApplication([
    Football.Modules.Leagues.Application.AssemblyReference.Assembly,
    Football.Modules.Users.Application.AssemblyReference.Assembly
]);

var databaseConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddInfrastructure(builder.Configuration, databaseConnectionString);

builder.Services.AddLeaguesModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Football API");
        c.RoutePrefix = "";
    });
    
    app.ApplyMigrations();
}

app.MapEndpoints();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapHub<MatchHub>("/match-hub");

await app.RunAsync().ConfigureAwait(true);