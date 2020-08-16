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
        /// <summary>
        /// Archives all cards on a list
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        Task<Responses.List> ArchiveAllCards(string listId);

        /// <summary>
        /// Gets all the cards on a list
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        Task<IEnumerable<Card>> GetCards(string listId);
    }
}
