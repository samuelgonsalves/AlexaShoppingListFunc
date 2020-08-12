using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trello.Clients.Interfaces;
using Trello.Interfaces;
using Trello;
using Trello.Responses;

namespace Trello.Clients
{
    public class ListsClient : ApiClient, IListsClient
    {
        public ListsClient(IApiHandler apiHandler) : base(apiHandler) { }

        public async Task<Responses.List> ArchiveAllCards(string listId)
        {
            return await ApiHandler.Post<Responses.List>(Endpoints.ListsArchiveAllCards(listId));
        }

        public async Task<IEnumerable<Card>> GetCards(string listId)
        {
            return await ApiHandler.Get<IEnumerable<Card>>(Endpoints.ListsCards(listId), null);
        }
    }
}
