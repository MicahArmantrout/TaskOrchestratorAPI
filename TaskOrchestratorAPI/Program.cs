using TaskOrchestratorAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

builder.Services.AddSingleton<ITaskService, TaskService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("DevelopmentCors");
}
else
{
    app.UseHttpsRedirection();
    app.UseCors("ProductionCors");
}

app.MapControllers();
app.Run();
