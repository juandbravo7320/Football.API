using Football.Common.Application;
using Football.Common.Infrastructure;
using Football.Common.Presentation.Endpoints;
using Football.Modules.Leagues.Infrastructure;
using FootballAPI.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
});

builder.Services.AddApplication([
    Football.Modules.Leagues.Application.AssemblyReference.Assembly
]);

var databaseConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddInfrastructure(databaseConnectionString);

builder.Configuration.AddModuleConfiguration(["leagues"]);

builder.Services.AddLeaguesModule(builder.Configuration);

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

await app.RunAsync().ConfigureAwait(true);