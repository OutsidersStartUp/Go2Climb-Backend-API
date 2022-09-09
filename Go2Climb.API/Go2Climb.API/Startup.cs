using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Go2Climb.API.Domain.Repositories;
using Go2Climb.API.Domain.Services;
using Go2Climb.API.Persistence.Contexts;
using Go2Climb.API.Persistence.Repositories;
using Go2Climb.API.Security.Authorization.Handlers.Implementations;
using Go2Climb.API.Security.Authorization.Handlers.Interfaces;
using Go2Climb.API.Security.Authorization.Middleware;
using Go2Climb.API.Security.Authorization.Settings;
using Go2Climb.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Go2Climb.API
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
            services.AddCors();
            services.AddRouting(options => options.LowercaseUrls = true);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Go2Climb.API", Version = "v1"});
                c.EnableAnnotations();
            });

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            
            /*
            //Configuration-In-memory Database
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("go2climb-api-in-memory");
            }); */
            services.AddDbContext<AppDbContext>();

            //Dependency Injection Rules
            services.AddScoped<IJwtHandler, JwtHandler>();
            
            
            services.AddScoped<IAgencyReviewRepository, AgencyReviewsRepository>();
            services.AddScoped<IAgencyReviewService, AgencyReviewService>();
            
            services.AddScoped<IServiceReviewRepository, ServiceReviewsRepository>();
            services.AddScoped<IServiceReviewService, ServiceReviewService>();
            
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IHiredServiceRepository, HiredServiceRepository>();
            services.AddScoped<IHiredServiceService, HiredServiceService>();

            services.AddScoped<IAgencyRepository, AgencyRepository>();
            services.AddScoped<IAgencyService, AgencyService>();

            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IActivityService, ActivityService>();

            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IServiceService, ServiceService>();

            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            
            services.AddScoped<IUserService, UserService>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            //AutoMapper Dependency Injection 
            //services.AddAutoMapper(typeof(Startup));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Go2Climb.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<JwtMiddleware>();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}