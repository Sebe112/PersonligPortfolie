using MyApi.Data;
using MyDAL.Interfaces;
using MyDAL.Models;
using MyDAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddControllers();  

var conStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PortfolioDbContext>(obj => obj.UseSqlServer(conStr));

builder.Services.AddScoped<IContact, ContactRepository>();
builder.Services.AddScoped<IEducation, EducationRepository>();
builder.Services.AddScoped<IExperience, ExperienceRepository>();
builder.Services.AddScoped<IProject, ProjectRepository>();
builder.Services.AddScoped<ISkill, SkillRepository>();
builder.Services.AddScoped<IProjectSkill, ProjectSkillRepository>();

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();    
    if (dbContext.Database.CanConnect())
    {
        Console.WriteLine("Database connected successfully");
    }
    else
    {
        Console.WriteLine("Database connection failed");
    }
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "MyApi"));
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();