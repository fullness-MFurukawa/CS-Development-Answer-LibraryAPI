using LibraryApi.Applications.Authentications;
using LibraryApi.Applications.Extensions;
using LibraryApi.Infrastructure.Extensions;
using LibraryApi.Presentations.Extensions;
using LibraryApi.Presentations.Middlewares;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


// --- 設定値の読み込み ---
// 接続文字列(インフラ層へ渡す)
var connectionString = builder.Configuration.GetConnectionString("LibraryDb")
    ?? throw new InvalidOperationException("接続文字列 'LibraryDb' が設定されていません。");

// JWT 設定(アプリケーション層へ渡す)
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()
    ?? throw new InvalidOperationException("JWT 設定 'Jwt' が設定されていません。");

// --- DI 登録 ---
// Controller を使用する
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 各層の構成要素を登録する(これまで各層に作成した拡張メソッド)
// インフラストラクチャ層
builder.Services.AddInfrastructure(connectionString);
// アプリケーション層
builder.Services.AddApplication(jwtSettings);
// プレゼンテーション層
builder.Services.AddPresentation();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "📚 C# REST API開発演習 解答例",
        Version = "v1",
        Description = "図書管理システムの REST API(DDD・クリーンアーキテクチャによる解答例)",
    });

    // XML ドキュメントコメントを Swagger に取り込む
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

var app = builder.Build();

// 例外ハンドリングミドルウェアの追加
app.UseMiddleware<ExceptionHandlingMiddleware>();

// --- HTTP パイプライン ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 認証は後で追加する

app.MapControllers();

app.Run();