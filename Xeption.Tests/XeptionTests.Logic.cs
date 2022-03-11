// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using ICollectionDictionary = System.Collections.IDictionary;

namespace Xeptions.Tests
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

            Dictionary<string, List<string>> randomDictionary =
                CreateRandomDictionary();

            Dictionary<string, List<string>> expectedDictionary =
                randomDictionary;

            // when
            foreach (string key in randomDictionary.Keys)
            {
                randomDictionary[key].ForEach(value =>
                    xeption.UpsertDataList(key, value));
            }

            ICollectionDictionary actualDictionary = xeption.Data;

            // then
            foreach (string key in expectedDictionary.Keys)
            {
                actualDictionary[key].Should().BeEquivalentTo(expectedDictionary[key]);
            }
        }

        [Fact]
        public void ShouldAddDataAsDictionary()
        {
            // given
            var xeption = new Xeption();

            Dictionary<string, List<string>> randomDictionary =
                CreateRandomDictionary();

            Dictionary<string, List<string>> expectedDictionary =
                randomDictionary;

            // when
            xeption.AddData(randomDictionary);

            ICollectionDictionary actualDictionary = xeption.Data;

            // then
            foreach (string key in expectedDictionary.Keys)
            {
                actualDictionary[key].Should().BeEquivalentTo(expectedDictionary[key]);
            }
        }

        [Fact]
        public void ShouldAddDataAsParameters()
        {
            // given
            var xeption = new Xeption();

            Dictionary<string, List<string>> randomDictionary =
                CreateRandomDictionary();

            Dictionary<string, List<string>> expectedDictionary =
                randomDictionary;

            // when
            foreach (string key in randomDictionary.Keys)
            {
                xeption.AddData(key, randomDictionary[key].ToArray());
            }

            ICollectionDictionary actualDictionary = xeption.Data;

            // then
            foreach (string key in expectedDictionary.Keys)
            {
                actualDictionary[key].Should().BeEquivalentTo(expectedDictionary[key]);
            }
        }

        [Fact]
        public void ShouldDoNothingOnAddDictionaryIfNull()
        {
            // given
            var xeption = new Xeption();
            ICollectionDictionary nullDictionary = null;

            // when
            xeption.AddData(dictionary: nullDictionary);

            ICollectionDictionary actualDictionary = xeption.Data;

            // then
            actualDictionary.Count.Should().Be(0);
        }

        [Fact]
        public void ShouldThrowIfContainsErrors()
        {
            // given
            var xeption = new Xeption();
            string someKey = GetRandomMessage();
            string someValue = GetRandomMessage();

            // when
            xeption.UpsertDataList(
                key: someKey,
                value: someValue);

            // then
            Assert.Throws<Xeption>(() =>
                xeption.ThrowIfContainsErrors());
        }

        [Fact]
        public void ShouldNotThrowIfContainsNoErrors()
        {
            // given
            var xeption = new Xeption();

            // when
            Exception actualException = Record.Exception(() =>
                xeption.ThrowIfContainsErrors());

            // then
            actualException.Should().BeNull();
        }

        [Fact]
        public void ShouldReturnTrueIfDataIsTheSame()
        {
            // given
            var xeption = new Xeption();

            Dictionary<string, List<string>> randomDictionary =
                CreateRandomDictionary();

            xeption.AddData(randomDictionary);

            // when
            bool isEqual = xeption.DataEquals(randomDictionary);

            // then
            isEqual.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnFalseIfDataIsNotTheSame()
        {
            // given
            var xeption = new Xeption();

            Dictionary<string, List<string>> randomDictionary =
                CreateRandomDictionary();

            Dictionary<string, List<string>> otherRandomDictionary =
                CreateRandomDictionary();

            xeption.AddData(randomDictionary);

            // when
            bool isEqual = xeption.DataEquals(otherRandomDictionary);

            // then
            isEqual.Should().BeFalse();
        }

        [Fact]
        public void ShouldTranscendDataFromAnotherException()
        {
            // given
            string randomMessage = GetRandomMessage();
            string exceptionMessage = randomMessage;
            var exception = new Exception(exceptionMessage);

            Dictionary<string, List<string>> randomData =
                CreateRandomDictionary();

            Dictionary<string, List<string>> exceptionData =
                randomData;

            Dictionary<string, List<string>> expectedExceptionData =
                exceptionData;

            foreach (string key in exceptionData.Keys)
            {
                exception.Data.Add(key, randomData[key]);
            }

            // when
            var xeption = new Xeption(exception, exception.Data);

            // then

            foreach (string key in expectedExceptionData.Keys)
            {
                xeption.Data[key].Should().BeEquivalentTo(expectedExceptionData[key]);
            }

            xeption.InnerException.Should().BeEquivalentTo(exception);
            xeption.Message.Should().BeEquivalentTo(exception.Message);
        }

        [Fact]
        public void ShouldTranscendDataFromAnotherExceptionWithCustomMessage()
        {
            // given
            string randomMessage = GetRandomMessage();
            string customMessage = randomMessage;
            string expectedMessage = customMessage;
            var exception = new Exception();

            Dictionary<string, List<string>> randomData =
                CreateRandomDictionary();

            Dictionary<string, List<string>> exceptionData =
                randomData;

            Dictionary<string, List<string>> expectedExceptionData =
                exceptionData;

            foreach (string key in exceptionData.Keys)
            {
                exception.Data.Add(key, randomData[key]);
            }

            // when
            var xeption = new Xeption(customMessage, exception, exception.Data);

            // then

            foreach (string key in expectedExceptionData.Keys)
            {
                xeption.Data[key].Should().BeEquivalentTo(expectedExceptionData[key]);
            }

            xeption.InnerException.Should().BeEquivalentTo(exception);
            xeption.Message.Should().BeEquivalentTo(expectedMessage);
        }

        [Fact]
        public void ShouldAppendListOfKeyValuesWhenUpsertDataListIsCalledAfterAddData()
        {
            string keyName = "Name";
            string textRandomValidationMessage1 = GetRandomMessage();
            string textRandomValidationMessage2 = GetRandomMessage();

            // given
            var xeption = new Xeption();

            xeption.AddData(
                key: keyName,
                values: textRandomValidationMessage1);

            xeption.UpsertDataList(
                key: keyName,
                value: textRandomValidationMessage2);

            Dictionary<string, List<string>> randomDictionary = new Dictionary<string, List<string>>
            {
                { keyName, new List<string> { textRandomValidationMessage1, textRandomValidationMessage2 } }
            };

            Dictionary<string, List<string>> expectedDictionary =
                randomDictionary;

            ICollectionDictionary actualDictionary = xeption.Data;

            // then
            foreach (string key in expectedDictionary.Keys)
            {
                actualDictionary[key].Should().BeEquivalentTo(expectedDictionary[key]);
            }
        }
    }
}