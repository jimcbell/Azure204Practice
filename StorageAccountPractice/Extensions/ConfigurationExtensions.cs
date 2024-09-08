using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAccountPractice.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetAppSetting(this IConfiguration config, string key)
        {
            string? value = config[key];
            if (value == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return value;
        }
    }
}
