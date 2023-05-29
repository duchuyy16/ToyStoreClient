using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ToyStoreClient.ApiServices;
using ToyStoreClient.Services;
using ToyStoreClient.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();


//Đếm sp trong giỏ hàng
builder.Services.AddScoped<ICart, Cart>();
builder.Services.AddScoped<Client>(s => new Client("https://localhost:44350"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

ToyStoreClient.Helpers.AppContext.Configure((IHttpContextAccessor)app.Services.GetService(typeof(IHttpContextAccessor))!);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.UseSession();

app.MapRazorPages();

app.MapAreaControllerRoute(
    name: "MyAreaAdmin",
    areaName: "Admin",
    pattern: "Admin/{controller=AuthAdmin}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
