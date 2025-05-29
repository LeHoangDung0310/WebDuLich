using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebDuLich.Helpers;
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
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDB"))
           .EnableSensitiveDataLogging()
           .EnableDetailedErrors()
);
builder.Services.AddAuthentication();

// Configure JWT
var secretKey = builder.Configuration["AppSettings:SecretKey"];
#pragma warning disable CS8604 // Possible null reference argument.
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
#pragma warning restore CS8604 // Possible null reference argument.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(opt =>
	{
		opt.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
			ClockSkew = TimeSpan.Zero
		};
	});

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
