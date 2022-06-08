// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Collections.Generic;
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

            // when
            var actualComparisonResult = actualXeption.DataEqualsWithDetail(expectedXeption.Data);

            // then
            actualComparisonResult.IsEqual.Should().BeFalse();
            actualComparisonResult.Message.Should().NotBeNullOrEmpty();

            actualComparisonResult.Message.Should()
                .Contain($"- Expected data item count to be {expectedXeption.Data.Count}, " +
                    $"but found {actualXeption.Data.Count}.");

            actualComparisonResult.Message.Should()
                .Contain($"- Did not expect to find key '{randomKey}'.");
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
                .Contain($"- Did not expect to find key '{randomKey}'.");
        }
    }
}