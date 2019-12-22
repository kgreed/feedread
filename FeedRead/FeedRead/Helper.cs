using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
 
namespace FeedRead
{
    public static  class Helper
    {
        public static FeedReadConfiguration GetApplicationConfiguration( )
        {
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            var iConfig = GetIConfigurationRoot(currentDirectory);
            return iConfig.Get<FeedReadConfiguration>();
        }

        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json")
                .Build();
        }
        
    }
    public class FeedReadConfiguration
    {
        public int Col1Width { get; set; }
    }
}
