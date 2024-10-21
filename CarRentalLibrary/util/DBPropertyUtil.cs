using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace UtilityPro // Namespace for organizing the UtilityPro classes
{
    public static class DBPropertyUtil
    {
        // Field to hold the configuration settings
        private static IConfigurationRoot _configuration;

        
        static DBPropertyUtil()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); 
            _configuration = builder.Build();
        }

        
        public static string ReturnCn(string key)
        {
            return _configuration.GetConnectionString(key);
        }
    }
}
