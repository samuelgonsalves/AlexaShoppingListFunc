using System;
using System.Collections.Generic;
using System.Text;

namespace Trello.Models
{
    public class Board
    {
        public string BoardId { get; set; }
        public string Name { get; set; }

        public IEnumerable<Card> Cards { get; set; }
    }
}
