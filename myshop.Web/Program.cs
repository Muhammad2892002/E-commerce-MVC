using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using myshop.DAL.Data;
using myshop.DAL.Interfaces;
using myshop.Domain.Models;
using NuGet.Protocol.Core.Types;
using Stripe;
using System;

using myshop.DAL.Interfaces;
using myshop.DAL.Repositories;
using myshop.BLL.Services;
using myshop.Web.IdentitySeeds;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using myshop.BLL.ApplicationServices.Interfaces;
using myshop.BLL.ApplicationServices.Services;
using myshop.BLL.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DBConnection")
    )) ;

builder.Services.AddIdentity<ApplicationUser,IdentityRole>(
    options=>options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(4)
    ).AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
});


builder.Services.AddHttpContextAccessor();
//DI here---------------------------------------------------------------
//builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<ICategoryService,CategoryServices>();
builder.Services.AddScoped<IProductService,ProductServices>();
builder.Services.AddScoped<IAccountService,AccountServices>();
builder.Services.AddScoped<ICartService,CartService>();
builder.Services.AddScoped<IUserManagmentService,UserManagmentServices>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IAccountRepo, AccounttRepo>();
builder.Services.AddScoped<IFileService,HandlingFiles>();

builder.Services.AddScoped<IUserManagment, UserManagment>();



//----------------------------------------------------------------------
builder.Services.AddAutoMapper(cfg =>
{

    cfg.AddProfile<myshop.BLL.Mapping.Mapping>();
    cfg.AddProfile<myshop.Web.PresentationMapper.PLMapper>();

});
//---------------------------------------------------------------------- Seeders
builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("CustomerAndAdmin", policy => policy.RequireRole("Customer","Admin"));
    options.AddPolicy("Customer", policy => policy.RequireRole("Customer"));

});



    builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => { 
  options.IdleTimeout = TimeSpan.FromMinutes(25);
  options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;


});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var db = services.GetRequiredService<ApplicationDbContext>();

    db.Database.Migrate();

    await IdentitySeeders.AddAllSeeds(services);
}






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

app.UseSession();

app.MapRazorPages();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

