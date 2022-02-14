using BLL.Interfaces.Entities;
using BLL.Interfaces.HttpApi;
using BLL.Interfaces.Testing;
using BLL.Services.Entities;
using BLL.Services.HttpApi;
using BLL.Services.Testing;
using DAL;
using EnglishApi.Infrastructure.AuthorizationHandlers;
using EnglishApi.Infrastructure.Profiles;
using EnglishApi.Middleware;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.Entities;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace EnglishApi
{
    public class Startup
    {
        public string StaticFileFolder { get; }
        public IConfiguration Configuration { get; }
        public string ConnectionString { get; }
        public string JwtValidIssuer { get; }
        public string JwtValidAudience { get; }
        public string JwtSecret { get; }
        public string UrlEnglishWordApi { get; }
        public string UrlPhotoApi { get; }
        public string UrlTranslateApi { get; }
        public string HostTranslateApi { get; }
        public string KeyTranslateApi { get; }
        public string GoogleClientId { get; }
        public string GoogleClientSecret { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            ConnectionString = Configuration.GetConnectionString("DefaultConnection");

            var jwtSettings = Configuration.GetSection("JWTSettings");
            JwtValidIssuer = jwtSettings["validIssuer"];
            JwtValidAudience = jwtSettings["validAudience"];
            JwtSecret = jwtSettings["Secret"];

            var translateApiOpt = Configuration.GetSection("TranslateApiOptions");
            UrlTranslateApi = translateApiOpt["Url"];
            HostTranslateApi = translateApiOpt["Host"];
            KeyTranslateApi = translateApiOpt["Key"];

            UrlEnglishWordApi = Configuration.GetSection("EnglishWordApiOptions")["Url"];
            UrlPhotoApi = Configuration.GetSection("PhotoApiOptions")["Url"];

            GoogleClientId = Configuration["Authentication:Google:ClientId"];
            GoogleClientSecret = Configuration["Authentication:Google:ClientSecret"];

            StaticFileFolder = Configuration["StaticFilesOptions:MainFolder"];
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EnglishApi", Version = "v1" });

            });

            //Http Clients
            services.AddHttpClient("WordInfoClient", config =>
            {
                config.BaseAddress = new Uri(UrlEnglishWordApi);
                config.Timeout = TimeSpan.FromSeconds(5);
            });

            services.AddHttpClient("TranslateClient", config =>
            {
                config.BaseAddress = new Uri(UrlTranslateApi);
                config.DefaultRequestHeaders.Add("x-rapidapi-host", HostTranslateApi);
                config.DefaultRequestHeaders.Add("x-rapidapi-key", KeyTranslateApi);
            });

            services.AddHttpClient("PhotoApiClient", config =>
            {
                config.BaseAddress = new Uri(UrlPhotoApi);
            });

            //Db connection
            services.AddDbContext<EnglishContext>(options =>
               options.UseSqlServer(ConnectionString));

            //Identity setting
            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<EnglishContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

            //Configure jwt authentication
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = JwtValidIssuer,
                    ValidAudience = JwtValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret))
                };
            });

            //Configure authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("GetDictionaryPolicy", policy =>
                    policy.Requirements.Add(new DictionaryGetRequirement()));
                options.AddPolicy("EditDictionaryPolicy", policy =>
                    policy.Requirements.Add(new DictionaryEditRequirement()));
            });

            //AuthorizationHandlers
            services.AddSingleton<IAuthorizationHandler, DictionaryGetHandler>();
            services.AddSingleton<IAuthorizationHandler, DictionaryEditHandler>();

            //Entity Services
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IDictionaryService, DictionaryService>();
            services.AddScoped<ITranslatedWordService, TranslatedWordService>();
            services.AddScoped<IWordService, WordService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGenerateWordService, GenerateWordService>();
            services.AddScoped<ITestResultService, TestResultService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IUploadImagesService, UploadImagesService>();
            services.AddScoped<ITypeOfTestingService, TypeOfTestingService>();

            //Http Services
            services.AddScoped<IHttpPhotoApiService, HttpPhotoApiService>();
            services.AddScoped<IHttpWordApiService, HttpWordApiService>();
            services.AddScoped<IHttpTranslateApiService, HttpTranslateApiService>();

            //Testing Services
            services.AddScoped<IMatchingWordTest, MatchingWordTest>();
            services.AddScoped<ISpellingTranslationTest, SpellingTranslationTest>();


            //Add AutoMapper
            services.AddAutoMapper(typeof(DictionaryProfile),
                                   typeof(TagProfile),
                                   typeof(TranslatedWordProfile),
                                   typeof(UserProfile),
                                   typeof(WordProfile),
                                   typeof(TestResultProfile),
                                   typeof(TestParametersProfile));

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnglishApi v1"));
            }

            app.UseMiddleware<ExceptionMiddleware>(loggerFactory.CreateLogger(nameof(ExceptionMiddleware)));

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), StaticFileFolder)),
                RequestPath = new PathString($"/{StaticFileFolder}")
            });

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(builder => builder.WithOrigins("https://localhost:5011")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithExposedHeaders("X-Pagination"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
