using System;
using System.Collections.Generic;
using System.Text;

namespace Trello
{
    public static class Endpoints
    {
        public readonly static string Boards = "/boards";
        public readonly static string Cards = "/cards";
        public readonly static string Lists = "/lists";
        public static string BoardCards(string boardId) => $"/boards/{boardId}/cards";
        public static string ListsArchiveAllCards(string listId) => $"/lists/{listId}/archiveAllCards";
        public static string ListsCards(string listId) => $"/lists/{listId}/cards";
    }
}
