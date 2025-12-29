using AccessControlApi.Application.Extensions;
using AccessControlApi.Application.Mappings;
using AccessControlApi.Extensions;
using AccessControlApi.Infrastructure.Contexts;
using AccessControlApi.Infrastructure.Extensions;
using AccessControlApi.Middleware;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Infrastructure
builder.Services.AddInfrastructureServices();
builder.Services.AddCustomAuth(builder.Configuration);
//applicationServices
builder.Services.AddApplicationServices();

builder.Services.AddControllers();
//Add applicationDbContext service
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddCustomValidators();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//swagger extension
builder.Services.AddCustomSwagger();




//automapper
builder.Services.AddAutoMapper(typeof(UserProfile));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
