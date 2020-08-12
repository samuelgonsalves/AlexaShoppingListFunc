using FluentAssertions;
using SkillLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void CapitalizeFirstLetterOfEachWord_Capitalizes_First_Letter_Of_Each_Word_Test()
        {
            // Arrange
            string sentence = "this IS a test sentence.";

            // Act, Assert
            sentence.CapitalizeFirstLetterOfEachWord().Should().Be("This IS A Test Sentence.");
        }

        [Fact]
        public void CapitalizeFirstLetterOfEachWord_Capitalizes_First_Letter_Of_Each_Word_And_Removes_Spaces_Test()
        {
            // Arrange
            string sentence = " a sentence with spaces  ";

            // Act, Assert
            sentence.CapitalizeFirstLetterOfEachWord().Should().Be("A Sentence With Spaces");
        }
    }
}
