using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeBlog.Application.Contracts;
using WeBlog.Contracts.Contracts;
using WeBlog.Entities.Models;
using WeBlog.Repository;
using WeBlog.Repository.Data;
using WeBlog.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
        b=>b.MigrationsAssembly("WeBlog.Web"));
});

// Identity Server
builder.Services.AddIdentity<AppUser,IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddScoped<IPostRepository,PostRepository>();
builder.Services.AddScoped<IAuth,AuthService>();
builder.Services.AddScoped<IPost,PostService>();


// Authentication


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
