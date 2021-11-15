using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Furlough.DAL.DatabaseContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("furloughJon"));
    });
builder.Services.AddMvc().AddRazorRuntimeCompilation();

//Localization and globalization
builder.Services.AddRequestLocalization(options =>
{
    var supportedCultures = new[] {
        new CultureInfo("en-US"),
        new CultureInfo("sq-AL"),
    };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddLocalization(
    options =>
    {
        options.ResourcesPath = "Resources";
    });

//Authentication and Authorization
//builder.Services.AddAuthentication(options =>
//{
//    options.AddScheme<>("BasicAuthenticator");
//});

var app = builder.Build();

app.UseRequestLocalization();
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "MyArea",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
