using Business;
using DataAccess;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using DotNetEnv;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDataAccessConfiguration(builder.Configuration);
builder.Services.AddBusinessConfiguration(builder.Configuration);

var test = Path.Combine(Directory.GetCurrentDirectory(), "..", ".env");
DotNetEnv.Env.Load(test);

builder.Configuration
    .AddEnvironmentVariables();

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(opt=>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Sablon API V1");
        opt.RoutePrefix = string.Empty;
    });
    app.UseSwagger();
}
//         );

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
