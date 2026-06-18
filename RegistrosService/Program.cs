using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRegistroService, RegistroService>();

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        "chave-super-secreta-com-mais-de-32-caracteres")),

            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
});

builder.Services.AddAuthorization();

var registrosConnectionString =
    builder.Configuration.GetConnectionString("RegistrosDb")
    ?? throw new InvalidOperationException(
        "Connection string 'RegistrosDb' not found.");

builder.Services.AddDbContext<RegistrosDbContext>(options =>
    options.UseNpgsql(registrosConnectionString));

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();