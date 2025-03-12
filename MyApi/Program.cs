using MyApi.Data;
using MyDAL.Interfaces;
using MyDAL.Models;
using MyDAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var conStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PortfolioDbContext>(options => options.UseSqlServer(conStr));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<PortfolioDbContext>()
    .AddDefaultTokenProviders();

var jwtKey = builder.Configuration["Jwt:Key"] ?? "SuperSecretKey123456789!";
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

builder.Services.AddScoped<IContact, ContactRepository>();
builder.Services.AddScoped<IEducation, EducationRepository>();
builder.Services.AddScoped<IExperience, ExperienceRepository>();
builder.Services.AddScoped<IProject, ProjectRepository>();
builder.Services.AddScoped<ISkill, SkillRepository>();
builder.Services.AddScoped<IProjectSkill, ProjectSkillRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<PortfolioDbContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    dbContext.Database.Migrate();

    await SeedAdminUser(userManager, roleManager);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "MyApi"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

static async Task SeedAdminUser(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
{
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    var adminUser = await userManager.FindByEmailAsync("admin@example.com");
    if (adminUser == null)
    {
        var newAdmin = new User
        {
            UserName = "admin",
            Email = "admin@example.com",
            Role = "Admin",
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(newAdmin, "Admin@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, "Admin");
            Console.WriteLine("Admin user created successfully!");
        }
    }
    else
    {
        Console.WriteLine("Admin user already exists.");
    }
}