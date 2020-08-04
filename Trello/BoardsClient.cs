using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Trello.Interfaces;
using Trello.Models;

namespace Trello
{
    public class BoardsClient : TrelloApiClient, IBoardsClient
    {
        //public async Task<HttpResponseMessage> CreateCardForListAsync(Card card, string listId)
        //{
        //    var url = "";
        //    return await PostAsync($"{_baseUrl}{url}", new StringContent(""));
        //}

        public Task<IEnumerable<Card>> GetCardsAsync(string boardId)
        {
            throw new NotImplementedException();
        }
    }
}

