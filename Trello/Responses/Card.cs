using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trello.Responses
{
    public class Card
    {
        [JsonProperty("idList")]
        public string ListId { get; set; }

        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
