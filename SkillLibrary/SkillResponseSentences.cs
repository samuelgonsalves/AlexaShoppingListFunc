using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trello.Responses;

namespace SkillLibrary
{
    public static class SkillResponseSentences
    {
        public static string GetGroceryItems(IEnumerable<Card> cards)
        {
            if (cards.Count() > 10)
                return "I'm sorry! There are more than 10 items in your list. Please look at your Trello board instead.";

            var items = string.Join(',', cards.Select(c => c.Name));

            return $@"Here are the items on your grocery list: {items}";
        }
    }
}
