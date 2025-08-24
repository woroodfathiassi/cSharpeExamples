using BookCatalog.Business.Interfaces;
using BookCatalog.Business.Managers;
using BookCatalog.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Filters;
using System.Text;

//Log.Logger = new LoggerConfiguration()
//            .MinimumLevel.Information() // capture Info and above
//            .WriteTo.Console()          // also log to console
//            .WriteTo.File("Logs/log-.txt",
//                          rollingInterval: RollingInterval.Day, // new file per day
//                          //retainedFileCountLimit: 7,           // keep last 7 days
//                          shared: true)                        // multi-process safe
//            .CreateLogger();


//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Information()
//    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
//    .WriteTo.Logger(lc => lc
//        .Filter.ByIncludingOnly(Matching.FromSource("RestApiProject.Services.BookService"))
//        .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day))
//    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

builder.Host.UseSerilog();

//builder.Logging.ClearProviders(); // remove default console logging
//builder.Logging.AddSerilog();     // use only Serilog


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBookManager, BookManager>();
builder.Services.AddScoped<ICsvBookRepository, CsvBookRepository>();
builder.Services.AddScoped<IJwtManager, JwtManager>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

var secretKey = "yv78r65rvZ76t87#y$W&970kv#nj67365jBIb!bbkhLHIHDOQD:N*%z4rtv76hgfjxvy";
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();  
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();