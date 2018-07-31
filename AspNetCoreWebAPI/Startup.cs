
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using AspNetCoreWebAPI.Services;
using AspNetCoreWebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using AspNetCoreWebAPI.Models;

namespace AspNetCoreWebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var conn = Configuration["connectionStrings:sqlConnectionAPI"];

            //SqlDbContext is our connection to the DB using our connection string from secrets.json(conn)
            services.AddDbContext<SqlDbContext>(options =>
                options.UseSqlServer(conn));

            //Implement Identity Services & Connect to Database
            services.AddDbContextPool<ApplicationDbContext>(options =>
                options.UseSqlServer(conn));
            services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            //Entity Framework allows interaction with DB, Generics allow data types to be assigned at runtime
            services.AddScoped(typeof(IGenericEFRepository), typeof(GenericEFRepository));
        
            //Add JSON Web Token Authentication
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        //Recieve JWT config info from secrets.json
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
            //Using AutoMapper Package to Map Entities to DTOs AND vice versa
            //Entities represent tables in the DB
            //Data Transfer Object is used to turn entity data into a response object OR  convert request data into an entity model
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Entities.Cart, Models.CartDTO>();
                config.CreateMap<Models.CartDTO, Entities.Cart>();
                config.CreateMap<Entities.Product, Models.ProductDTO>();
                config.CreateMap<Models.ProductDTO, Entities.Product>();
                config.CreateMap<Models.CartUpdateDTO, Entities.Cart>();
                config.CreateMap<Entities.Cart, Models.CartUpdateDTO>();
                config.CreateMap<Entities.Product, Models.ProductUpdateDTO>();
                config.CreateMap<Models.ProductUpdateDTO, Entities.Product>();

            });
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Initialize use of Authentication
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
