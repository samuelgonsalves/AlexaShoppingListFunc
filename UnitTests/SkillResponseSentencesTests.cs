using SkillLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using Trello.Responses;
using Xunit;

namespace UnitTests
{
    public class SkillResponseSentencesTests
    {
        [Fact]
        public void Test()
        {
            var response = SkillResponseSentences.GetGroceryItems(new List<Card> { new Card { Name = "Coffee" }, new Card { Name = "Chocolate" } });
        }

    }
}
