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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.Entities;
using Serilog;
using System;
using System.Text;

namespace EnglishApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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
                config.BaseAddress = new Uri(Configuration.GetSection("EnglishWordApiOptions")["Url"]);
                config.Timeout = TimeSpan.FromSeconds(5);
            });

            var translateApiOpt = Configuration.GetSection("TranslateApiOptions");
            services.AddHttpClient("TranslateClient", config =>
            {
                config.BaseAddress = new Uri(translateApiOpt["Url"]);
                config.DefaultRequestHeaders.Add("x-rapidapi-host", translateApiOpt["Host"]);
                config.DefaultRequestHeaders.Add("x-rapidapi-key", translateApiOpt["Key"]);
            });

            services.AddHttpClient("PhotoApiClient", config =>
            {
                config.BaseAddress = new Uri(Configuration.GetSection("PhotoApiOptions")["Url"]);
            });

            //Db connection
            services.AddDbContext<EnglishContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Identity setting
            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<EnglishContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

            //Configure jwt authentication
            var jwtSettings = Configuration.GetSection("JWTSettings");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]))
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
            services.AddScoped(typeof(IBaseGenaricService<>), typeof(BaseGenaricService<>));
            services.AddScoped(typeof(ITestResultService<>), typeof(TestResultService<>));
            services.AddScoped<IJwtTokenService, JwtTokenService>();

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

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            
            app.UseCors(builder => builder.WithOrigins("https://localhost:5011")
                            .AllowAnyMethod()
                            .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
