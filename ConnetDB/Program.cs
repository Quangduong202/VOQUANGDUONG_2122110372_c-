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


using Microsoft.EntityFrameworkCore;
using connetdb.Data;
using ConnetDB.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ===== Đăng ký DbContext với SQL Server =====
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ===== Đăng ký Controllers và Swagger =====
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== Build app =====
var app = builder.Build();

// ===== Middleware =====
if (app.Environment.IsDevelopment() || true) // luôn bật Swagger khi deploy
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ❌ BỎ HTTPS khi deploy free hosting (tránh lỗi SSL)
/// app.UseHttpsRedirection();

// Middleware xử lý lỗi
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();


// ===== PORT ĐỘNG (QUAN TRỌNG) =====
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");