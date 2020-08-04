using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trello
{
    public static class QueryStringBuilder
    {
        public static string Build(string[] parameters)
        {
            return string.Join('&', parameters);
        }

        public static string Build(Dictionary<string,string> parameters)
        {
            var sb = new StringBuilder();
            
            foreach(var param in parameters)
            {
                sb = sb.AppendJoin('&', $"{param.Key}={param.Value}");
            }

            return sb.ToString();
        }

    }
}
