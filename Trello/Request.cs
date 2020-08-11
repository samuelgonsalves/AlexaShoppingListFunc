using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Trello.Interfaces;

namespace Trello
{
    public class Request : IRequest
    {
        public Uri Uri { get; set; }
        public HttpMethod Method { get; set; } = HttpMethod.Get;
    }
}
