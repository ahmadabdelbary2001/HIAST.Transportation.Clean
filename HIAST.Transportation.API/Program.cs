using HIAST.Transportation.Application;
using HIAST.Transportation.Infrastructure;
using HIAST.Transportation.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Register services from other projects
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();

// Add services to the container.
// Define the CORS policy name
const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            // In a real application, you would lock this down to your actual frontend URL.
            // For development, allowing common React ports is fine.
            policy.WithOrigins("http://localhost:3000", "http://localhost:5173") 
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add this to your app configuration (before UseAuthorization and UseEndpoints)
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();