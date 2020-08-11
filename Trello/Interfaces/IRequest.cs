using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Trello.Interfaces
{
    public interface IRequest
    {
        Uri Uri { get; }
        HttpMethod Method { get; }

    }
}
