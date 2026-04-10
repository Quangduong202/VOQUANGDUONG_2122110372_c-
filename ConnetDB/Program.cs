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

using connetdb.Data;
using ConnetDB.Middleware;
using Microsoft.EntityFrameworkCore;

usingusing Microsoft.EntityFrameworkCore;
using ConnetDB.Data;           // sửa lại namespace cho đúng
using ConnetDB.Middleware;     // sửa lại namespace cho đúng

var builder = WebApplication.CreateBuilder(args);

// ===== DbContext =====
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ===== Controllers + Swagger =====
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ConnetDB API", Version = "v1" });
});

var app = builder.Build();

// ===== Swagger (luôn bật trên Render để test) =====
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConnetDB API v1");
    c.RoutePrefix = "swagger";        // truy cập tại /swagger
});

// app.UseHttpsRedirection();        // Bỏ hoặc comment khi deploy Render free

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

// ===== KHÔNG dùng app.Run() với port cứng =====
// Chỉ cần để app.Run() không tham số là đủ
app.Run();