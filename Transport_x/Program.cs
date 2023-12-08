using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//thoi tiet
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

// Thêm các dịch vụ cần thiết vào container.
builder.Services.AddControllers();
builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your_issuer", // Thay thế bằng Issuer của bạn
            ValidAudience = "your_audience", // Thay thế bằng Audience của bạn
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("your_secret_key")) // Thay thế bằng secret key của bạn
        };
    });

builder.Services.AddAuthorization();

// Thêm Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Add db context
var connectionString = builder.Configuration.GetConnectionString("API");
builder.Services.AddDbContext<Transport_x.Entities.ProjectContext>(
    options => options.UseSqlServer(connectionString)
    );

var app = builder.Build();


//Cấu hình pipeline cho HTTP request.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Sử dụng Cors
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();