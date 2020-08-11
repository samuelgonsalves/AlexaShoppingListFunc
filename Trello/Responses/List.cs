using System;
using System.Collections.Generic;
using System.Text;

namespace Trello.Responses
{
    public class List
    {
        public string Name { get; set; }

        public IEnumerable<Card> Cards { get; set; }
    }
}
