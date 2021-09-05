// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using FluentAssertions;
using Xunit;
using CollectionDictionary = System.Collections.IDictionary;

namespace Xeption.Tests
{
    public partial class XeptionTests
    {
        [Fact]
        public void ShouldInheritFromSystemException()
        {
            // given . when . then
            typeof(Xeption).IsSubclassOf(typeof(Exception))
                .Should().BeTrue();
        }

        [Fact]
        public void ShouldAppendListOfKeyValues()
        {
            // given
            Dictionary<string, List<object>> randomDictionary =
                CreateRandomDictionary();

            Dictionary<string, List<object>> expectedDictionary =
                randomDictionary;

            var xeption = new Xeption();

            // when
            foreach (string key in randomDictionary.Keys)
            {
                randomDictionary[key].ForEach(value =>
                    this.xeption.UpsertDataList(key, value));
            }

            CollectionDictionary actualDictionary = this.xeption.Data;

            // then
            foreach (string key in expectedDictionary.Keys)
            {
                actualDictionary[key].Should().BeEquivalentTo(expectedDictionary[key]);
            }
        }
    }
}