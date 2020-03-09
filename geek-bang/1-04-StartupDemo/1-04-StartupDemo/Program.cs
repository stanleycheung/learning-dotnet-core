using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace _1_04_StartupDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 执行顺序：
        /// ConfigureWebHostDefaults ->
        /// ConfigureHostConfiguration ->
        /// ConfigureAppConfiguration ->
        /// ConfigureServices / ConfigureLogging / Startup / Startup.ConfigureServices ->
        /// Startup.Configure
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                {
                    Console.WriteLine("ConfigureAppConfiguration");
                })
                .ConfigureServices(service =>
                {
                    Console.WriteLine("ConfigureServices");
                })
                .ConfigureHostConfiguration(builder =>
                {
                    Console.WriteLine("ConfigureHostConfiguration");
                })
                .ConfigureLogging(builder => {
                    Console.WriteLine("ConfigureLogging");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Console.WriteLine("ConfigureWebHostDefaults");

                    //Startup不是必须的
                    webBuilder.UseStartup<Startup>();

                    #region 可以使用以下代码来代替
                    //webBuilder.ConfigureServices(services =>
                    //{
                    //    Console.WriteLine("webBuilder.ConfigureServices");
                    //    services.AddControllers();
                    //});

                    //webBuilder.Configure(app =>
                    //{
                    //    Console.WriteLine("webBuilder.Configure");

                    //    app.UseHttpsRedirection();

                    //    app.UseRouting();

                    //    app.UseAuthorization();

                    //    app.UseStaticFiles();

                    //    app.UseWebSockets();

                    //    app.UseEndpoints(endpoints =>
                    //    {
                    //        endpoints.MapControllers();
                    //    });
                    //});
                    #endregion
                });
    }
}
