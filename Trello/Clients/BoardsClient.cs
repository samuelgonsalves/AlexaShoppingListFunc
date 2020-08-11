using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Trello.Clients.Interfaces;
using Trello.Interfaces;
using Trello.Responses;

namespace Trello.Clients
{
    public class BoardsClient : ApiClient, IBoardsClient
    {
        public BoardsClient(IApiHandler apiHandler) : base(apiHandler) { }
        
        public async Task<IEnumerable<Card>> GetCardsAsync(string boardId)
        {
            return await ApiHandler.Get<IEnumerable<Card>>(Endpoints.BoardCards(boardId), new Dictionary<string, string> { { "limit", "10" } });
        }
    }
}

