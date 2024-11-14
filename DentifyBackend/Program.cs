using DentifyBackend.Dentify.Application.Internal.CommandServices;
using DentifyBackend.Dentify.Application.Internal.QueryServices;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Dentify.Domain.Services;
using DentifyBackend.Dentify.Infrastructure.Repositories;
using DentifyBackend.Odontology.Domain.Services.Payments;
using DentifyBackend.Odontology.Domain.Services.Inventory;
using DentifyBackend.Shared.Domain.Repositories;
using DentifyBackend.Shared.Infrastructure.Interfaces.ASP.Configuration;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Lower Case URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Configure Kebab Case Route Naming Convention.
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

// Connection database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString is null) throw new Exception("Database connection string is not set.");

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(
        options =>
        {
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });
}
else if (builder.Environment.IsProduction()) {
    builder.Services.AddDbContext<AppDbContext>(
        options =>
        {
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Error)
                .EnableDetailedErrors();
        });
}


// Configure Dependency Injection

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// News Bounded Context Injection Configuration
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IDentistRepository, DentistRepository>();
builder.Services.AddScoped<IDentistCommandService, DentistCommandService>();
builder.Services.AddScoped<IDentistQueryService, DentistQueryService>();
builder.Services.AddScoped<IScheduleDentistRepository, ScheduleDentistRepository>();
builder.Services.AddScoped<IScheduleDentistCommandService, ScheduleDentistCommandService>();
builder.Services.AddScoped<IScheduleDentistQueryService, ScheduleDentistQueryService>();
builder.Services.AddScoped<IPaymentsRepository, PaymentsRepository>();
builder.Services.AddScoped<IPaymentsCommandService, PaymentsCommandService>();
builder.Services.AddScoped<IPaymentsQueryService, PaymentsQueryService>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryCommandService, InventoryCommandService>();
builder.Services.AddScoped<IIventoryQueryService, InventoryQueryService>();


var app = builder.Build();

// Create the database
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseDeveloperExceptionPage();
app.Run();



/*
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}*/