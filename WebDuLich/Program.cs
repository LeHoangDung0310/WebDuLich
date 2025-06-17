using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebDuLich.Helpers;
using Microsoft.EntityFrameworkCore;
using WebDuLich.Data;
using WebDuLich.Interfaces.IRepositories;
using WebDuLich.Interfaces.Repositories;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
	options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "WebDuLich", Version = "v1" });

	// Thêm cấu hình bảo mật cho Swagger
	c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
		Name = "Authorization",
		In = Microsoft.OpenApi.Models.ParameterLocation.Header,
		Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});

	c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
	{
		{
			new Microsoft.OpenApi.Models.OpenApiSecurityScheme
			{
				Reference = new Microsoft.OpenApi.Models.OpenApiReference
				{
					Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});

builder.Services.AddDbContext<MyDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("MyDB"))
);
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowReactApp",
		builder => builder
			.WithOrigins("http://localhost:5173") // React app's URL
			.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowCredentials());
});

// Disable HTTPS requirement in development
if (builder.Environment.IsDevelopment())
{
	builder.Services.AddHttpsRedirection(options =>
	{
		options.HttpsPort = 7003;
	});
}

// Configure JWT
var secretKey = builder.Configuration["AppSettings:SecretKey"]; // Fixed case to match LoginRepository
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey ?? throw new InvalidOperationException("JWT secret key is not configured"));

builder.Services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
	opt.SaveToken = true;
	opt.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
		ClockSkew = TimeSpan.Zero,
		RoleClaimType = ClaimTypes.Role, // Ensure this matches the claim type used in token generation
		NameClaimType = ClaimTypes.Name  // Ensure this matches the claim type used in token generation
	}; opt.Events = new JwtBearerEvents
	{
		OnAuthenticationFailed = async context =>
		{
			System.Diagnostics.Debug.WriteLine($"[JWT Debug] Authentication failed: {context.Exception.Message}");

			context.NoResult();
			context.Response.StatusCode = 401;
			context.Response.ContentType = "application/json";

			var message = context.Exception.GetType() == typeof(SecurityTokenExpiredException)
				? "Token has expired"
				: "Invalid token";

			await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
			{
				status = 401,
				message = message,
				detailed = context.Exception.Message
			}));
		},
		OnTokenValidated = context =>
		{
			var claims = context.Principal?.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
			var roleClaim = context.Principal?.FindFirst(ClaimTypes.Role);

			System.Diagnostics.Debug.WriteLine($"[JWT Debug] Token validated successfully:");
			System.Diagnostics.Debug.WriteLine($"[JWT Debug] Role claim: {roleClaim?.Type}={roleClaim?.Value}");
			System.Diagnostics.Debug.WriteLine($"[JWT Debug] All claims: {string.Join(", ", claims ?? new List<string>())}");
			return Task.CompletedTask;
		},
		OnChallenge = context =>
		{
			System.Diagnostics.Debug.WriteLine($"[JWT Debug] Challenge issued. Authentication scheme: {context.AuthenticateFailure?.Message ?? "No failure details"}");
			context.HandleResponse(); // Prevent the default challenge response
			context.Response.StatusCode = 401;
			context.Response.ContentType = "application/json";
			var result = System.Text.Json.JsonSerializer.Serialize(new { message = "Unauthorized. Authentication required." });
			return context.Response.WriteAsync(result);
		},
		OnForbidden = context =>
		{
			System.Diagnostics.Debug.WriteLine("[JWT Debug] Access forbidden");
			context.Response.StatusCode = 403;
			context.Response.ContentType = "application/json";
			var result = System.Text.Json.JsonSerializer.Serialize(new { message = "Forbidden. Insufficient permissions." });
			return context.Response.WriteAsync(result);
		}
	};
});

builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ITaiKhoanRepository, TaiKhoanRepository>();
builder.Services.AddScoped<IMienRepository, MienRepository>();
builder.Services.AddScoped<ITinhRepository, TinhRepository>();
builder.Services.AddScoped<INguoiDungRepository, NguoiDungRepository>();
builder.Services.AddScoped<IQuyenTKRepository, QuyenTKRepository>();
builder.Services.AddScoped<INhaHangRepository, NhaHangRepository>();
builder.Services.AddScoped<IKhachSanRepository, KhachSanRepository>();
builder.Services.AddScoped<IAnhKSRepository, AnhKSRepository>(); // Added line

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// Remove HTTPS redirection in development
if (!app.Environment.IsDevelopment())
{
	app.UseHttpsRedirection();
}

app.UseRouting();

// CORS must be between UseRouting and UseAuthentication
app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
