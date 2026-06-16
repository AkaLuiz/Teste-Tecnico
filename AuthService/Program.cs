using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<
    IAuthenticationService,
    AuthenticationService>();

builder.Services.AddScoped<JwtService>();

var authConnectionString =
    builder.Configuration.GetConnectionString("AuthDb")
    ?? throw new InvalidOperationException(
        "Connection string 'AuthDb' not found.");

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(authConnectionString));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();