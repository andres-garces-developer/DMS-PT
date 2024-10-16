using Microsoft.EntityFrameworkCore;
using WebAPIDMSPruebaTecnica.Dtos.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure context to project for Scaffolding App
builder.Services.AddDbContext<PruebaTecnicaDMSSoftContext>(
    otp =>
    {
        otp.UseSqlServer(builder.Configuration.GetConnectionString("DefaulfConnectDB"));
    });

// Configure CORS in this project
var rulesCorsAPI = "RulesCorsAPISetting";
builder.Services.AddCors(otp =>
{
    otp.AddPolicy(name: rulesCorsAPI, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(rulesCorsAPI);

app.UseAuthorization();

app.MapControllers();

app.Run();
