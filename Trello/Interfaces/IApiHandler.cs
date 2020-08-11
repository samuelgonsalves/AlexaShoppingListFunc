using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Trello.Interfaces
{
    public interface IApiHandler
    {
        Task<T> Get<T>(string uri, Dictionary<string, string> queryParams);

        Task<T> Post<T>(string url, Dictionary<string, string> queryParams = null);

    }
}
