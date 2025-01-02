using System;
using backend.Data;
using backend.interfaces;
using backend.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.extentions;

public static class AppServicesExtention
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {

        services.AddDbContext<DataBase>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("Default"));
        });

        services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost4200", policy =>
            {
                policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        services.AddScoped<ITokenService, TokenService>();
        services.AddControllers();
        services.AddCors();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["TokenKey"] ?? throw new ArgumentException("Token key not found"))),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        return services;
    }

}
