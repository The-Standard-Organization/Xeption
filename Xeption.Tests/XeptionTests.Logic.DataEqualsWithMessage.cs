// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Force.DeepCloner;
using Xunit;

namespace Xeptions.Tests
{
    public partial class XeptionTests
    {
        [Fact]
        public void ShouldReturnTrueAndNullStringIfDataMatchInCount()
        {
            // given
            Xeption randomXeption = new Xeption();

            Dictionary<string, List<string>> randomDictionary = CreateRandomDictionary();
            randomXeption.AddData(randomDictionary);
            Xeption expectedXeption = randomXeption;
            Xeption actualXeption = expectedXeption.DeepClone();

            // when
            var actualComparisonResult = actualXeption.DataEqualsWithDetail(expectedXeption.Data);

            // then
            actualComparisonResult.IsEqual.Should().BeTrue();
            actualComparisonResult.Message.Should().BeNullOrEmpty();
        }

        [Fact]
        public void ShouldReturnFalsAndMessageStringIfActualDataContainsKeysNotInExpectedData()
        {
            // given
            Xeption randomXeption = new Xeption();
            string randomKey = GetRandomMessage();
            string randomValue = GetRandomMessage();

            Dictionary<string, List<string>> randomDictionary =
                CreateRandomDictionary();

            randomXeption.AddData(randomDictionary);
            Xeption expectedXeption = randomXeption;
            Xeption actualXeption = expectedXeption.DeepClone();
            actualXeption.UpsertDataList(randomKey, randomValue);

            StringBuilder expectedMessage = new StringBuilder();
            expectedMessage.AppendLine($"Expected data to: ");

            expectedMessage.AppendLine(
                $"- have a count of {expectedXeption.Data.Count}, " +
                $"but found {actualXeption.Data.Count}.");

            expectedMessage.AppendLine($"- NOT contain key '{randomKey}'.");

            // when
            var actualComparisonResult = actualXeption.DataEqualsWithDetail(expectedXeption.Data);

            // then
            actualComparisonResult.IsEqual.Should().BeFalse();
            actualComparisonResult.Message.Should().NotBeNullOrEmpty();
            actualComparisonResult.Message.Should().BeEquivalentTo(expectedMessage.ToString());
        }

        [Fact]
        public void ShouldReturnFalseAndMessageStringIfExpectedDataContainsKeysNotInActulaData()
        {
            // given
            Xeption randomXeption = new Xeption();
            string randomKey = GetRandomMessage();
            string randomValue = GetRandomMessage();

            Dictionary<string, List<string>> randomDictionary =
                CreateRandomDictionary();

            randomXeption.AddData(randomDictionary);
            Xeption expectedXeption = randomXeption;
            Xeption actualXeption = expectedXeption.DeepClone();
            expectedXeption.UpsertDataList(randomKey, randomValue);

            // when
            var actualComparisonResult = actualXeption
                .DataEqualsWithDetail(expectedXeption.Data);

            // then
            actualComparisonResult.IsEqual.Should().BeFalse();
            actualComparisonResult.Message.Should().NotBeNullOrEmpty();

            actualComparisonResult.Message.Should()
                .Contain($"- Expected data item count to be {expectedXeption.Data.Count}, " +
                    $"but found {actualXeption.Data.Count}.");

            actualComparisonResult.Message.Should()
                .Contain($"- Expected to find key '{randomKey}'.");
        }

        [Fact]
        public void ShouldReturnFalseAndMessageStringIfExpectedDataContainsKeysMatchingKeysWithUnmatchedValues()
        {
            // given
            Xeption randomXeption = new Xeption();
            string randomKey = GetRandomMessage();
            string randomValue1 = GetRandomMessage();
            string randomValue2 = GetRandomMessage();
            string randomValue3 = GetRandomMessage();
            string randomValue4 = GetRandomMessage();

            Xeption expectedXeption = randomXeption;
            Xeption actualXeption = expectedXeption.DeepClone();
            expectedXeption.UpsertDataList(randomKey, randomValue1);
            expectedXeption.UpsertDataList(randomKey, randomValue2);
            actualXeption.UpsertDataList(randomKey, randomValue3);
            actualXeption.UpsertDataList(randomKey, randomValue4);

            string expectedValues = ((List<string>)expectedXeption.Data[randomKey])
                .Select(value => value).Aggregate((t1, t2) => t1 + "','" + t2);

            string actualValues = ((List<string>)actualXeption.Data[randomKey])
                .Select(value => value).Aggregate((t1, t2) => t1 + "','" + t2);

            // when
            var actualComparisonResult = actualXeption
                .DataEqualsWithDetail(expectedXeption.Data);

            // then
            actualComparisonResult.IsEqual.Should().BeFalse();
            actualComparisonResult.Message.Should().NotBeNullOrEmpty();

            actualComparisonResult.Message.Should()
                .Contain($"- Expected to find key '{randomKey}' with value(s) ['{expectedValues}'], " +
                    $"but found value(s) ['{actualValues}'].");
        }
    }
}