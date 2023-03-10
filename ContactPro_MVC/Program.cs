using ContactPro_MVC.Data;
using ContactPro_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ContactPro_MVC.Services;
using ContactPro_MVC.Services.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using ContactPro_MVC.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Tells how to connect to the database

//var connectionString = builder.Configuration.GetSection("pgSettings")["pgConnection"];
var connectionString = ConnectionHelper.GetConnectionString(builder.Configuration);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString)); // configures database driver
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//custom services
builder.Services.AddScoped<IImageService, ImageService >();
builder.Services.AddScoped<IAddressBookService, AddressBookService>();
builder.Services.AddScoped<IEmailSender, EmailService>();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

var app = builder.Build();
var scope = app.Services.CreateScope();
//keep database update with the latest migrations
await DataHelper.ManageDataAsync(scope.ServiceProvider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/HandleError/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
