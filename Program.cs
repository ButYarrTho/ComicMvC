using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using ComicMvC.Data;
using ComicMvC.Models;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Register IHttpContextAccessor so we can read session in views/layout
builder.Services.AddHttpContextAccessor();

// 2. Add in-memory cache & session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//// 3. Add MVC with a global “require authenticated user” policy,
////    and register FluentValidation validators
//builder.Services.AddControllersWithViews(options =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//    options.Filters.Add(new AuthorizeFilter(policy));
//})
//.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ComicValidator>());

// 3. Add MVC and register FluentValidation validators
builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ComicValidator>());

// 4. Register your DbContext with SQL Server and retry on failure
builder.Services.AddDbContext<ComicsContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ComicMvCContext"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

// 5. Configure authentication: cookies + Google OAuth
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RegisteredOnly", policy =>
        policy.RequireAuthenticatedUser());

    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});

var app = builder.Build();

// 6. Seed the database on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// 7. Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// 8. Enable session before auth middleware
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

// 9. Map default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
