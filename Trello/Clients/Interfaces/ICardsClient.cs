using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trello.Responses;

namespace Trello.Clients.Interfaces
{
    public interface ICardsClient
    {
        Task<Card> CreateCardAsync(Card card);
    }
}
