using System.Text;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Razor.Api.Context;
using Razor.Api.DataAccess.Cache.Base;
using Razor.Api.DataAccess.Cache.Service;
using Razor.Api.DataAccess.Connection;
using Razor.Api.DataAccess.Connection.Impl;
using Razor.Api.DataAccess.Repository.ForEntities.Inventory;
using Razor.Api.DataAccess.Repository.ForEntities.Inventory.Impl;
using Razor.Api.DataAccess.Repository.ForEntities.Inventory.Model;
using Razor.Api.DataAccess.Repository.ForEntities.Kit;
using Razor.Api.DataAccess.Repository.ForEntities.Kit.Impl;
using Razor.Api.DataAccess.Repository.Handler;
using Razor.Api.DataAccess.Repository.Service.Company;
using Razor.Api.DataAccess.Repository.Service.Company.Impl;
using Razor.Api.DataAccess.Repository.Service.Token;
using Razor.Api.DataAccess.Repository.Service.Token.Impl;
using Razor.Api.DataAccess.Repository.Service.User;
using Razor.Api.DataAccess.Repository.Service.User.Impl;
using Razor.Api.Services.V1.Mappings;
using Razor.Api.Services.V1.Services;
using Razor.Api.Services.V1.Services.Impl;
using Razor.Api.Services.V1.Validation.AccessValidation;
using Razor.Api.Services.V1.Validation.AccessValidation.Impl;
using Razor.Api.Services.V1.Validation.AccessValidation.Impl.Rules;
using Razor.Api.Services.V1.Validation.AccessValidation.Rules;
using Razor.Api.Services.V1.Validation.ModelValidation;
using Razor.Api.Services.V1.Validation.ModelValidation.Impl;
using Razor.Api.Services.V1.Validation.ModelValidation.Impl.Rules;
using Razor.Api.Services.V1.Validation.ModelValidation.Rules;
using Razor.Api.Web.Authentication.HeaderParser;
using Razor.Api.Web.Authentication.HeaderParser.Impl;
using Razor.Api.Web.Authentication.TokenSource;
using Razor.Api.Web.Authentication.TokenSource.Impl;
using Razor.Api.Web.Cache.Scope;
using Razor.Api.Web.Cache.Service;
using Razor.Api.Web.Config;
using Razor.Api.Web.Config.Api;
using Razor.Api.Web.Context;
using Razor.Api.Web.Filters;
using Razor.Api.Web.Middlewares;
using Razor.Api.Web.Swagger;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Razor.Api.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            CreateSerilogLogger(configuration);
        }

        private void CreateSerilogLogger(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            });
            //services.AddAutoMapper(typeof(Startup));

            SetupSqlMapper();
            SetupAutoMapper(services);
            SetupApiVersioning(services, _configuration.GetSection("Api").Get<ApiConfig>());
            SetupJwt(services, _configuration.GetSection("TokenConfig").Get<TokenConfig>());
            SetupCache(services, _configuration.GetSection("CacheConfig").Get<CacheConfig>());
            SetupDi(services);
            SetupLogger(services);
            SetupSwagger(services);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            var apiConfig = _configuration.GetSection("Api").Get<ApiConfig>();
            var apiTitle = apiConfig.Title;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
          
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", apiTitle);
                }
            });

          
            app.UseRouting();

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ScopedLoggingMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void SetupLogger(IServiceCollection services)
        {
            services.AddLogging((builder) =>
            {
                builder.AddSerilog(dispose: true);
            });
        }

        private void SetupSqlMapper()
        {
            SqlMapper.AddTypeHandler(typeof(InventoryCapability[]), new JsonObjectTypeHandler());
            SqlMapper.AddTypeHandler(typeof(Substitute[]), new JsonObjectTypeHandler());
        }

        private void SetupCache(IServiceCollection services, CacheConfig cacheConfig)
        {
            services.AddMemoryCache();
            services.AddSingleton(cacheConfig);
        }

        private void SetupAutoMapper(IServiceCollection services)
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(mapperConfig => {
                mapperConfig.AddProfile<KitsProfile>();
                mapperConfig.AddProfile<InventoryProfile>();
                mapperConfig.AddProfile<InventoryCapabilityProfile>();
                mapperConfig.AddProfile<SubstituteProfile>();
            });

            services.AddSingleton(mapperConfiguration.CreateMapper());
        }

        private void SetupSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name         = "Authentication",
                    Description  = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter your token in the text input below.\r\n\r\nExample: \"12345abcdef\"",
                    In           = ParameterLocation.Header,
                    Type         = SecuritySchemeType.Http,
                    Scheme       = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id   = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });
        }

        private void SetupApiVersioning(IServiceCollection services, ApiConfig config)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion                   = new ApiVersion(config.Versions.Default.Major, config.Versions.Default.Minor);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions                   = true;
            });
            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service  
                // note: the specified format code will format the version as "'v'major[.minor][-status]"  
                options.GroupNameFormat = config.GroupNameFormat;

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat  
                // can also be used to control the format of the API version in route templates  
                options.SubstituteApiVersionInUrl = config.SubstituteApiVersionInUrl;
            });
        }

        private void SetupJwt(IServiceCollection services, TokenConfig tokenConfig)
        {
            services.AddSingleton(tokenConfig);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken            = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer           = true,
                    ValidIssuer              = tokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Secret)),
                    ValidateAudience         = true,
                    ValidAudience            = tokenConfig.Audience,
                    ValidateLifetime         = false,
                };
            });
        }

        private void SetupDi(IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ITokenSource, TokenSource>();
            services.AddTransient<IAuthDataReader, BearerTokenDataReader>();
            services.AddTransient<IExecContext, WebExecContext>();
            services.AddTransient<ICompanyDbConnectionFactory, CompanyDbConnectionFactory>();
            services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
            services.AddTransient<ITokenRequestValidationService, TokenRequestValidationService>();
            services.AddTransient<ITokenRequestValidationRule,TokenRequestValidationRule>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IKitsService, KitsService>();
            services.AddTransient<IKitValidationService, KitValidationService>();
            services.AddTransient<IInventoryService, InventoryService>();
            services.AddTransient<IInventoryValidationService, InventoryValidationService>();
            services.AddTransient<ICompanyAccessValidationService, CompanyAccessValidationService>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IKitsRepository, KitsRepository>();
            services.AddTransient<ICompanyRepository, CompanyDataRepository>();
            services.AddTransient<IUserTokenRepository, UserTokenDataRepository>();
            services.AddTransient<IUserAccessValidationService, UserAccessValidationService>();
            services.AddTransient<IUserValidationRule, UserValidationRule>();
            services.AddTransient<ITokenValidationRule, TokenValidationRule>();
            services.AddTransient<IUserRepository, UserDataRepository>();
            services.AddTransient<IInventoryModelValidationRule, InventoryModelValidationRule>();
            services.AddTransient<IKitsModelValidationRules, KitsModelValidationRules>();
            services.AddTransient<ICompanyValidationRule, CompanyValidationRule>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
           

            services.AddTransient<ICacheManager, CacheManager>();
            services.AddTransient<ICompanyUserScopeCacheManager, CompanyUserScopeCacheManager>();
            services.AddTransient<ICompanyScopeCacheManager, CompanyScopeCacheManager>();
            services.AddTransient<ICompanyConnectionCacheService, CompanyConnectionCacheService>();
            services.AddTransient<ICompanyUserTokenCacheService, CompanyUserTokenCacheService>();
            services.AddTransient<ICompanyAccessCacheService, CompanyAccessCacheService>();
            services.AddTransient<ICompanyUserCacheService, CompanyUserCacheService>();

            services.AddScoped<AccessFilterAttribute>();
            services.AddScoped<TokenAccessFilterAttribute>();
        }
    }
}
