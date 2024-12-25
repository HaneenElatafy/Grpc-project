using Microsoft.EntityFrameworkCore;
using system.context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddGrpc();

// Add DbContext to the DI container
builder.Services.AddDbContext<SongContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .LogTo(Console.WriteLine, LogLevel.Information)); // Log SQL queries

builder.Services.AddScoped<SongServiceImplementation>();

var app = builder.Build();


// Configure the HTTP request pipeline
app.MapGrpcService<SongServiceImplementation>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();