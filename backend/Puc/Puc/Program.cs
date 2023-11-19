using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Puc.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar o IDistributedCache
builder.Services.AddDistributedMemoryCache(); // Configuração para armazenamento em memória. Troque por outra implementação conforme necessário.

// Configuração da sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Defina o tempo limite da sessão conforme necessário
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connection));

builder.Services.Configure<CookiePolicyOptions>(options => {
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/Usuarios/AccessDenied/";
        options.LoginPath = "/Usuarios/Login/";
    });

builder.Services.AddAuthorization(); // Adicionando o serviço de autorização

builder.Services.AddControllersWithViews(); // Adicionar os controllers

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Adicionar uso da sessão antes da autenticação e autorização

app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
