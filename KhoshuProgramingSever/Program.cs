using KhonshuProgramingAiDatabase.Database;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

String? connectionString = builder.Configuration.GetConnectionString("localPg");

builder.Services.AddDbContext<KhonshuDbContext>(options => {
    options.UseNpgsql(connectionString);
});

if (connectionString is null)
{
    Console.WriteLine("No connection string");
    return;
}

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options => {
    options.AddPolicy("KhonshuCorsPolicy", policy => {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}
app.UseCors("KhonshuCorsPolicy");
app.UseHttpsRedirection();
app.UseStaticFiles();



app.Run();


