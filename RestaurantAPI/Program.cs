using RestaurantAPI.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSingleton<DbService>();

// Configure CORS for the desktop app
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Set the API to run on port 49391 to match launchSettings.json
builder.WebHost.UseUrls("http://localhost:49393");
Console.WriteLine("API is starting on http://localhost:49393");

var app = builder.Build();
 
 // Automatically ensure database schema is correct on startup
 using (var scope = app.Services.CreateScope())
 {
     var dbService = scope.ServiceProvider.GetRequiredService<DbService>();
     await dbService.EnsureDatabaseSchemaAsync();
 }

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment() || true)
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // Accessible at /scalar/v1
}

app.UseCors();
app.MapControllers();

app.Run();
