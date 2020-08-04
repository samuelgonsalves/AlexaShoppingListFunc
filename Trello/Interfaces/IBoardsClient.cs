using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trello.Models;

namespace Trello.Interfaces
{
    public interface IBoardsClient
    {
        /// <summary>
        /// Gets cards for a board based on BoardID
        /// </summary>
        /// <param name="boardId"></param>
        /// <returns></returns>
        Task<IEnumerable<Card>> GetCardsAsync(string boardId);
    }
}
