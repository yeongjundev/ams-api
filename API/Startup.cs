using System.Text;
using AutoMapper;
using Core.AppDbContext;
using Core.AutoMapper;
using DataAccess;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.AttendanceManager;

namespace API
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
            // EntityFrameworkCore DbContext
            services.ConfigureInMemoryDbContext<AppDbContext>("InMemoryDB");
            // AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile));
            // Services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAttendanceManager, AttendanceManager>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();

                app.UseExceptionHandler(config =>
                {
                    config.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            var e = error.Error;
                            await context.Response.WriteAsync($"Error (to be replaced):\n{e.Message}", Encoding.UTF8);
                        }
                    });
                });
            }

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
