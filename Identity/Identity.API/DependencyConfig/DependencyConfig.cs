using Asp.Versioning;
using Carter;
using Event.Infrastructure.Queries;
using Identity.API.Services;
using Identity.Application.Feature.BackGroundService;
using Identity.Application.Features.Users.Queries.GetUsers;
using Identity.Application.Handlers;
using Identity.Application.Interfaces;
using Identity.Application.MessageBus;
using Identity.Domain.Interfaces.Services;
using Identity.Domain.Settings;
using Identity.Infrastructure.Context;
using Identity.Infrastructure.EmailHandler;
using Identity.Infrastructure.Queries;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Identity.API.DependencyConfig
{
    public class DependencyConfig
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCarter();

            services.AddControllers();

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
            services.AddHttpClient();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(GetUsersQuery).Assembly);
            });

            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserQuery,UserQuery>();
            services.AddScoped<IUserEventQuery, UserEventQuery>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();

            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddHostedService<NotificationService>();
            services.AddScoped<GetEventUpComingRequest>(); 
            services.AddMemoryCache();

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