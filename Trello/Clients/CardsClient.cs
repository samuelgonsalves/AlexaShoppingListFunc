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
    public class CardsClient : ApiClient, ICardsClient
    {
        public CardsClient(IApiHandler apiHandler) : base(apiHandler) { }

        public async Task<Card> CreateCardAsync(Card card)
        {
            return await ApiHandler.Post<Card>(Endpoints.Cards, new Dictionary<string, string> { { "name", card.Name }, { "idList", card.ListId } });
        }
    }
}

