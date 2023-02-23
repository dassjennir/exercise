using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using UrlShortener.Data;
using UrlShortener.Data.Models;
using UrlShortener.Data.Repositories;
using UrlShortener.Data.Repositories.Interfaces;
using UrlShortener.Domain.Services;
using UrlShortener.Domain.Services.Interfaces;

namespace UrlShortener.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UrlContext>(options => options.UseInMemoryDatabase(databaseName: "ShortUrlDatabase"));
            services.AddScoped<IUrlService, UrlService>();
            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }

    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new UrlContext(serviceProvider.GetRequiredService<DbContextOptions<UrlContext>>()))
            {
                if (context.UrlPairs.Any()) return;

                context.UrlPairs.AddRange(
                    new UrlPair
                    {
                        Id = Guid.NewGuid(),
                        LongUrl = "http://gmail.com",
                        ShortUrlCode = "a",
                        NumericIndex = 0
                    },
                    new UrlPair
                    {
                        Id = Guid.NewGuid(),
                        LongUrl = "http://google.com",
                        ShortUrlCode = "b",
                        NumericIndex = 1
                    });
                context.SaveChanges();
            }
        }
    }
}
