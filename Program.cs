using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CollegeManagement.Models;
using CollegeManagement.Services.Email;
using CollegeManagement.Services.Sms;
using CollegeManagement.Services.Student;
using CollegeManagement.Models.Emails;
using CollegeManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// THIS IS THE ONLY LINE YOU NEED FOR MVC
builder.Services.AddControllersWithViews();

// Database
builder.Services.AddDbContextPool<CollegeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<IStudentRepository, MockStudentRepository>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<SmsSettings>(builder.Configuration.GetSection("SmsSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISmsService, SmsService>();

// Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<CollegeContext>()
    .AddDefaultTokenProviders();

// Session (MUST HAVE THIS)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();           // ← BEFORE Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();