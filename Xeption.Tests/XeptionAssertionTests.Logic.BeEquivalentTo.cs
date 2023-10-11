// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using FluentAssertions;
using Xunit;

namespace Xeptions.Tests
{
    public partial class XeptionAssertionTests
    {
        [Fact]
        public void BeEquivalentToShouldPassIfExceptionsMatchOnType()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedException = new Xeption(message: randomMessage);
            var actualException = new Xeption(message: randomMessage);

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact]
        public void BeEquivalentToShouldPassIfNullExceptionsMatchOnType()
        {
            // given
            Xeption expectedException = null;
            Xeption actualException = null;

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact]
        public void BeEquivalentToShouldPassIfExceptionMessagesMatch()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedException = new Xeption(message: randomMessage);
            var actualException = new Xeption(message: randomMessage);

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

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
