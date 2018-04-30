using System;
using System.Configuration;

namespace ChatServer.Configuration
{
    public class ConfigurationProvider
    {
        public ConfigurationModel Get()
        {
            return new ConfigurationModel
            {
                IpAddress = ConfigurationManager.AppSettings["ipAddress"],
                Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"])
            };
        }
    }
}
