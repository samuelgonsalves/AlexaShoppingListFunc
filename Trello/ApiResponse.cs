using System;
using System.Collections.Generic;
using System.Text;
using Trello.Interfaces;

namespace Trello
{
    public class ApiResponse<T> : IApiResponse<T>
    {
        public T Body { get; set; }
        public IResponse Response { get; set; }

        public ApiResponse(IResponse response, T body)
        {
            Response = response;
            Body = body;
        }
    }
}
