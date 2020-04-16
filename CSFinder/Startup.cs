﻿
using CSFinder.Models;
using Hangfire;
using Hangfire.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CSFinder
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CSFinderContext>(options => options.UseSqlite(connection));
            var sqliteOptions = new SQLiteStorageOptions();
            services.AddHangfire(configuration => configuration
                //.UseMemoryStorage(new MemoryStorageOptions { JobExpirationCheckInterval = TimeSpan.FromMinutes(10) })
                .UseSQLiteStorage("Filename=../HangFireDB.db;", sqliteOptions)
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var option = new BackgroundJobServerOptions { WorkerCount = 1 };
            app.UseHangfireServer(option);
            app.UseHangfireDashboard();
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
            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=RegisLogin}/{action=Login}/{id?}");
            });
        }
    }
}
