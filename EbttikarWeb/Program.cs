using EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EmployeeManagement.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
    {
        options.Conventions.AuthorizeFolder("/").AllowAnonymousToPage("/Index")
        . AllowAnonymousToPage("/Identity/Account/Login")
        .AllowAnonymousToPage(("/Identity/Account/Register"));
    });
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));


// Microsoft.AspNetCore.Identity configuration
{
    IdentityBuilder identityBuilder = builder.Services.AddDefaultIdentity<Employee>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDBContext>();

    identityBuilder.Services.AddAuthentication()
     .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
    

    builder.Services.Configure<IdentityOptions>(options =>
    {
        // SignIn settings.
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.SignIn.RequireConfirmedAccount = false;

        // Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;
    });
}

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RequireAdministratorRole",
//         policy => policy.RequireRole("Administrator"));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
