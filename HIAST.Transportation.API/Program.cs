using HIAST.Transportation.Application;
using HIAST.Transportation.Infrastructure;
using HIAST.Transportation.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Register services from other projects
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();