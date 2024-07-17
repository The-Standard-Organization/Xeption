// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using FluentAssertions;
using Force.DeepCloner;
using Xunit;
using Xunit.Sdk;

namespace Xeptions.Tests
{
    public partial class XeptionAssertionTests
    {
        [Fact(DisplayName = "01.0 - BeEquivalentToShouldPassIfNullExceptionsMatchOnType")]
        public void BeEquivalentToShouldPassIfNullExceptionsMatchOnType()
        {
            // given
            Xeption expectedException = null;
            Xeption actualException = null;

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact(DisplayName = "02.1 - BeEquivalentToShouldPassIfExceptionsMatchOnType")]
        public void BeEquivalentToShouldPassIfExceptionsMatchOnType()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedException = new Xeption(message: randomMessage);
            var actualException = new Xeption(message: randomMessage);

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact(DisplayName = "02.2 - BeEquivalentToShouldFailIfExceptionsDontMatchOnType")]
        public void BeEquivalentToShouldFailIfExceptionsDontMatchOnType()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedException = new Xeption(message: randomMessage);
            var actualException = new Exception(message: randomMessage);

            string expectedMessage =
                "Expected exception type to be \"Xeptions.Xeption\", but found \"System.Exception\".";

            // when
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().Contain(expectedMessage);
        }

        [Fact(DisplayName = "03.1 - BeEquivalentToShouldPassIfExceptionMessagesMatch")]
        public void BeEquivalentToShouldPassIfExceptionMessagesMatch()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedException = new Xeption(message: randomMessage);
            var actualException = new Xeption(message: randomMessage);

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact(DisplayName = "03.2 - BeEquivalentToShouldFailIfExceptionMessagesDontMatch")]
        public void BeEquivalentToShouldFailIfExceptionMessagesDontMatch()
        {
            // given
            var expectedException = new Xeption(message: GetRandomString());
            var actualException = new Xeption(message: GetRandomString());

            string expectedMessage =
                $"Expected exception message to be \"{expectedException.Message}\", " +
                $"but found \"{actualException.Message}\"";

            // when
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().Contain(expectedMessage);
        }

        [Fact(DisplayName = "04.1 - BeEquivalentToShouldPassIfExceptionDataMatch")]
        public void BeEquivalentToShouldPassIfExceptionDataMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            KeyValuePair<string, List<string>> randomData = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> expectedData = randomData.DeepClone();
            KeyValuePair<string, List<string>> actualData = randomData.DeepClone();

            var expectedException = new Xeption(
                message: exceptionMessage);

            expectedException.AddData(
                key: expectedData.Key,
                values: expectedData.Value.ToArray());

            var actualException = new Xeption(
                message: exceptionMessage);

            actualException.AddData(
                key: actualData.Key,
                values: actualData.Value.ToArray());

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact(DisplayName = "04.2 - BeEquivalentToShouldFailIfExceptionDataDontMatch")]
        public void BeEquivalentToShouldFailIfExceptionDataDontMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            string mutualKey = GetRandomString();
            KeyValuePair<string, List<string>> expectedDataOne = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> expectedDataTwo = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> actualData = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> expectedDataSameKeyName = GenerateKeyValuePair(count: 1, mutualKey);
            KeyValuePair<string, List<string>> actualDataSameKeyName = GenerateKeyValuePair(count: 1, mutualKey);

            var expectedException = new Xeption(
                message: exceptionMessage);

            expectedException.AddData(
                key: expectedDataTwo.Key,
                values: expectedDataTwo.Value.ToArray());

            expectedException.AddData(
                key: expectedDataOne.Key,
                values: expectedDataOne.Value.ToArray());

            expectedException.AddData(
                key: expectedDataSameKeyName.Key,
                values: expectedDataSameKeyName.Value.ToArray());

            var actualException = new Xeption(
                message: exceptionMessage);

            actualException.AddData(
                key: actualData.Key,
                values: actualData.Value.ToArray());

            actualException.AddData(
                key: actualDataSameKeyName.Key,
                values: actualDataSameKeyName.Value.ToArray());

            string titleMessage =
                $"Expected exception with type '{expectedException.GetType().FullName}' " +
                $"and message '{expectedException.Message}' to:";

            string errorCountMessage =
                $"- have an expected data item count to be {expectedException.Data.Count}, " +
                $"but found {actualException.Data.Count}.";

            string messageOne =
                $"- contain key '{expectedDataOne.Key}', but it was not found.";

            string messageTwo =
                $"- contain key '{expectedDataTwo.Key}', but it was not found.";

            string messageThree =
                $"- not contain key '{actualData.Key}'.";

            string messageFour =
                $"- not contain key '{actualData.Key}'.";

            // when
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().Contain(errorCountMessage);
            actualError.Message.Should().Contain(messageOne);
            actualError.Message.Should().Contain(messageTwo);
            actualError.Message.Should().Contain(messageThree);
            actualError.Message.Should().Contain(messageFour);
        }

        // TODO: Remove old tests below at the end of the refactoring

        [Fact]
        public void BeEquivalentToShouldPassIfInnerExceptionsMatchOnType()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerException = new Xeption(message: randomMessage);
            Xeption actualInnerException = new Xeption(message: randomMessage);

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact]
        public void BeEquivalentToShouldPassIfInnerExceptionMessagesMatch()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedException = new Xeption(message: randomMessage);
            var actualException = new Xeption(message: randomMessage);

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact]
        public void ShouldPassIfInnerExceptionDataCountMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            var expectedInnerException = new Xeption(message: exceptionMessage);
            var actualInnerException = new Xeption(message: exceptionMessage);

            var expectedException = new Xeption(
                message: exceptionMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: exceptionMessage,
                innerException: actualInnerException);

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact]
        public void ShouldPassIfInnerExceptionDataMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            string randomKey = GetRandomString();
            string randomValue = GetRandomString();
            string expectedInnerExceptionDataKey = randomKey;
            string expectedInnerExceptionDataValue = randomValue;
            string actualInnerExceptionDataKey = randomKey;
            string actualInnerExceptionDataValue = randomValue;
            var expectedInnerException = new Xeption(message: exceptionMessage);
            var actualInnerException = new Xeption(message: exceptionMessage);

            expectedInnerException.AddData(
                key: expectedInnerExceptionDataKey,
                values: expectedInnerExceptionDataValue);

            actualInnerException.AddData(
                key: actualInnerExceptionDataKey,
                values: actualInnerExceptionDataValue);

            var expectedException = new Xeption(
                message: exceptionMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: exceptionMessage,
                innerException: actualInnerException);

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }
    }
}
