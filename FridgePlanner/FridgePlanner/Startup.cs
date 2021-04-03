using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgePlanner.Models;
using FridgePlanner.Repository;
using FridgePlanner.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FridgePlanner
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<RepoDataBaseContext>(options => options.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=FridgePlannerRepos;  Integrated Security=True"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // register repositorys for FridgeItems, ShoppingItems and Recipes including RecipeItems and RecipeSteps
            services.AddScoped<FridgeItemRepository>();
            services.AddScoped<ShoppingItemRepository>();
            services.AddScoped<RecipeRepository>();
            
            // register ApiCaller as Access Point to the ApiControllers
            services.AddHttpClient<IApiCaller, ApiCaller>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Fridge}/{action=Index}/{id?}");
            });
        }
    }
}
