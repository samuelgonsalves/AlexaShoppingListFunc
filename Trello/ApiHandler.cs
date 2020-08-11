using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Trello.Interfaces;
//using Trello.QueryStringBuilder;

namespace Trello
{
    public class ApiHandler : IApiHandler
    {
        private static readonly HttpClient _client = new HttpClient();
        private readonly Uri _baseUri;
        private readonly string _apiKey;
        private readonly string _apiToken;

        public ApiHandler(Uri baseUri, string apiKey, string apiToken)
        {
            _baseUri = baseUri;
            _apiKey = apiKey;
            _apiToken = apiToken;
        }

        public async Task<T> Get<T>(string endPoint, Dictionary<string, string> queryParams)
        {
            var request = BuildRequest(endPoint, queryParams, HttpMethod.Get);

            IApiResponse<T> response = await PerformRequestAsync<T>(request);
            return response.Body;
        }

        public async Task<T> Post<T>(string endPoint, Dictionary<string, string> queryParams)
        {
            var request = BuildRequest(endPoint, queryParams, HttpMethod.Post);
            IApiResponse<T> response = await PerformRequestAsync<T>(request);
            return response.Body;
        }

        private IRequest BuildRequest(string endPoint, Dictionary<string, string> queryParams, HttpMethod method)
        {
            if (queryParams == null)
                queryParams = new Dictionary<string, string>();

            queryParams = queryParams.AddParameters(GetAuthenticationParameters());

            var queryString = QueryStringBuilder.Build(queryParams);

            return new Request { Uri = new Uri($"{_baseUri}{endPoint}{queryString}"), Method = method };
        }

        private Dictionary<string, string> GetAuthenticationParameters() => new Dictionary<string, string> { { "key", _apiKey }, { "token", _apiToken } };


        private async Task<IApiResponse<T>> PerformRequestAsync<T>(IRequest request)
        {
            HttpResponseMessage response = await _client.SendAsync(new HttpRequestMessage { RequestUri = request.Uri, Method = request.Method }, HttpCompletionOption.ResponseContentRead);
            
            var parsedResponse = new Response { Body = await response.Content.ReadAsStringAsync(), ContentType = "application/json" };

            return DeserializeResponse<T>(parsedResponse);
        }

        private IApiResponse<T> DeserializeResponse<T>(IResponse response)
        {
            return new ApiResponse<T>(response, JsonConvert.DeserializeObject<T>(response.Body as string));
        }
    }
}
