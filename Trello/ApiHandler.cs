using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Trello.Interfaces;

namespace Trello
{
    public class ApiHandler : IApiHandler
    {
        private static readonly HttpClient client = new HttpClient();
        public Task<T> Get<T>(string uri)
        {
            //await client.SendAsync();
            throw new NotImplementedException();
        }
    }
}
