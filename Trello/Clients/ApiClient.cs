using System;
using System.Collections.Generic;
using System.Text;
using Trello.Interfaces;

namespace Trello.Clients
{
    public abstract class ApiClient
    {
        protected IApiHandler ApiHandler;
        public ApiClient(IApiHandler apiHandler)
        {
            ApiHandler = apiHandler;
        }
    }
}
