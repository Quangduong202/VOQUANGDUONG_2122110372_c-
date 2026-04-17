using connetdb.Data;
using ConnetDB.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ===== DbContext - PostgreSQL =====
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure();
    });
});

// ===== CORS (FIX CHUẨN) =====
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()   // ⚡ cho phép tất cả (dev + deploy)
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// ===== Controllers + Swagger =====
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ConnetDB API", Version = "v1" });
});

var app = builder.Build();

// ===== Auto Migrate Database =====
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        Console.WriteLine("Database migrated successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error migrating database: {ex.Message}");
    }
}

// ===== MIDDLEWARE (THỨ TỰ RẤT QUAN TRỌNG) =====

// ⚡ CORS phải đặt sớm
app.UseCors("AllowAll");

// ⚡ Exception middleware nên đặt trước controller
app.UseMiddleware<ExceptionMiddleware>();

// ===== Swagger =====
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConnetDB API v1");
    c.RoutePrefix = "swagger";
});

// app.UseHttpsRedirection(); // có thể bỏ

// app.UseAuthorization();

app.MapControllers();

app.Run();