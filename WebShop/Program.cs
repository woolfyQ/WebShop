using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Service;
using WebShop.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAuth, AuthService>();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register HttpClient before building the app
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5197") });

// Register the DbContext before building the app
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Build the app
var app = builder.Build();

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

app.Run();
