using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CodeClubAssets.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.DataProtection;
var builder = WebApplication.CreateBuilder(args);

IConfiguration config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddDbContext<CodeClubAssetsContext>(options =>
    //options.UseSqlServer(config["SECRET_DB"] ?? throw new InvalidOperationException("Connection string not found"))
    options.UseSqlServer(builder.Configuration.GetConnectionString("CodeClubAssetsContext") ?? throw new InvalidOperationException("Connection string 'CodeClubAssetsContext' not found."))
    );

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<CodeClubAssetsContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Auth/Access-Denied";
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Manage");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
