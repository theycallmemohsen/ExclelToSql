using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ExclelToSql.Data;

var builder = WebApplication.CreateBuilder(args);

// ==========================
// Services (everything must be registered BEFORE builder.Build())
// ==========================
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ExclelToSqlContext>(options =>          // CHANGED: context name (see note 1)
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddMemoryCache();                                     // CHANGED: moved above Build()
builder.Services.AddResponseCompression(o => o.EnableForHttps = true); // CHANGED: moved above Build()

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:4173",
                "http://localhost:3000",
                "http://localhost:5173",
                "http://localhost:5174",
                "https://extending-nucleus-flavorful.ngrok-free.dev"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.Name = ".AspNetCore.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

var app = builder.Build();

// ==========================
// Pipeline (order matters)
// ==========================
app.UseResponseCompression();                                          // CHANGED: first, so responses are compressed

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseRouting();                                                      // CHANGED: moved before CORS/session/auth

app.UseCors("ReactPolicy");
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllers();                                                  // attribute-routed APIs
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
