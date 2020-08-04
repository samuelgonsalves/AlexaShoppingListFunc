using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Trello
{
    public abstract class ApiClient
    {
        private static readonly string _baseUrl = "https://api.trello.com/1/";
        protected static readonly HttpClient client = new HttpClient();

        private readonly string _apiKey;
        private readonly string _apiToken;

        public ApiClient(string apiKey, string apiToken)
        {
            _apiKey = apiKey;
            _apiToken = apiToken;
        }
    }
}
