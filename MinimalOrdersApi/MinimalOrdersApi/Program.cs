using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using MinimalOrdersApi.Data;
using MinimalOrdersApi.Data.Entities;
using MinimalOrdersApi.Repositories;
using MinimalOrdersApi.Repositories.Interfases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseHttpsRedirection();

app.MapPost("api/orders", (Order order) =>
{
});

app.MapGet("api/orders/{id:int}", (int id, IOrderRepository repository) =>
{
    return repository.GetByIdAsync(id);
});

app.MapGet("api/orders", (int pageIndex, IOrderRepository repository) =>
{
    return repository.GetByPageAsync(pageIndex);
});

app.MapPut("api/orders/{id:int}", (int id) =>
{
});

app.Run();
