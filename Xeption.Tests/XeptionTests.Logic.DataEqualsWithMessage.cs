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
            (bool isEqual, string message) = actualXeption.DataEqualsWithDetail(expectedXeption.Data);

            // then
            isEqual.Should().BeTrue();
            message.Should().BeNullOrEmpty();
        }

        [Fact]
        public void ShouldReturnFalseAndMessageStringIfActualDataContainsKeysNotInExpectedData()
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

            var expectedMessage = new StringBuilder();
            expectedMessage.AppendLine($"Expected exception to:");

            expectedMessage.AppendLine(
                $"- have a data count of {expectedXeption.Data.Count}, " +
                $"but found {actualXeption.Data.Count}");

            expectedMessage.AppendLine($"- NOT contain key \"{randomKey}\"");

            // when
            (bool isEqual, string message) = actualXeption.DataEqualsWithDetail(expectedXeption.Data);

            // then
            isEqual.Should().BeFalse();
            message.Should().NotBeNullOrEmpty();
            message.Should().BeEquivalentTo(expectedMessage.ToString().Trim());
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

            var expectedMessage = new StringBuilder();
            expectedMessage.AppendLine($"Expected exception to:");

            expectedMessage.AppendLine(
                $"- have a data count of {expectedXeption.Data.Count}, " +
                $"but found {actualXeption.Data.Count}");

            expectedMessage.AppendLine(
                $"- contain key \"{randomKey}\" with value(s) ['{randomValue}']");

            // when
            (bool isEqual, string message) = actualXeption
                .DataEqualsWithDetail(expectedXeption.Data);

            // then
            isEqual.Should().BeFalse();
            message.Should().NotBeNullOrEmpty();
            message.Should().BeEquivalentTo(expectedMessage.ToString().Trim('\r', '\n'));
        }

        [Fact]
        public void ShouldReturnFalseAndMessageStringIfExpectedDataContainsKeysWithUnmatchedValues()
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

            var expectedMessage = new StringBuilder();
            expectedMessage.AppendLine($"Expected exception to:");

            expectedMessage.AppendLine(
                $"- have key \"{randomKey}\" with value(s) ['{expectedValues}'], " +
                $"but found value(s) ['{actualValues}']");

            // when
            (bool isEqual, string message) = actualXeption
                .DataEqualsWithDetail(expectedXeption.Data);

            // then
            isEqual.Should().BeFalse();
            message.Should().NotBeNullOrEmpty();
            message.Should().BeEquivalentTo(expectedMessage.ToString().Trim('\r', '\n'));
        }
    }
}