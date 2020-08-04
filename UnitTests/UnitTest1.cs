using FluentAssertions;
using SkillLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trello;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string sentence = "this IS a test sentence.";

            sentence.CapitalizeFirstLetterOfEachWord().Should().Be("This IS A Test Sentence.");
        }

        [Fact]
        public void Test2()
        {
            string sentence = " a sentence with spaces  ";

            sentence.CapitalizeFirstLetterOfEachWord().Should().Be("A Sentence With Spaces");
        }

        //[Fact]
        //public void Test3()
        //{
        //    string[] parameters = { "key=123", "name=test", "limit=10" };
        //    var par = new Dictionary<string, string> { { "key", "123" }, { "name", "test" }, { "limit", "10" } };

        //    QueryStringBuilder.Build(par).Should().Be($"{parameters[0]}&{parameters[1]}&{parameters[2]}");
        //}

        [Fact]
        public async Task Test4()
        {
            //var trelloClient = new TrelloApiClient("", "");

            //var cards = await trelloClient.Boards.GetCardsAsync("");

        }
    }
}
