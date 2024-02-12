﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CodeClubAssets.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.DataProtection;
var builder = WebApplication.CreateBuilder(args);

IConfiguration config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();
string dbConnStr = "";
try
{
    dbConnStr = config["SECRET_DB"];
    if (dbConnStr == null || dbConnStr == "") throw new InvalidOperationException("no secrets file");
}
catch
{
    dbConnStr = builder.Configuration.GetConnectionString("CodeClubAssetsContext");
}



// Add services to the container.
builder.Services.AddRazorPages();
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddDbContext<CodeClubAssetsContext>(options =>
    //options.UseSqlServer(config["SECRET_DB"] ?? throw new InvalidOperationException("Connection string not found"))
    //options.UseSqlServer(builder.Configuration.GetConnectionString("CodeClubAssetsContext") ?? throw new InvalidOperationException("Connection string 'CodeClubAssetsContext' not found."))
    options.UseSqlServer(dbConnStr ?? throw new InvalidOperationException("Connection string not found"))
    );

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<CodeClubAssetsContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Auth/Access-Denied";
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Manage");
    options.Conventions.AuthorizePage("/Loan");
    options.Conventions.AuthorizePage("/Return");
});

builder.Services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
{
    microsoftOptions.AuthorizationEndpoint = "https://login.microsoftonline.com/5eb26f0a-532d-45f6-b1b4-58c84e52a7c5/oauth2/v2.0/authorize";
    microsoftOptions.TokenEndpoint = "https://login.microsoftonline.com/5eb26f0a-532d-45f6-b1b4-58c84e52a7c5/oauth2/v2.0/token";
    microsoftOptions.ClientId = config["AUTH_MS_ID"];
    microsoftOptions.ClientSecret = config["AUTH_MS_SECRET"];
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
