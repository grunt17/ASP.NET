using Microsoft.AspNetCore.Authentication;
using Kursovaya_BD.Models;
using Microsoft.EntityFrameworkCore;

namespace Kursovaya_BD
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
                // База данных находится в папке пользователя Data Source AttachDbFilename
                //string strConn = "Server = (localdb)\\mssqllocaldb;" +
                //"Database = DBComp; Trusted_Connection = true";

                // Путь к файлу базы данных задано в явном виде
                string strConn = @"Server=(localdb)\mssqllocaldb; " +
                                     @"AttachDbFilename=C:\Users\salih\Kurs4.mdf; " +
                "Trusted_Connection = true;";

                services.AddDbContext<CarContext>(
                                       options => options.UseSqlServer(strConn));
                services.AddMvc();
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

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });
            }
        }
}
