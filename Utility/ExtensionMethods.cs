using System;
namespace Utility
{
    public static class ExtensionMethods
    {
        public static int ToInt32(this object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (System.Exception)
            {
                return default(int);
            }
        }

        public static string TrimIfNull(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return str.Trim();
        }

        public static void DisposeWithoutException<T>(this T obj) where T : IDisposable
        {
            try
            {
                obj.Dispose();
            }
            catch (System.Exception)
            {
            }
        }

        public static string Serialize(this object data, bool ignoreNull = false, bool isFormatIndent = false)
        {
            if (data == null)
                return null;

            var _options = new System.Text.Json.JsonSerializerOptions()
            {
                IgnoreNullValues = ignoreNull,
                WriteIndented = isFormatIndent
            };
            var json = System.Text.Json.JsonSerializer.Serialize(data, options: _options);
            return json;
        }

        public static T Deserialize<T>(this string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return default(T);
            }
            var data = System.Text.Json.JsonSerializer.Deserialize<T>(json);
            return data;
        }

        public static T GetService<T>(this IServiceProvider serviceProvider) where T : class
        {
            try
            {
                T _item = serviceProvider.GetService(typeof(T)) as T;
                return _item;
            }
            catch (System.Exception)
            {
            }
            return default(T);
        }
    }
}