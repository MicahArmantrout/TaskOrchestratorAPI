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


    Uri[]? origins = null;
    try
    {
        origins = builder.Configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<Uri[]>();


        if (origins?.Any() == true)
        {
            options.AddPolicy("ProductionCors", policy =>
            {
                policy
                    .WithOrigins(origins.Select(o => o.ToString()).ToArray())
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("The Production Cors policy settings in the appsettings.json file was not set program will not continue.");
        Console.WriteLine(e);
        throw;
    }
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
