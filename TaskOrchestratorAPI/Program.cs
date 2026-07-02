using Microsoft.AspNetCore.Authentication.JwtBearer;
using TaskOrchestratorAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskOrchestratorAPI.Data;

var builder = WebApplication.CreateBuilder(args);
var googleClientId = builder.Configuration["Authentication:Google:ClientId"];

builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://accounts.google.com";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuers =
            [
                "https://accounts.google.com",
                "accounts.google.com"
            ],
            ValidateAudience = true,
            ValidAudience = googleClientId
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentCors", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });


    Uri[]? origins;
    try
    {
        origins = builder.Configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<Uri[]>();
        
        options.AddPolicy("ProductionCors", policy =>
        {
            if (origins?.Length > 0)
            {
                policy
                    .WithOrigins(origins.Select(o => o.ToString()).ToArray())
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }
        });
    }
    catch (Exception e)
    {
        Console.WriteLine("The Production Cors policy settings in the appsettings.json file was not set program will not continue.");
        Console.WriteLine(e);
        throw;
    }
});

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevelopmentCors");
}
else
{
    app.UseHttpsRedirection();
    app.UseCors("ProductionCors");
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
