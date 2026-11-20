using System.Text;
using KhonshuProgramingAiDatabase.Database;
using KhonshuProgramingAiDatabase.Interfaces;
using KhonshuProgramingAiDatabase.Repositoryes;
using KhoshuProgramingSever.Auth;
using KhoshuProgramingSever.Endpoints;
using KhoshuProgramingSever.General;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

String? connectionString = builder.Configuration.GetConnectionString("localPg");


if (connectionString is null)
{
    Console.WriteLine("No connection string");
    return;
}

builder.Services.AddDbContext<KhonshuDbContext>(options => {
    options.UseNpgsql(connectionString);
});


#region Scoped

builder.Services.AddScoped<IPersonRepo, PersonRepository>();
builder.Services.AddScoped<IMessagesRepo, MessagesRepository>();
builder.Services.AddScoped<IChatsRepo, ChatRepository>();

  #endregion

#region Singleton

builder.Services.AddSingleton<EncryptHandler>(new EncryptHandler(builder.Configuration["Key"]!, builder.Configuration["Salt"]!));

  #endregion

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

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidIssuer = AuthHandler.validUser,
            ValidateAudience = true,
            ValidAudience = AuthHandler.validAud,
            ValidateLifetime = true,
            IssuerSigningKey = AuthHandler.GetKey(),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>
        {
            ["Bearer"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Введите JWT токен"
            }
        };

        foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations))
        {
            operation.Value.Security ??= [];
            operation.Value.Security.Add(new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = []
            });
        }

        return Task.CompletedTask;
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
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapPersonEndpoints();
app.MapAuthEndpoints();
app.MapMessagesEndpoints();
app.MapChatsEndpoints();


app.Run();


