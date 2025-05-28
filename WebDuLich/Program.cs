using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.IRepositories;
using WebDuLich.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("MyDB"));
});
builder.Services.AddAuthentication();
// Add this in your Program.cs
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // cau hinh middleware Thay cho Configure

app.UseAuthorization();

app.MapControllers();

app.Run();
