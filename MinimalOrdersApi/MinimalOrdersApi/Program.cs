using Microsoft.EntityFrameworkCore;
using MinimalOrdersApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
        opts.UseSqlServer(builder.Configuration.GetConnectionString("BikeStores")));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("api/orders", (ApplicationDbContext dbContext) =>
{
    return dbContext.Products.First();
});

app.Run();
