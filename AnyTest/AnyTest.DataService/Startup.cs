using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnyTest.DbAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AnyTest.IDataRepository;
using AnyTest.MSSQLNetCoreDataRepository;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AnyTest.DataService
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
            services.AddDbContext<AnyTestDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:LocalMSSQLServerWindows"]));
            services.AddDbContext<AnyTestIdentityDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:LocalMSSQLAuthorizationWindows"]));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AnyTestIdentityDbContext>().AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:JwtSecurityKey"]))
                };
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AnyTestDbContext ctx, AnyTestIdentityDbContext ictx)
        {
            ctx.Database.Migrate();
            ictx.Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            AnyTestIdentityDbContext.SeedData(app);
        }
    }
}
