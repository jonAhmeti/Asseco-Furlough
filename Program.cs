using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Furlough.SecurityHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Furlough.DAL.FurloughContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("furloughJon"));
    });
builder.Services.AddMvc().AddRazorRuntimeCompilation()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

//Localization and globalization
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
     {
        new CultureInfo("sq"),
        new CultureInfo("en"),
        new CultureInfo("mk")
    };
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddLocalization(
    options =>
    {
        options.ResourcesPath = "Resources";
    });

//Authentication and Authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/";
    options.LogoutPath = "/Logout";
    options.ReturnUrlParameter = "";
    options.Cookie.Name = "DaddyCookie";
});
//AntiFrogeryToken Header
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-AFTAsseco";
});

//Mapper services
builder.Services.AddScoped<Furlough.Models.Mapper.DalMapper>();
builder.Services.AddScoped<Furlough.Models.Mapper.ViewModelMapper>();
//DAL services
builder.Services.AddScoped<Furlough.DAL.User>();
builder.Services.AddScoped<Furlough.DAL.Employee>();
builder.Services.AddScoped<Furlough.DAL.Department>();
builder.Services.AddScoped<Furlough.DAL.DepartmentPositions>();
builder.Services.AddScoped<Furlough.DAL.Role>();
builder.Services.AddScoped<Furlough.DAL.Position>();
builder.Services.AddScoped<Furlough.DAL.PositionHistory>();
builder.Services.AddScoped<Furlough.DAL.Request>();
builder.Services.AddScoped<Furlough.DAL.RequestType>();
builder.Services.AddScoped<Furlough.DAL.RequestStatus>();
builder.Services.AddScoped<Furlough.DAL.RequestHistory>();
builder.Services.AddScoped<Furlough.DAL.AvailableDays>();

var app = builder.Build();
app.UseRequestLocalization(options =>
{
    options.RequestCultureProviders.Add(new CookieRequestCultureProvider());
});

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
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"); //.RequireAuthorization();

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
