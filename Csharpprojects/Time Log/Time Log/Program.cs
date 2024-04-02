using Microsoft.EntityFrameworkCore;
using Time_Log.Data_Context;
using Time_Log.Interface;
using Time_Log.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TimeLogContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myConnection")));
builder.Services.AddTransient<TimeLogInterface, TimeLogRepo>();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAnyOrigin", builder => {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
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
app.UseCors(builder => { builder.AllowAnyOrigin(); builder.AllowAnyMethod(); builder.AllowAnyHeader(); });
app.UseAuthorization();

app.MapControllers();

app.Run();
