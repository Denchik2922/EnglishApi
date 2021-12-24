using BLL.Interfaces;
using BLL.Services;
using DAL;
using EnglishApi.Infrastructure.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            //Db connection
            services.AddDbContext<EnglishContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Identity setting
            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<EnglishContext>();

            //Services
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IDictionaryService, DictionaryService>();
            services.AddScoped(typeof(IBaseGenaricService<>), typeof(BaseGenaricService<>));
            services.AddScoped<ITranslatedWordService, TranslatedWordService>();
            services.AddScoped<IWordService, WordService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            //Add AutoMapper
            services.AddAutoMapper(typeof(DictionaryProfile),
                                   typeof(TagProfile),
                                   typeof(TranslatedWordProfile),
                                   typeof(UserProfile),
                                   typeof(WordProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnglishApi v1"));
            }

            app.UseHttpsRedirection();

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
