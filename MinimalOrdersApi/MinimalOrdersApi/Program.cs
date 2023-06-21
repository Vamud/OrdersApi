using System;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinimalOrdersApi.Data;
using MinimalOrdersApi.Data.Entities;
using MinimalOrdersApi.Models.Dto;
using MinimalOrdersApi.Models.Requests;
using MinimalOrdersApi.Repositories;
using MinimalOrdersApi.Repositories.Interfases;

var users = new List<UserDto>
 {
    new UserDto()
    {
        Email = "tom@gmail.com",
        Password = "12345"
    },
    new UserDto()
    {
        Email = "bob@gmail.com",
        Password = "12345"
    }
 };

var configuration = GetConfiguration();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Order API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidIssuer = configuration["Isser"],
            ValidateAudience = true,
            ValidAudience = configuration["Jwt:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DLHaoS5M0fzI2OpjS5IrOKPaqe2lDVWK")),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
        opts.UseSqlServer(builder.Configuration.GetConnectionString("BikeStores")));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapPost("/login", (UserDto loginData) =>
{
    UserDto? user = users.FirstOrDefault(p => p.Email == loginData.Email && p.Password == loginData.Password);

    if (user == null)
    {
        return Results.Unauthorized();
    }

    var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };

    var jwt = new JwtSecurityToken(
            issuer: configuration["Jwt:Isser"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DLHaoS5M0fzI2OpjS5IrOKPaqe2lDVWK")),
                SecurityAlgorithms.HmacSha256));

    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

    var response = new
    {
        access_token = encodedJwt,
        username = user.Email
    };

    return Results.Json(response);
});

app.MapPost("api/orders",  async (CreateOrderRequest orderRequest, IOrderRepository repository) =>
{
    await repository.AddAsync(orderRequest);
});

app.MapGet("api/orders/{id:int}", async (int id, IOrderRepository repository) =>
{
    return await repository.GetByIdAsync(id);
}).RequireAuthorization();

app.MapGet("api/orders", async (int pageIndex, IOrderRepository repository) =>
{
    return await repository.GetByPageAsync(pageIndex);
});

app.MapPut("api/orders/{id:int}", async (int id, IOrderRepository repository) =>
{
    await repository.CancelOrderAsync(id);
    return Results.Ok();
});

app.Run();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}
