using Application.Services;
using Core.DTO;
using Core.Entity;
using Infrastructure.Data;
using Infrastructure.Intefaces;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopAPI;
using ShopAPI.Service;
using ShopAPI.Token;
using System.Text;


var builder = WebApplication.CreateBuilder(args);



//builder.Services.AddDistributedMemoryCache();  // Добавляем кэш для сессии


//builder.Services.AddSession(options =>
//{
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;  // Обязательно для работы с сессией
//});


builder.Services.AddScoped<IAuth, AuthService>();

builder.Services.AddScoped<TokenProvider>();

//builder.Services.AddHttpClient<AuthService>();

// Регистрация сервисов для работы с HttpContext

//builder.Services.AddHttpContextAccessor();  // Для доступа к HttpContext

builder.Services.AddScoped<IProductCartInteface<ProductCart, ProductCartDTO>, ProductCartRepository>();
builder.Services.AddScoped<IUserInterface<User, UserDTO>, UserRepository>();
builder.Services.AddScoped<ICartInterface<Cart, CartDTO>, CartRepository>();
builder.Services.AddScoped<IProductInterface<Product, ProductDTO>, ProductRepository>();
builder.Services.AddScoped<IOrderInterface<Order, OrderDTO>, OrderRepository>();


//builder.Services.AddScoped<ICartSessionService, CartSessionService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductCartService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();


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





builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();




builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options => 
//    {
//        options.Cookie.Name = "auth_token";
//        options.LoginPath = "/Login";
//        options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
//        options.AccessDeniedPath = "/acces-denied";

//    });

//builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

SeedData.Initialize(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseSession();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
