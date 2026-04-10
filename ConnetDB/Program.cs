//using Microsoft.EntityFrameworkCore;

//using connetdb.Data;
//using ConnetDB.Middleware;
//var builder = WebApplication.CreateBuilder(args);
//// Đăng ký SQL Server
//builder.Services.AddDbContext<AppDbContext>(options =>

//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//var app = builder.Build();
//// Cấu hình Middleware
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//app.UseHttpsRedirection();
//app.UseMiddleware<ExceptionMiddleware>();
//app.MapControllers();
//app.Run();

//using Microsoft.EntityFrameworkCore;
//using connetdb.Data;
//using ConnetDB.Middleware;

//var builder = WebApplication.CreateBuilder(args);

//// ===== Đăng ký DbContext với SQL Server =====
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// ===== Đăng ký Controllers và Swagger =====
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// ===== Nếu cần JWT Authentication (nếu thêm AuthController) =====
//// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
////     .AddJwtBearer(options => { ... });

//// ===== Build app =====
//var app = builder.Build();

//// ===== Middleware =====
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//// Middleware tùy chỉnh xử lý lỗi
//app.UseMiddleware<ExceptionMiddleware>();

//// Nếu dùng authentication
//// app.UseAuthentication();
//// app.UseAuthorization();

//app.MapControllers();

//app.Run();

using connetdb.Data;           // Namespace của AppDbContext
using ConnetDB.Middleware;     // Namespace của ExceptionMiddleware
using Microsoft.EntityFrameworkCore;
using Npgsql;                   // Thêm using này

var builder = WebApplication.CreateBuilder(args);

// ===== DbContext - PostgreSQL =====
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure();   // Tự động retry khi mất kết nối (rất quan trọng trên cloud)
    });
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
        context.Database.Migrate();   // Tự động tạo bảng + cập nhật schema
        Console.WriteLine("Database migrated successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error migrating database: {ex.Message}");
        // Có thể throw lại nếu muốn app crash rõ ràng
    }
}

// ===== Swagger (luôn bật để test dễ dàng) =====
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConnetDB API v1");
    c.RoutePrefix = "swagger";        // Truy cập Swagger tại /swagger
});

// app.UseHttpsRedirection();        // Comment hoặc bỏ vì Render free không cần HTTPS redirection

//app.UseMiddleware<ExceptionMiddleware>();

//app.UseAuthorization();

app.MapControllers();

// ===== Chạy ứng dụng =====
app.Run();