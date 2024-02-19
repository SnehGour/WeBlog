using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using WeBlog.Application.Contracts;
using WeBlog.Contracts.Contracts;
using WeBlog.Entities.Models;
using WeBlog.Repository;
using WeBlog.Repository.Data;
using WeBlog.Services;
using WeBlog.Web.Mapping;

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

// Repository
builder.Services.AddScoped<IPostRepository,PostRepository>();
builder.Services.AddScoped<ICommentRepository,CommentRepository>();

// Services
builder.Services.AddScoped<IAuth,AuthService>();
builder.Services.AddScoped<IComment,CommentService>();
builder.Services.AddScoped<IPost,PostService>();


// Authentication

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });


// Automapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

app.UseRouting(); // Identifting Action Method based route
app.UseAuthentication(); // Reading Identity Cookie
app.UseAuthorization();

app.MapControllerRoute(   // Execute the filter pipeline(action+filter)
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
