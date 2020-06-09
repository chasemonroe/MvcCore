using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialHub.DAL;
using SocialHub.Models;
using Microsoft.EntityFrameworkCore;

namespace SocialHub
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

            // Entity Framework Setup. Pool method is better, introduced in 2.0
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("SocialHubConnection")));

            services.AddControllersWithViews();

            // AddMvc() method calls AddMvcCore(). Core has less options
            // core is the 'core' functionality 

            // AddXMLSerializerFormatter - add XML content negotiation
            services.AddMvc().AddXmlSerializerFormatters();

            // Dependency Injection 
            // If someone requests this IUserRepository, make an instance of the actual type
            // SQLUserRepository - The actual DB 
            // MockUserRepository
            services.AddScoped<IUserRepository, SQLUserRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Routing 
            app.UseEndpoints(endpoints =>
            {
               // Use this for CONVENTIONAL based Routing
               endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");

                // Use this to enable ATTRIBUTE based routing
                //endpoints.MapControllers();
            });
        }
    }
}
