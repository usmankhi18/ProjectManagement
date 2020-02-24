using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ProjectManagement.Utilities
{
    public class CommonObjects
    {
        public static string GetCongifValue(string ConfigKey)
        {
            return ConfigurationManager.AppSettings[ConfigKey].ToString();
        }

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[GenericConstants.ConnectionStringKey].ConnectionString;
        }

    }
}
