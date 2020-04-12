using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AnyTest.DataService
{
    /// <summary>
    /// \~english A service startup class    
    /// \~ukrainian Клас старту служби
    /// </summary>
    public class Program
    {
        /// <summary>
        /// \~english A service statrup method
        /// \~ukrainian Метод старту слжуби
        /// </summary>
        /// <param name="args">
        /// \~english Command line arguments
        /// \~ukrainian Аргументи командного рядка
        /// </param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// \~english A method, creating a web host builder
        /// \~ukrainian Метод, який створює будівника веб-хосту
        /// </summary>
        /// <param name="args">
        /// \~english Command line arguments from <c>Main</c> method
        /// \~ukrainian Аргументи командного рдяка з метода <c>Main</c>
        /// </param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
