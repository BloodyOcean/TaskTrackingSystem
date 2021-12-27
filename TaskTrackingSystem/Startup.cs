using Administration;
using Administration.Account;
using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
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
using System;
using System.Collections.Generic;
using System.Linq;
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

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();


            services.AddSwaggerGen();

            services.AddControllers();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
