using WebShop.Components;
using WebShop.Services;
using Microsoft.AspNetCore.Components.Authorization;
using ShopAPI;
using Blazored.SessionStorage;
using ShopAPI.Service;


var builder = WebApplication.CreateBuilder(args);



//builder.Services.AddScoped<IAuth, AuthService>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<ProductCartClientService>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<ProductClientService>();
builder.Services.AddScoped<OrderClientService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<IAuth, AuthService>();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);  // Устанавливаем время жизни сессии
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

// Register HttpClient before building the app
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5197") });


//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Build the app
var app = builder.Build();


//SeedData.Initialize(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts(); // Use HSTS in production
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.UseSession();
app.Run();
