using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trello
{
    public static class QueryStringBuilder
    {
        public static string Build(Dictionary<string,string> parameters = null)
        {
            if (parameters == null)
                return string.Empty;

            var sb = new StringBuilder();
            
            sb = sb.AppendJoin('&', parameters.Select(i => Uri.EscapeUriString($"{i.Key}={i.Value}")));

            return $"?{sb}";
        }

        public static string BuildQueryParams<T>(this T type)
        {
            var properties = from prop in type.GetType().GetProperties()
                             let property = prop.GetValue(type)
                             where property != null
                             let jsonAttr = prop.GetCustomAttributes(typeof(JsonPropertyAttribute), false).FirstOrDefault()
                             select $"{(jsonAttr as JsonPropertyAttribute)?.PropertyName ?? prop.Name}={Uri.EscapeUriString(property as string)}";

            return string.Join('&', properties);
        }

        public static Dictionary<string, string> AddParameters(this Dictionary<string, string> queryParams, Dictionary<string, string> parametersToAdd)
        {
            return queryParams.Union(parametersToAdd).ToDictionary(k => k.Key, v => v.Value);
        }
    }
}
