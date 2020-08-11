using FluentAssertions;
using SkillLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trello;
using Trello.Clients;
using Trello.Responses;
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

        [Fact(Skip = "Needs to be mocked, makes API call")]
        public async Task Test3()
        {
            var trelloClient = new TrelloApiClient("", "");

            var cards = await trelloClient.Boards.GetCardsAsync("");
        }

        [Fact(Skip = "Needs to be mocked, makes API call")]
        public async Task Test4()
        {
            var trelloClient = new TrelloApiClient("", "");
            var card = await trelloClient.Cards.CreateCardAsync(new Card { Name = "Chicken Breast", ListId = "" });
        }

        [Fact]
        public async Task Test5()
        {
            var queryString = QueryStringBuilder.BuildQueryParams(new Card { Name = "Chicken Breast's", ListId = "123", Id = "1" });
            queryString.Should().Be("idList=123&Id=1&name=Chicken%20Breast's");
        }


        [Fact(Skip = "Needs to be mocked, makes API call")]
        public async Task Test6()
        {
            var trelloClient = new TrelloApiClient("", "");
            var card = await trelloClient.Lists.ArchiveAllCards("");

        }

        [Fact]
        public async Task Test7()
        {
            var queryParams = new Dictionary<string, string> { };
            queryParams = queryParams.AddParameters(new Dictionary<string, string> { { "apiKey", "SecretsLiveHere" }, { "apiToken", "MyLittleTolkien" } });

            queryParams.Count.Should().NotBe(0);
        }
    }
}
