using App2.BLL.Common;
using App2.BLL.Service.Abstraction;
using App2.BLL.Service.Implementation;
using App2.DAL.Common;
using App2.DAL.Database;
using App2.DAL.Repo.Abstraction;
using App2.DAL.Repo.Implemetation;
using App2.PL.Language;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        factory.Create(typeof(Resource));
    });

;

var connectionString = builder.Configuration.GetConnectionString("defaultConnection");
builder.Services.AddDbContext<App2DbContext>(options=>options.UseSqlServer(connectionString));

//Dependancy Injection
//builder.Services.AddTransient               //new instance every time
//builder.Services.AddScoped                 //1 instance per request
//builder.Services.AddSingleton             //1 instance for whole the program  (used in validation

//builder.Services.AddKeyedSinglton();
//builder.Services.AddKeyedScoped();
//builder.Services.AddKeyTransient();

//builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
//builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddBuisnessInDAL();
builder.Services.AddBuisnessInBLL();


var app = builder.Build();
var supportedCultures = new[] {
                      new CultureInfo("ar-EG"),
                      new CultureInfo("en-US"),
                };

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

app.UseAuthorization();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
