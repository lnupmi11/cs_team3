using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
