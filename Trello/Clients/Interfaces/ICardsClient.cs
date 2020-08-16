using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trello.Responses;

namespace Trello.Clients.Interfaces
{
    public interface ICardsClient
    {
        /// <summary>
        /// Creates a card on a list
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        Task<Card> CreateCardAsync(Card card);
    }
}
