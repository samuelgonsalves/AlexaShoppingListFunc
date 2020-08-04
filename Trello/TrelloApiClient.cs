using System;
using System.Collections.Generic;
using System.Text;
using Trello.Interfaces;

namespace Trello
{
    public class TrelloApiClient
    {
        protected IApiHandler apiHandler;
        public IBoardsClient Boards; 

        public TrelloApiClient()
        {
            Boards = new BoardsClient();
        }
    }
}
