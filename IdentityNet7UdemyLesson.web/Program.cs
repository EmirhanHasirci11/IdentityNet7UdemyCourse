using IdentityNet7UdemyLesson.web.Extensions;
using IdentityNet7UdemyLesson.web.Models;
using IdentityNet7UdemyLesson.web.OptionsModel;
using IdentityNet7UdemyLesson.web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddIdentityWithExt();

builder.Services.ConfigureApplicationCookie(opts =>
{
    var cookieBuilder = new CookieBuilder();
    cookieBuilder.Name = "UdemyCookie";
    cookieBuilder.SameSite = SameSiteMode.Lax;
    opts.LoginPath = new PathString("/Home/SignIn");
    opts.LogoutPath = new PathString("/Member/LogOut");
    opts.Cookie = cookieBuilder;
    opts.ExpireTimeSpan = TimeSpan.FromDays(60);
    opts.SlidingExpiration = true;
    
});
builder.Services.Configure<SecurityStampValidatorOptions>(opts =>
{
    opts.ValidationInterval = TimeSpan.FromMinutes(30);
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.Configure<DataProtectionTokenProviderOptions>(opts =>
{
    opts.TokenLifespan = TimeSpan.FromMinutes(20);
});

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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    
);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
