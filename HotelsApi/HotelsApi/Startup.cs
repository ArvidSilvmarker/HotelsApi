using System;
using HotelsApi.Domain.Interfaces;
using HotelsApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace HotelsApi
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
            var appConfiguration = Configuration.GetSection("AppConfiguration").Get<AppConfiguration>();
            services.AddSingleton(appConfiguration);
            services.AddSingleton<IFileReader, FileReader>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddDbContext<HotelContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseDirectoryBrowser();

            app.UseMvc();
        }
    }
}
