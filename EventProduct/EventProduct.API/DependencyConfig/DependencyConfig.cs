using Asp.Versioning;
using Carter;
using EventProduct.Application.Interfaces;
using EventProduct.Domain.Settings;
using EventProduct.Infrastructure.Context;
using EventProduct.Infrastructure.EmailHandler;
using EventProduct.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ApiVersion = Asp.Versioning.ApiVersion;
using Newtonsoft.Json.Converters;
using EventProduct.Application.Interface;
using EventProduct.Infrastructure.Queries;
using EventProduct.Domain.Entities;

namespace EventProduct.API.DependencyConfig
{
    public class DependencyConfig
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCarter();

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = null // Giữ nguyên tên thuộc tính
                    };
                    options.SerializerSettings.Converters.Add(new StringEnumConverter()); // Serialize Enum thành chuỗi
                });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
                });
            });

            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped<IEventQuery, EventQuery>();
            services.AddScoped<ICategoryQuery, CategoryQuery>();
            services.AddScoped<IProductQuery, ProductQuery>();
            services.AddScoped<IEventOrderQuery, EventOderQuery>();
            services.AddScoped<IEventOrderProductQuery, EventOrderProductQuery>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
   
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
                    };
                });
            services.AddAuthorizationBuilder()
                .AddPolicy("AdminPolicy", policy =>
                    policy.RequireRole("Admin"))
                .AddPolicy("StaffPolicy", policy =>
                    policy.RequireRole("Staff"))
                .AddPolicy("UserPolicy", policy =>
                    policy.RequireRole("User"));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}