using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trello.Clients.Interfaces;
using Trello.Interfaces;
using Trello;

namespace Trello.Clients
{
    public class ListsClient : ApiClient, IListsClient
    {
        public ListsClient(IApiHandler apiHandler) : base(apiHandler) { }

        public async Task<Responses.List> ArchiveAllCards(string listId)
        {
            return await ApiHandler.Post<Responses.List>(Endpoints.ListsArchiveAllCards(listId));
        }
    }
}
