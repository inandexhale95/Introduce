using Introduce.Services.Contexts;
using Introduce.Services.Interfaces;
using Introduce.Services.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<SeedData>();
builder.Services.AddTransient<IUser, UserService>();
builder.Services.AddTransient<IFreeBoard, FreeBoardService>();
builder.Services.AddTransient<IForum, ForumService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/User/Index";
        options.LoginPath = "/User/Login";
    });
builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// SeedData
using (var scope = app.Services.CreateScope())
{
    SeedData seedData = scope.ServiceProvider.GetService<SeedData>();
    seedData.PlantData();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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