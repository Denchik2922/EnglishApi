using BLL.Interfaces.Entities;
using BLL.Interfaces.HttpApi;
using BLL.Interfaces.Testing;
using BLL.Services.Entities;
using BLL.Services.HttpApi;
using BLL.Services.Testing;
using DAL;
using EnglishApi.Infrastructure.Profiles;
using EnglishApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models.Entities;
using Serilog;
using System;
using System.Linq;

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
            //Http Clients
            services.AddHttpClient("WordInfoClient", config =>
            {
                config.BaseAddress = new Uri(Configuration.GetSection("EnglishWordApiOptions")["Url"]);
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


            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EnglishApi", Version = "v1" });
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

            //Entity Services
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IDictionaryService, DictionaryService>();
            services.AddScoped(typeof(IBaseGenaricService<>), typeof(BaseGenaricService<>));
            services.AddScoped<ITranslatedWordService, TranslatedWordService>();
            services.AddScoped<IWordService, WordService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGenerateWordService, GenerateWordService>();

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
                                   typeof(TestResultProfile));
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
