using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SMS_LMS.Bussiness.AutoMapper;
using SMS_LMS.Bussiness.Services.PleasePreviewModelServices;
using SMS_LMS.Bussiness.Services.SMSDataModelServices;
using SMS_LMS.Bussiness.Services.SMSModelServices;
using SMS_LMS.Bussiness.Services.UserServices;
using SMS_LMS.Data;
using SMS_LMS.Data.UnitOfWorkDef;
using SMS_LMS.Extensions;
using SMS_LMS.Localization;
using SMS_LMS.Models.DataBaseTablesModels;
using SMS_LMS.Models.DataModels.PleasePreDataModels.Validations;
using System;
using System.Globalization;
using System.Reflection;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<SMS_LMS_DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
// implement local
builder.Services.AddLocalization();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<SMS_LMS_DBContext>()
   .AddRoles<ApplicationRole>().AddDefaultTokenProviders();


// Add services to the container.
builder.Services.AddControllers().AddRazorRuntimeCompilation();
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


// Add services to the container.
builder.Services.AddSingleton<SMS_LMS_LocalService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPleasePreviewService, PleasePreviewService>();
builder.Services.AddScoped<ISMSDataService , SMSDataService>();
builder.Services.AddScoped<ISMSService, SMSService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(typeof(SMS_LMS_Profiler));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme,
        securityScheme: new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
});

builder.AddAppAuthetication();

builder.Services.AddAuthorization();

//
builder.Services.Configure<IdentityOptions>(options => options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);

var app = builder.Build();
// Configure the HTTP request pipeline
app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    if (false) //(!app.Environment.IsDevelopment())
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cart API");
        c.RoutePrefix = string.Empty;
    }
});
var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("ar")
                    };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("ar"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider()
    }
};

app.UseRequestLocalization(localizationOptions);
//ApplyMigration();
Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
//void ApplyMigration()
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        var _db = scope.ServiceProvider.GetRequiredService<SMS_LMS_DBContext>();

//        if (_db.Database.GetPendingMigrations().Count() > 0)
//        {
//            _db.Database.Migrate();
//        }
//    }
//}