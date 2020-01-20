using AttendanceTracker.Data;
using AttendanceTracker.Models.DTO.Validators;
using AttendanceTracker.Repositories;
using AttendanceTracker.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;

namespace AttendanceTracker
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
            services
                .AddControllers()
                .AddFluentValidation(fv => 
                    fv.RegisterValidatorsFromAssemblyContaining<ClassSessionDtoValidator>());

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AttendanceTrackerRef"))
            );

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            services.AddScoped<IClassSessionService, ClassSessionService>();

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v0.1";
                    document.Info.Title = "AttendanceTrackerAPI";
                    document.Info.Description =
                        "A simple ASP.NET Core API for tracking the attendance of students of a sports club.";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Jon Karlsen",
                        Email = "karlsen.jonarild@gmail.com",
                        Url = "https://www.github.com/jakarlse88"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "MIT",
                        Url = "https://opensource.org/licenses/MIT"
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseOpenApi();

            app.UseSwaggerUi3();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}