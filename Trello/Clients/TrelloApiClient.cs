using System;
using System.Collections.Generic;
using System.Text;
using Trello.Clients.Interfaces;
using Trello.Interfaces;

namespace Trello.Clients
{
    public class TrelloApiClient
    {
        protected IApiHandler apiHandler;
        public IBoardsClient Boards { get; } 
        public ICardsClient Cards { get;  }
        public IListsClient Lists { get; }
        public TrelloApiClient(string apiKey, string apiToken)
        {
            apiHandler = new ApiHandler(new Uri("https://api.trello.com/1"), apiKey, apiToken);
            Boards = new BoardsClient(apiHandler);
            Cards = new CardsClient(apiHandler);
            Lists = new ListsClient(apiHandler);
        }
    }
}
