using System;
using System.Text;
using System.Threading.Tasks;
using Trello;

namespace Trello.Clients.Interfaces
{
    public interface IListsClient
    {
        Task<Responses.List> ArchiveAllCards(string listId); 
    }
}
