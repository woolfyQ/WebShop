using Application.Services;
using Core;
using Core.Entity;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Service;
using ShopAPI.Token;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAuth, AuthService>();

builder.Services.AddScoped<TokenProvider>();
builder.Services.AddHttpClient<ShopAPI.Service.AuthService>();
builder.Services.AddScoped<Application.Services.UserService>();
builder.Services.AddScoped< Application.Services.StorageService>();
builder.Services.AddScoped<Application.Services.ProductService>();
builder.Services.AddScoped<Application.Services.OrderService>();
builder.Services.AddScoped<Application.Services.CartService>();

builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IRepository<ProductStorage>, StorageRepository>();
builder.Services.AddScoped<IRepository<Cart>, CartRepository>();

builder.Services.AddControllers();



builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
