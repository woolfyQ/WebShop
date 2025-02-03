using Application.Services;
using Core.DTO;
using Core.Entity;
using Infrastructure.Data;
using Infrastructure.Intefaces;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopAPI;
using ShopAPI.Token;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<TokenProvider>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);  // Устанавливаем время жизни сессии
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "WebShop.Session"; 
}); 
builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped<ICartInterface<Cart, CartDTO>, CartRepository>();
builder.Services.AddScoped<IProductInterface<Product, ProductDTO>, ProductRepository>();
builder.Services.AddScoped<IUserInterface<User, UserDTO>, UserRepository>();
builder.Services.AddScoped<IOrderInterface<Order, OrderDTO>, OrderRepository>();

builder.Services.AddScoped<EmailService>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

//builder.Services.AddScoped<IAuth, AuthService>();

builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<CartService>();

builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<OrderService>();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"], // Получаем из конфигурации
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"], // Получаем из конфигурации
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])) // Секретный ключ
    };
});
builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();



var app = builder.Build();

SeedData.Initialize(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSession();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
