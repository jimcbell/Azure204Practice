using Microsoft.Extensions.Configuration;

namespace Az204Common.Extensions
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
