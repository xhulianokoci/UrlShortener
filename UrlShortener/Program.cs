using Application.Configuration;
using Infrastructure;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ShortUrlSettings>(
    builder.Configuration.GetSection("ShortUrlSettings")
);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin() 
               .AllowAnyMethod()
               .AllowAnyHeader();
    });

    options.AddPolicy("AllowSwagger", builder =>
    {
        builder.WithOrigins("https://localhost:7214") // Allow Swagger origin
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSwagger");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
