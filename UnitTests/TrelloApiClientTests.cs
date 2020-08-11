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
    public class TrelloApiClientTests
    {
        

        [Fact(Skip = "Needs to be mocked, makes API call")]
        public async Task GetCardsAsync_Returns_Cards_From_A_Board_Test()
        {
            // Arrange
            var trelloClient = new TrelloApiClient("APIKEY", "APITOKEN");

            // Act
            var cards = await trelloClient.Boards.GetCardsAsync("");

            // Assert
            cards.Count().Should().BeGreaterThan(0);
        }

        [Fact(Skip = "Needs to be mocked, makes API call")]
        public async Task CreateCardAsync_Creates_A_Card_For_A_List_Test()
        {
            // Arrange
            var trelloClient = new TrelloApiClient("APIKEY", "APITOKEN");

            // Act
            var card = await trelloClient.Cards.CreateCardAsync(new Card { Name = "Chicken Breast", ListId = "" });
        }

        [Fact(Skip = "Needs to be mocked, makes API call")]
        public async Task ArchiveAllCards_Archives_All_Cards_For_A_List_Test()
        {
            // Arrange
            var trelloClient = new TrelloApiClient("APIKEY", "APITOKEN");

            // Act
            var card = await trelloClient.Lists.ArchiveAllCards("");
        }
    }
}
