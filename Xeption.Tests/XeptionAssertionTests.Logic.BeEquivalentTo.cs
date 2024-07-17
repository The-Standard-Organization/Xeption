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
