using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddControllers();  

var conStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DbContext>(obj => obj.UseSqlServer(conStr));

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();    
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