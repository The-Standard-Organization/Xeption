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
        public void ShouldExposeMessageApi()
        {
            // given
            string randomMessage = GetRandomMessage();
            string inputMessage = randomMessage;
            string expectedMessage = inputMessage;

            // when
            var xeption = new Xeption(message: inputMessage);

            // then
            xeption.Message.Should().BeEquivalentTo(expectedMessage);
        }

        [Fact]
        public void ShouldExposeMessageAndExceptionApi()
        {
            // given
            string randomMessage = GetRandomMessage();
            string inputMessage = randomMessage;
            string expectedMessage = inputMessage;
            var inputInnerException = new Exception();
            Exception expectedInnerException = inputInnerException;

            // when
            var xeption = new Xeption(
                message: inputMessage,
                innerException: inputInnerException);

            // then
            xeption.Message.Should().BeEquivalentTo(expectedMessage);
            xeption.InnerException.Should().BeEquivalentTo(expectedInnerException);
        }

        [Fact]
        public void ShouldAppendListOfKeyValues()
        {
            // given
            var xeption = new Xeption();
            
            Dictionary<string, List<object>> randomDictionary =
                CreateRandomDictionary();

            Dictionary<string, List<object>> expectedDictionary =
                randomDictionary;


            // when
            foreach (string key in randomDictionary.Keys)
            {
                randomDictionary[key].ForEach(value =>
                    xeption.UpsertDataList(key, value));
            }

            CollectionDictionary actualDictionary = xeption.Data;

            // then
            foreach (string key in expectedDictionary.Keys)
            {
                actualDictionary[key].Should().BeEquivalentTo(expectedDictionary[key]);
            }
        }
    }
}