﻿using Emi.Employees.Application.Mappings;
using Emi.Employees.Application.Port;
using Emi.Employees.Application.UseCase;
using Emi.Employees.Domain.IRepository;
using Emi.Employees.Domain.Unit;
using Emi.Employees.Infrastructure;
using Emi.Employees.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.Text;

namespace Emi.Employee.Api.DI
{
    /// <summary>
    /// Clase encargada de realizar las IoC del proyecto
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Metodo encargado de realizar las Injection de las clases
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            AddRegisterDBContext(services, configuration);
            AddRegisterApplication(services);
            AddRegisterInfrastructure(services);
            AddAuthenticationLib(services, configuration);
            services.AddEndpointsApiExplorer();
            AddSwaggerConf(services);
            Cors(services);
            return services;
        }

        private static void AddRegisterDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmiDbContext>(cfg => cfg.UseSqlServer(configuration.GetConnectionString("cnxEmi")));
            services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<EmiDbContext>());
        }

        private static void AddRegisterApplication(IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<EmployeeMapperProfile>(), AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IEmployeePort, EmployeeUseCase>();
            services.AddTransient<IUserPort, UserUseCase>();
            services.AddScoped<IEmployeeProjectPort, EmployeeProjectUseCase>();
        }

        private static void AddRegisterInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPositionHistoryRepository, PositionHistoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IEmployeeProjectRepository, EmployeeProjectRepository>();
        }

        private static void Cors(this IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(name: "politica", builder =>
            {
                builder
                       .AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        private static void AddSwaggerConf(this IServiceCollection services)
        {

            // Agregamos servicios para Swagger
            services.AddSwaggerGen(options =>
            {
                // Configuramos la seguridad del token JWT en Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Ingrese el token JWT con el prefijo 'Bearer '",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        []
                    }
                });
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Emi", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        private static void AddAuthenticationLib(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("Jwt");
            var issuer = jwtConfig.GetValue<string>("Issuer");
            var audience = jwtConfig.GetValue<string>("Audience");
            var keyEncript = jwtConfig.GetValue<string>("SecretKey");

            // Configuramos la autenticación JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyEncript))
                };
            });

            services.AddAuthorizationBuilder()
                .AddPolicy("ADM", policy => policy.RequireRole("ADM"))
                .AddPolicy("USR", policy => policy.RequireRole("USR"))
                .AddPolicy("ALL", policy => policy.RequireRole("ADM", "USR"));
        }
    }
}
