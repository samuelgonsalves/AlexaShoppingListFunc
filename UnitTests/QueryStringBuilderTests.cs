using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Trello;
using Trello.Responses;
using Xunit;

namespace UnitTests
{
    public class QueryStringBuilderTests
    {

        [Fact]
        public void AddParameters_Returns_Query_Parameters_Including_Parameters()
        {
            // Arrange
            var queryParams = new Dictionary<string, string> { };

            // Act
            queryParams = queryParams.AddParameters(new Dictionary<string, string> { { "apiKey", "SecretsLiveHere" }, { "apiToken", "MyLittleTolkien" } });

            // Assert
            queryParams.Count.Should().NotBe(0);
        }

        [Fact]
        public void BuildQueryParams_Returns_Correctly_Encoded_Query_String()
        {
            // Arrange
            var card = new Card { Name = "Chicken Breast's", ListId = "123", Id = "1" };

            // Act
            var queryString = QueryStringBuilder.BuildQueryParams(card);

            // Assert
            queryString.Should().Be("idList=123&Id=1&name=Chicken%20Breast's");
        }
    }
}
