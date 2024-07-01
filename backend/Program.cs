using System.Globalization;
using System.Reflection;
using backend.Accounts.Interfaces.Repositories;
using backend.Accounts.Interfaces.Shared;
using backend.Accounts.Repositories;
using backend.Accounts.Services;
using backend.BaseModule.Interfaces.Shared;
using backend.BaseModule.Services;
using backend.Configs;
using backend.Helpers;
using backend.Middlewares;
using backend.Products.Interfaces.Repositories;
using backend.Products.Interfaces.Shared;
using backend.Products.Repositories;
using backend.Products.Services;
using backend.Vouchers.Interfaces.Repositories;
using backend.Vouchers.Interfaces.Shared;
using backend.Vouchers.Repositories;
using backend.Vouchers.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "AllowAll", builder =>
    {
        builder
            .SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Register localization service for supporting multiple language
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("id-ID"),
            new CultureInfo("en-US"),
        };

        options.DefaultRequestCulture = new RequestCulture("id-ID");
        // Formatting numbers, dates, etc.
        options.SupportedCultures = supportedCultures;
        // UI strings that we have localized.
        options.SupportedUICultures = supportedCultures;

        options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
        {
            var languages = context.Request.Headers["Accept-Language"].ToString();
            var currentLanguage = languages.Split(',').FirstOrDefault();
            var defaultLanguage = string.IsNullOrEmpty(currentLanguage) ? "id-ID" : currentLanguage;

            if (defaultLanguage != "id-ID" && defaultLanguage != "en-US")
            {
                defaultLanguage = "id-ID";
            }

            return Task.FromResult(new ProviderCultureResult(defaultLanguage, defaultLanguage));
        }));
    }
);

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("TopupDatabase");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x => x.UseNetTopologySuite());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Top Up Web API",
        Description = ".NET Web API for Top Up App",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "John Doe",
            Email = string.Empty,
            Url = new Uri("https://google.com/"),
        },
        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
    c.SwaggerDoc("internal-v1", new OpenApiInfo
    {
        Version = "internal-v1",
        Title = "Telin Internal API",
        Description = ".NET Internal API for Others App",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "John Doe",
            Email = string.Empty,
            Url = new Uri("https://google.com/"),
        },
        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

    c.DocumentFilter<SwaggerDocumentFilter>();
    c.OperationFilter<HeaderForInternalApiFilter>();

    // Set the comments path for the Swagger JSON and UI.
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis") + ",abortConnect=false";
});

// Services & Repo Start
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();

builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IVoucherRepo, VoucherRepo>();
// Services & Repo End

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");

app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<CronMiddleware>();

app.Run();
