using Airtraffic.Domain.Models;
using Airtraffic.Domain.Repositories;
using Airtraffic.Domain.Services;
using Airtraffic.Persistence.Contexts;
using Airtraffic.Persistence.Repositories;
using Airtraffic.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Airtraffic
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
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("airtraffic-in-memory");
            });

           

            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<IAircraftService<Airplane>, AirplaneCrudService>();
            services.AddScoped<IAircraftService<Glider>, GliderCrudService>();

            services.AddScoped<IAirportRepository, AirportRepository>();
            services.AddScoped<IAircraftRepository<Airplane>, AirplaneRepository>();
            services.AddScoped<IAircraftRepository<Glider>, GliderRepository>();
         
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
