using System;
using System.Collections.Generic;
using System.Text;
using Trello.Interfaces;

namespace Trello
{
    public class Response : IResponse
    {
        public object Body { get; set; }
        public string ContentType { get; set; }
    }
}
