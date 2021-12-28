using Administration;
using Administration.Account;
using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskTrackingSystem.Helpers;

namespace TaskTrackingSystem
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            string administrationConnection = Configuration.GetConnectionString("AdministrationDB");

            services.AddDbContext<TaskTrackingDbContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<AdministrationDbContext>(options => options.UseSqlServer(administrationConnection));

            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IHistoryRepository, HistoryRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(AutomapperProfile));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
            }).AddEntityFrameworkStores<AdministrationDbContext>();

            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            services
                .AddAuthorization()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                        ClockSkew = TimeSpan.Zero
                    };
                });


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddControllers();

            //services.AddSwaggerGen(c =>
            //{
            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        Description = "JWT containing userid claim",
            //        Name = "Authorization",
            //        In = ParameterLocation.Header,
            //        Type = SecuritySchemeType.ApiKey,
            //    });
            //    var security =
            //        new OpenApiSecurityRequirement
            //        {
            //            {
            //                new OpenApiSecurityScheme
            //                {
            //                    Reference = new OpenApiReference
            //                    {
            //                        Id = "Bearer",
            //                        Type = ReferenceType.SecurityScheme
            //                    },
            //                    UnresolvedReference = true
            //                },
            //                new List<string>()
            //            }
            //        };
            //    c.AddSecurityRequirement(security);
            //});
            services.AddSwaggerGen(setup =>
            {
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI();
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
