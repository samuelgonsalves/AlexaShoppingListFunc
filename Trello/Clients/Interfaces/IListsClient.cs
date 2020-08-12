using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trello;
using Trello.Responses;

namespace Trello.Clients.Interfaces
{
    public interface IListsClient
    {
        Task<Responses.List> ArchiveAllCards(string listId);

        Task<IEnumerable<Card>> GetCards(string listId);
    }
}
