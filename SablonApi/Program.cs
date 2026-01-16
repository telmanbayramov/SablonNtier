using Business;
using DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDataAccessConfiguration(builder.Configuration);
builder.Services.AddBusinessConfiguration();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(opt=>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Sablon API V1");
        opt.RoutePrefix = string.Empty;
    });
    app.UseSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
