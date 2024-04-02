using EmployeeRegisterDetails.DataContext;
using EmployeeRegisterDetails.Interface;
using EmployeeRegisterDetails.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<EmployeeRegisterContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myConnection")));
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAnyOrigin",
    builder => { builder.AllowAnyOrigin(); builder.AllowAnyMethod(); builder.AllowAnyHeader(); });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IEmployeeRegister, EmployeeRegisterRepository>();
builder.Services.AddSwaggerGen();

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
