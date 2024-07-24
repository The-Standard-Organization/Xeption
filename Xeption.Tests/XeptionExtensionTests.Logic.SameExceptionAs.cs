// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Force.DeepCloner;
using Xunit;

namespace Xeptions.Tests
{
    public partial class XeptionExtensionTests
    {
        [Fact]
        public void ShouldReturnTrueIfExceptionsMatch()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedInnerException = new Xeption(message: randomMessage);

            expectedInnerException.AddData(
                key: GetRandomString(),
                values: GetRandomString());

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = expectedException.DeepClone();

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException);

            // then
            Assert.True(actualComparisonResult);
        }

        [Fact]
        public void ShouldReturnTrueIfExceptionsMatchWithEmptyErrorDetails()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedInnerException = new Xeption(message: randomMessage);

            expectedInnerException.AddData(
                key: GetRandomString(),
                values: GetRandomString());

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = expectedException.DeepClone();

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException, out string message);

            // then
            Assert.True(actualComparisonResult);
            message.Should().BeEmpty();
        }

        [Fact]
        public void ShouldReturnTrueIfBothExceptionsMatchOnNull()
        {
            // given
            Xeption expectedException = null;
            Xeption actualException = null;

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException);

            // then
            Assert.True(actualComparisonResult);
        }

        [Fact]
        public void ShouldReturnTrueIfBothExceptionsMatchOnNullWithEmptyErrorDetails()
        {
            // given
            Xeption expectedException = null;
            Xeption actualException = null;

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException, out string message);

            // then
            Assert.True(actualComparisonResult);
            message.Should().BeEmpty();
        }

        [Fact]
        public void ShouldReturnFalseIfExceptionsDontMatchOnType()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedInnerException = new Xeption(message: randomMessage);

            expectedInnerException.AddData(
                key: GetRandomString(),
                values: GetRandomString());

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new Exception(
                message: randomMessage,
                innerException: expectedInnerException);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException);

            // then
            Assert.False(actualComparisonResult);
        }

        [Fact]
        public void ShouldReturnFalseIfExceptionsDontMatchOnTypeWithErrorDetails()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedInnerException = new Xeption(message: randomMessage);

            expectedInnerException.AddData(
                key: GetRandomString(),
                values: GetRandomString());

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new Exception(
                message: randomMessage,
                innerException: expectedInnerException);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException, out string message);

            // then
            Assert.False(actualComparisonResult);
            message.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void ShouldReturnFalseIfInnerExceptionsDontMatchOnType()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerException = new Xeption(message: randomMessage);
            Exception actualInnerException = new Exception(message: randomMessage);

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException);

            // then
            Assert.False(actualComparisonResult);
        }

        [Fact]
        public void ShouldReturnFalseIfInnerExceptionsDontMatchOnTypeWithErrorDetails()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerException = new Xeption(message: randomMessage);
            Exception actualInnerException = new Exception(message: randomMessage);

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException, out string message);

            // then
            Assert.False(actualComparisonResult);
            message.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void ShouldReturnFalseIfActualInnerExceptionIsNullWhileExpectedInnerExceptionIsPresent()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerException = new Xeption(message: randomMessage);
            Exception actualInnerException = null;

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException);

            // then
            Assert.False(actualComparisonResult);
        }

        [Fact]
        public void ShouldReturnFalseIfActualInnerExceptionIsNullWhileExpectedInnerExceptionIsPresentWithErrorDetails()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerException = new Xeption(message: randomMessage);
            Exception actualInnerException = null;

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException, out string message);

            // then
            Assert.False(actualComparisonResult);
            message.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void ShouldReturnFalseIfExpectedExceptionIsNullWhileActualExceptionIsPresent()
        {
            // given
            string randomMessage = GetRandomString();
            Exception actualInnerException = new Exception(message: randomMessage);

            Xeption expectedException = null;

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException);

            // then
            Assert.False(actualComparisonResult);
        }

        [Fact]
        public void ShouldReturnFalseIfExpectedExceptionIsNullWhileActualExceptionIsPresentWithErrorDetails()
        {
            // given
            string randomMessage = GetRandomString();
            Exception actualInnerException = new Exception(message: randomMessage);

            Xeption expectedException = null;

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException, out string message);

            // then
            Assert.False(actualComparisonResult);
            message.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void ShouldReturnFalseIfActualExceptionIsNullWhileExpectedExceptionIsPresent()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerException = null;

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            Xeption actualException = null;

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException);

            // then
            Assert.False(actualComparisonResult);
        }

        [Fact]
        public void ShouldReturnFalseIfActualExceptionIsNullWhileExpectedExceptionIsPresentWithErrorDetails()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerException = null;

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            Xeption actualException = null;

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException, out string message);

            // then
            Assert.False(actualComparisonResult);
            message.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void ShouldReturnFalseIfExpectedInnerExceptionIsNullWhileActualInnerExceptionIsPresent()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerException = null;
            Exception actualInnerException = new Exception(message: randomMessage);

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException);

            // then
            Assert.False(actualComparisonResult);
        }

        [Fact]
        public void ShouldReturnFalseIfExpectedInnerExceptionIsNullWhileActualInnerExceptionIsPresentWithErrorDetails()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerException = null;
            Exception actualInnerException = new Exception(message: randomMessage);

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException, out string message);

            // then
            Assert.False(actualComparisonResult);
            message.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void ShouldReturnTrueIfOuterExceptionsMatchWithNullInnerExceptions()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerException = null;
            Xeption actualInnerException = null;

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException);

            // then
            Assert.True(actualComparisonResult);
        }

        [Fact]
        public void ShouldReturnTrueIfOuterExceptionsMatchWithNullInnerExceptionsWithNoErrorDetails()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerException = null;
            Xeption actualInnerException = null;

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException, out string message);

            // then
            Assert.True(actualComparisonResult);
            message.Should().BeNullOrEmpty();
        }

        [Fact]
        public void ShouldReturnFalseIfExceptionMessageDontMatch()
        {
            // given
            string randomExceptionMessage = GetRandomString();
            string innerExceptionMessage = randomExceptionMessage;
            string expectedExceptionMessage = GetRandomString();
            string actualExceptionMessage = GetRandomString();

            var innerException = new Xeption(
                message: innerExceptionMessage);

            var expectedException = new Xeption(
                message: expectedExceptionMessage,
                innerException: innerException);

            var actualException = new Xeption(
                message: actualExceptionMessage,
                innerException: innerException);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException);

            // then
            Assert.False(actualComparisonResult);
        }

        [Fact]
        public void ShouldReturnFalseIfExceptionMessageDontMatchWithErrorDetails()
        {
            // given
            string randomExceptionMessage = GetRandomString();
            string innerExceptionMessage = randomExceptionMessage;
            string expectedExceptionMessage = GetRandomString();
            string actualExceptionMessage = GetRandomString();

            var innerException = new Xeption(
                message: innerExceptionMessage);

            var expectedException = new Xeption(
                message: expectedExceptionMessage,
                innerException: innerException);

            var actualException = new Xeption(
                message: actualExceptionMessage,
                innerException: innerException);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException, out string message);

            // then
            Assert.False(actualComparisonResult);
            message.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void ShouldReturnFalseIfInnerExceptionMessageDontMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            string expectedInnerExceptionMessage = GetRandomString();
            string actualInnerExceptionMessage = GetRandomString();
            var expectedInnerException = new Xeption(message: expectedInnerExceptionMessage);
            var actualInnerException = new Xeption(message: actualInnerExceptionMessage);

            var expectedException = new Xeption(
                message: exceptionMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: exceptionMessage,
                innerException: actualInnerException);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException);

            // then
            Assert.False(actualComparisonResult);
        }

        [Fact]
        public void ShouldReturnFalseIfInnerExceptionMessageDontMatchWithErrorDetails()
        {
            // given
            string exceptionMessage = GetRandomString();
            string expectedInnerExceptionMessage = GetRandomString();
            string actualInnerExceptionMessage = GetRandomString();
            var expectedInnerException = new Xeption(message: expectedInnerExceptionMessage);
            var actualInnerException = new Xeption(message: actualInnerExceptionMessage);

            var expectedException = new Xeption(
                message: exceptionMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: exceptionMessage,
                innerException: actualInnerException);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException, out string message);

            // then
            Assert.False(actualComparisonResult);
            message.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void ShouldReturnFalseIfInnerExceptionDataDontMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            string expectedInnerExceptionDataKey = GetRandomString();
            string expectedInnerExceptionDataValue = GetRandomString();
            string actualInnerExceptionDataKey = GetRandomString();
            string actualInnerExceptionDataValue = GetRandomString();

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

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException);

            // then
            Assert.False(actualComparisonResult);
        }

        [Fact]
        public void ShouldReturnFalseIfInnerExceptionDataDontMatchWithErrorDetails()
        {
            // given
            string exceptionMessage = GetRandomString();
            string expectedInnerExceptionDataKey = GetRandomString();
            string expectedInnerExceptionDataValue = GetRandomString();
            string actualInnerExceptionDataKey = GetRandomString();
            string actualInnerExceptionDataValue = GetRandomString();

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

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException, out string message);

            // then
            Assert.False(actualComparisonResult);
            message.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void ShouldReturnFalseIfExceptionDataDontMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            string expectedExceptionDataKey = GetRandomString();
            string expectedExceptionDataValue = GetRandomString();
            string actualExceptionDataKey = GetRandomString();
            string actualExceptionDataValue = GetRandomString();

            var expectedException = new Xeption(message: exceptionMessage);
            var actualException = new Xeption(message: exceptionMessage);

            expectedException.AddData(
                key: expectedExceptionDataKey,
                values: expectedExceptionDataValue);

            actualException.AddData(
                key: actualExceptionDataKey,
                values: actualExceptionDataValue);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException);

            // then
            Assert.False(actualComparisonResult);
        }

        [Fact]
        public void ShouldReturnFalseIfExceptionDataDontMatchWithErrorDetails()
        {
            // given
            string exceptionMessage = GetRandomString();
            string expectedExceptionDataKey = GetRandomString();
            string expectedExceptionDataValue = GetRandomString();
            string actualExceptionDataKey = GetRandomString();
            string actualExceptionDataValue = GetRandomString();

            var expectedException = new Xeption(message: exceptionMessage);
            var actualException = new Xeption(message: exceptionMessage);

            expectedException.AddData(
                key: expectedExceptionDataKey,
                values: expectedExceptionDataValue);

            actualException.AddData(
                key: actualExceptionDataKey,
                values: actualExceptionDataValue);

            // when
            bool actualComparisonResult =
                expectedException.SameExceptionAs(actualException, out string message);

            // then
            Assert.False(actualComparisonResult);
            message.Should().NotBeNullOrWhiteSpace();
        }

        [Fact(DisplayName = "01.0 - Level 0 - SameExceptionAsShouldPassIfNullExceptionsMatch")]
        public void SameExceptionAsShouldPassIfNullExceptionsMatch()
        {
            // given
            Xeption expectedException = null;
            Xeption actualException = null;

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            // then
            Assert.True(result);
            Assert.True(String.IsNullOrWhiteSpace(actualMessage));
        }

        [Fact(DisplayName = "02.1 - Level 0 - SameExceptionAsShouldPassIfExceptionsMatch")]
        public void SameExceptionAsShouldPassIfExceptionsMatch()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedException = new Xeption(message: randomMessage);
            var actualException = new Xeption(message: randomMessage);

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            // then
            Assert.True(result);
            Assert.True(String.IsNullOrWhiteSpace(actualMessage));
        }

        [Fact(DisplayName = "02.2 - Level 0 - SameExceptionAsShouldFailIfExceptionsDontMatchOnType")]
        public void SameExceptionAsShouldFailIfExceptionsDontMatchOnType()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedException = new Xeption(message: randomMessage);
            var actualException = new Exception(message: randomMessage);

            string expectedMessage =
                $"Expected exception to be \"{expectedException.GetType().FullName}\", " +
                $"but found \"{actualException.GetType().FullName}\"";

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            //then
            Assert.False(result);
            actualMessage.Should().BeEquivalentTo(expectedMessage);
        }

        [Fact(DisplayName = "02.3 - Level 0 - SameExceptionAsShouldFailIfExceptionMessagesDontMatch")]
        public void SameExceptionAsShouldFailIfExceptionMessagesDontMatch()
        {
            // given
            var expectedException = new Xeption(message: GetRandomString());
            var actualException = new Xeption(message: GetRandomString());

            string expectedMessage =
                $"Expected exception message to be \"{expectedException.Message}\", " +
                $"but found \"{actualException.Message}\"";

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            //then
            Assert.False(result);
            actualMessage.Should().BeEquivalentTo(expectedMessage);
        }

        [Fact(DisplayName = "02.4 - Level 0 - SameExceptionAsShouldPassIfExceptionDataMatch")]
        public void SameExceptionAsShouldPassIfExceptionDataMatch()
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

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            // then
            Assert.True(result);
            Assert.True(String.IsNullOrWhiteSpace(actualMessage));
        }

        [Fact(DisplayName = "02.5 - Level 0 - SameExceptionAsShouldFailIfExceptionDataDontMatch")]
        public void SameExceptionAsShouldFailIfExceptionDataDontMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            string mutualKey = $"mutual-{GetRandomString()}";
            KeyValuePair<string, List<string>> expectedDataOne = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> expectedDataTwo = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> actualData = GenerateKeyValuePair(count: 1);

            KeyValuePair<string, List<string>> expectedDataSameKeyName =
                GenerateKeyValuePair(count: 1, keyName: mutualKey);

            KeyValuePair<string, List<string>> actualDataSameKeyName =
                GenerateKeyValuePair(count: 1, keyName: mutualKey);

            var expectedException = new Xeption(
                message: exceptionMessage);

            expectedException.AddData(
                key: expectedDataOne.Key,
                values: expectedDataOne.Value.ToArray());

            expectedException.AddData(
                key: expectedDataTwo.Key,
                values: expectedDataTwo.Value.ToArray());

            expectedException.AddData(
                key: expectedDataSameKeyName.Key,
                values: expectedDataSameKeyName.Value.ToArray());

            var actualException = new Xeption(
                message: exceptionMessage);

            actualException.AddData(
                key: actualDataSameKeyName.Key,
                values: actualDataSameKeyName.Value.ToArray());

            actualException.AddData(
                key: actualData.Key,
                values: actualData.Value.ToArray());

            var expectedMessage = new StringBuilder();
            expectedMessage.AppendLine($"Expected exception to:");

            expectedMessage.AppendLine(
                $"- have a data count of {expectedException.Data.Count}, " +
                $"but found {actualException.Data.Count}");

            expectedMessage.AppendLine(
                $"- NOT contain key \"{actualData.Key}\"");

            expectedMessage.AppendLine(
                $"- contain key \"{expectedDataOne.Key}\" with value(s) ['{expectedDataOne.Value[0]}']");

            expectedMessage.AppendLine(
                $"- contain key \"{expectedDataTwo.Key}\" with value(s) ['{expectedDataTwo.Value[0]}']");

            expectedMessage.AppendLine(
                $"- have key \"{mutualKey}\" with value(s) ['{expectedDataSameKeyName.Value[0]}'], " +
                $"but found value(s) ['{actualDataSameKeyName.Value[0]}']");

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            //then
            Assert.False(result);
            actualMessage.Should().BeEquivalentTo(expectedMessage.ToString().Trim());
        }

        [Fact(DisplayName = "03.1 - Level 0 - SameExceptionAsShouldPassIfInnerExceptionsMatch")]
        public void SameExceptionAsShouldPassIfInnerExceptionsMatchOnType()
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

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            // then
            Assert.True(result);
            Assert.True(String.IsNullOrWhiteSpace(actualMessage));
        }

        [Fact(DisplayName = "03.2 - Level 1 - SameExceptionAsShouldFailIfInnerExceptionsTypeDontMatch")]
        public void SameExceptionAsShouldFailIfInnerExceptionsTypeDontMatch()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerException = new Xeption(message: randomMessage);
            Exception actualInnerException = new Exception(message: randomMessage);

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            string expectedMessage =
                $"Expected inner exception (level 1) to be \"{expectedInnerException.GetType().FullName}\", " +
                $"but found \"{actualInnerException.GetType().FullName}\"";

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            //then
            Assert.False(result);
            actualMessage.Should().BeEquivalentTo(expectedMessage.ToString().Trim());
        }

        [Fact(DisplayName = "03.3 - Level 1 - SameExceptionAsShouldFailIfInnerExceptionMessageDontMatch")]
        public void SameExceptionAsShouldFailIfInnerExceptionMessageDontMatch()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedInnerException = new Xeption(message: GetRandomString());
            var actualInnerException = new Xeption(message: GetRandomString());

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            string expectedMessage =
                $"Expected inner exception (level 1) message to be \"{expectedInnerException.Message}\", " +
                $"but found \"{actualInnerException.Message}\"";

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            //then
            Assert.False(result);
            actualMessage.Should().BeEquivalentTo(expectedMessage.ToString().Trim());
        }

        [Fact(DisplayName = "03.4 - Level 1 - SameExceptionAsShouldPassIfInnerExceptionDataMatch")]
        public void SameExceptionAsShouldPassIfInnerExceptionDataMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            KeyValuePair<string, List<string>> randomData = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> expectedData = randomData.DeepClone();
            KeyValuePair<string, List<string>> actualData = randomData.DeepClone();

            var expectedInnerException = new Xeption(
                message: exceptionMessage);

            expectedInnerException.AddData(
                key: expectedData.Key,
                values: expectedData.Value.ToArray());

            var actualInnerException = new Xeption(
                message: exceptionMessage);

            actualInnerException.AddData(
                key: actualData.Key,
                values: actualData.Value.ToArray());

            var expectedException = new Xeption(
                message: exceptionMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: exceptionMessage,
                innerException: actualInnerException);

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            // then
            Assert.True(result);
            Assert.True(String.IsNullOrWhiteSpace(actualMessage));
        }

        [Fact(DisplayName = "03.5 - Level 1 - SameExceptionAsShouldFailIfInnerExceptionDataDontMatch")]
        public void SameExceptionAsShouldFailIfInnerExceptionDataDontMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            string mutualKey = GetRandomString();
            KeyValuePair<string, List<string>> expectedDataOne = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> expectedDataTwo = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> actualData = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> expectedDataSameKeyName = GenerateKeyValuePair(count: 1, mutualKey);
            KeyValuePair<string, List<string>> actualDataSameKeyName = GenerateKeyValuePair(count: 1, mutualKey);

            var expectedInnerException = new Xeption(
                message: exceptionMessage);

            expectedInnerException.AddData(
                key: expectedDataOne.Key,
                values: expectedDataOne.Value.ToArray());

            expectedInnerException.AddData(
                key: expectedDataTwo.Key,
                values: expectedDataTwo.Value.ToArray());

            expectedInnerException.AddData(
                key: expectedDataSameKeyName.Key,
                values: expectedDataSameKeyName.Value.ToArray());

            var actualInnerException = new Xeption(
                message: exceptionMessage);

            actualInnerException.AddData(
                key: actualDataSameKeyName.Key,
                values: actualDataSameKeyName.Value.ToArray());

            actualInnerException.AddData(
                key: actualData.Key,
                values: actualData.Value.ToArray());

            var expectedMessage = new StringBuilder();
            expectedMessage.AppendLine($"Expected inner exception (level 1) to:");

            expectedMessage.AppendLine(
                $"- have a data count of {expectedInnerException.Data.Count}, " +
                $"but found {actualInnerException.Data.Count}");

            expectedMessage.AppendLine(
                $"- NOT contain key \"{actualData.Key}\"");

            expectedMessage.AppendLine(
                $"- contain key \"{expectedDataOne.Key}\" with value(s) ['{expectedDataOne.Value[0]}']");

            expectedMessage.AppendLine(
                $"- contain key \"{expectedDataTwo.Key}\" with value(s) ['{expectedDataTwo.Value[0]}']");

            expectedMessage.AppendLine(
                $"- have key \"{mutualKey}\" with value(s) ['{expectedDataSameKeyName.Value[0]}'], " +
                $"but found value(s) ['{actualDataSameKeyName.Value[0]}']");

            var expectedException = new Xeption(
                message: exceptionMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: exceptionMessage,
                innerException: actualInnerException);

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            //then
            Assert.False(result);
            actualMessage.Should().BeEquivalentTo(expectedMessage.ToString().Trim());
        }

        [Fact(DisplayName = "04.1 - Level 2 - SameExceptionAsShouldPassIfInnerInnerExceptionsMatch")]
        public void SameExceptionAsShouldPassIfInnerInnerExceptionsMatch()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerInnerException = new Xeption(message: randomMessage);
            Xeption actualInnerInnerException = new Xeption(message: randomMessage);

            Xeption expectedInnerException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerInnerException);

            Xeption actualInnerException = new Xeption(
                message: randomMessage,
                innerException: actualInnerInnerException);

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            // then
            Assert.True(result);
            Assert.True(String.IsNullOrWhiteSpace(actualMessage));
        }

        [Fact(DisplayName = "04.2 - Level 2 - SameExceptionAsShouldFailIfInnerInnerExceptionsTypeDontMatch")]
        public void SameExceptionAsShouldFailIfInnerInnerExceptionsTypeDontMatch()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerInnerException = new Xeption(message: randomMessage);
            Exception actualInnerInnerException = new Exception(message: randomMessage);

            Xeption expectedInnerException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerInnerException);

            Xeption actualInnerException = new Xeption(
                message: randomMessage,
                innerException: actualInnerInnerException);

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            string expectedMessage =
                $"Expected inner exception (level 2) to be \"{expectedInnerInnerException.GetType().FullName}\", " +
                $"but found \"{actualInnerInnerException.GetType().FullName}\"";

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            //then
            Assert.False(result);
            actualMessage.Should().BeEquivalentTo(expectedMessage.ToString().Trim());
        }

        [Fact(DisplayName = "04.3 - Level 2 - SameExceptionAsShouldFailIfInnerInnerExceptionMessageDontMatch")]
        public void SameExceptionAsShouldFailIfInnerInnerExceptionMessageDontMatch()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedInnerInnerException = new Xeption(message: GetRandomString());
            var actualInnerInnerException = new Xeption(message: GetRandomString());

            Xeption expectedInnerException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerInnerException);

            Xeption actualInnerException = new Xeption(
                message: randomMessage,
                innerException: actualInnerInnerException);

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            string expectedMessage =
                $"Expected inner exception (level 2) message to be \"{expectedInnerInnerException.Message}\", " +
                $"but found \"{actualInnerInnerException.Message}\"";

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            //then
            Assert.False(result);
            actualMessage.Should().BeEquivalentTo(expectedMessage.ToString().Trim());
        }

        [Fact(DisplayName = "04.4 - Level 2 - SameExceptionAsShouldPassIfInnerInnerExceptionDataMatch")]
        public void SameExceptionAsShouldPassIfInnerInnerExceptionDataMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            KeyValuePair<string, List<string>> randomData = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> expectedData = randomData.DeepClone();
            KeyValuePair<string, List<string>> actualData = randomData.DeepClone();

            var expectedInnerInnerException = new Xeption(
                message: exceptionMessage);

            expectedInnerInnerException.AddData(
                key: expectedData.Key,
                values: expectedData.Value.ToArray());

            var actualInnerInnerException = new Xeption(
                message: exceptionMessage);

            actualInnerInnerException.AddData(
                key: actualData.Key,
                values: actualData.Value.ToArray());

            Xeption expectedInnerException = new Xeption(
                message: exceptionMessage,
                innerException: expectedInnerInnerException);

            Xeption actualInnerException = new Xeption(
                message: exceptionMessage,
                innerException: actualInnerInnerException);

            var expectedException = new Xeption(
                message: exceptionMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: exceptionMessage,
                innerException: actualInnerException);

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            // then
            Assert.True(result);
            Assert.True(String.IsNullOrWhiteSpace(actualMessage));
        }

        [Fact(DisplayName = "04.5 - Level 2 - SameExceptionAsShouldFailIfInnerInnerExceptionDataDontMatch")]
        public void SameExceptionAsShouldFailIfAggregateInnerInnerExceptionDataDontMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            string mutualKey = GetRandomString();
            KeyValuePair<string, List<string>> expectedDataOne = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> expectedDataTwo = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> actualData = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> expectedDataSameKeyName = GenerateKeyValuePair(count: 1, mutualKey);
            KeyValuePair<string, List<string>> actualDataSameKeyName = GenerateKeyValuePair(count: 1, mutualKey);

            var expectedInnerInnerException = new Xeption(
                message: exceptionMessage);

            expectedInnerInnerException.AddData(
                key: expectedDataOne.Key,
                values: expectedDataOne.Value.ToArray());

            expectedInnerInnerException.AddData(
                key: expectedDataTwo.Key,
                values: expectedDataTwo.Value.ToArray());

            expectedInnerInnerException.AddData(
                key: expectedDataSameKeyName.Key,
                values: expectedDataSameKeyName.Value.ToArray());

            var actualInnerInnerException = new Xeption(
                message: exceptionMessage);

            actualInnerInnerException.AddData(
                key: actualDataSameKeyName.Key,
                values: actualDataSameKeyName.Value.ToArray());

            actualInnerInnerException.AddData(
                key: actualData.Key,
                values: actualData.Value.ToArray());

            Xeption expectedInnerException = new Xeption(
                message: exceptionMessage,
                innerException: expectedInnerInnerException);

            Xeption actualInnerException = new Xeption(
                message: exceptionMessage,
                innerException: actualInnerInnerException);

            var expectedMessage = new StringBuilder();
            expectedMessage.AppendLine($"Expected inner exception (level 2) to:");

            expectedMessage.AppendLine(
                $"- have a data count of {expectedInnerInnerException.Data.Count}, " +
                $"but found {actualInnerInnerException.Data.Count}");

            expectedMessage.AppendLine(
                $"- NOT contain key \"{actualData.Key}\"");

            expectedMessage.AppendLine(
                $"- contain key \"{expectedDataOne.Key}\" with value(s) ['{expectedDataOne.Value[0]}']");

            expectedMessage.AppendLine(
                $"- contain key \"{expectedDataTwo.Key}\" with value(s) ['{expectedDataTwo.Value[0]}']");

            expectedMessage.AppendLine(
                $"- have key \"{mutualKey}\" with value(s) ['{expectedDataSameKeyName.Value[0]}'], " +
                $"but found value(s) ['{actualDataSameKeyName.Value[0]}']");

            var expectedException = new Xeption(
                message: exceptionMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: exceptionMessage,
                innerException: actualInnerException);

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            //then
            Assert.False(result);
            actualMessage.Should().BeEquivalentTo(expectedMessage.ToString().Trim());
        }

        [Fact(DisplayName = "05.0 - Aggregate - SameExceptionAsShouldPassIfNullExceptionsMatch")]
        public void AggregateSameExceptionAsShouldPassIfNullExceptionsMatch()
        {
            // given
            AggregateException expectedException = null;
            AggregateException actualException = null;

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            // then
            Assert.True(result);
            Assert.True(String.IsNullOrWhiteSpace(actualMessage));
        }

        [Fact(DisplayName = "06.1 - Aggregate - SameExceptionAsShouldPassIfExceptionsMatch")]
        public void AggregateSameExceptionAsShouldPassIfExceptionsMatch()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedException = new AggregateException(message: randomMessage);
            var actualException = new AggregateException(message: randomMessage);

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            // then
            Assert.True(result);
            Assert.True(String.IsNullOrWhiteSpace(actualMessage));
        }

        [Fact(DisplayName = "06.2 - Aggregate - SameExceptionAsShouldPassIfExceptionDataMatch")]
        public void AggregateSameExceptionAsShouldPassIfExceptionDataMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            KeyValuePair<string, List<string>> randomData = GenerateKeyValuePair(count: 2);
            KeyValuePair<string, List<string>> expectedData = randomData.DeepClone();
            KeyValuePair<string, List<string>> actualData = randomData.DeepClone();

            var expectedException = new AggregateException(
                message: exceptionMessage);

            expectedException.Data.Add(
                key: expectedData.Key,
                value: expectedData.Value.ToArray());

            var actualException = new AggregateException(
                message: exceptionMessage);

            actualException.Data.Add(
                key: actualData.Key,
                value: actualData.Value.ToArray());

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            // then
            Assert.True(result);
            Assert.True(String.IsNullOrWhiteSpace(actualMessage));
        }

        [Fact(DisplayName = "06.3 - Aggregate - SameExceptionAsShouldFailIfExceptionDataDontMatch")]
        public void AggregateSameExceptionAsShouldFailIfExceptionDataDontMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            string mutualKey = $"mutual-{GetRandomString()}";
            KeyValuePair<string, List<string>> expectedDataOne = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> expectedDataTwo = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> actualData = GenerateKeyValuePair(count: 1);

            KeyValuePair<string, List<string>> expectedDataSameKeyName =
                GenerateKeyValuePair(count: 1, keyName: mutualKey);

            KeyValuePair<string, List<string>> actualDataSameKeyName =
                GenerateKeyValuePair(count: 1, keyName: mutualKey);

            var expectedException = new AggregateException(
                message: exceptionMessage);

            expectedException.Data.Add(
                key: expectedDataOne.Key,
                value: expectedDataOne.Value.ToArray());

            expectedException.Data.Add(
                key: expectedDataTwo.Key,
                value: expectedDataTwo.Value.ToArray());

            expectedException.Data.Add(
                key: expectedDataSameKeyName.Key,
                value: expectedDataSameKeyName.Value.ToArray());

            var actualException = new AggregateException(
                message: exceptionMessage);

            actualException.Data.Add(
                key: actualDataSameKeyName.Key,
                value: actualDataSameKeyName.Value.ToArray());

            actualException.Data.Add(
                key: actualData.Key,
                value: actualData.Value.ToArray());

            var expectedMessage = new StringBuilder();
            expectedMessage.AppendLine($"Expected exception to:");

            expectedMessage.AppendLine(
                $"- have a data count of {expectedException.Data.Count}, " +
                $"but found {actualException.Data.Count}");

            expectedMessage.AppendLine(
                $"- NOT contain key \"{actualData.Key}\"");

            expectedMessage.AppendLine(
                $"- contain key \"{expectedDataOne.Key}\" with value(s) ['{expectedDataOne.Value[0]}']");

            expectedMessage.AppendLine(
                $"- contain key \"{expectedDataTwo.Key}\" with value(s) ['{expectedDataTwo.Value[0]}']");

            expectedMessage.AppendLine(
                $"- have key \"{mutualKey}\" with value(s) ['{expectedDataSameKeyName.Value[0]}'], " +
                $"but found value(s) ['{actualDataSameKeyName.Value[0]}']");

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            //then
            Assert.False(result);
            actualMessage.Should().BeEquivalentTo(expectedMessage.ToString().Trim());
        }

        [Fact(DisplayName = "07.1 - Aggregate - SameExceptionAsShouldPassIfInnerExceptionMatch")]
        public void AggregateSameExceptionAsShouldPassIfInnerExceptionMatchOnType()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerException = new Xeption(message: randomMessage);
            Xeption actualInnerException = new Xeption(message: randomMessage);

            var expectedException = new AggregateException(
                message: randomMessage,
                innerException: expectedInnerException);

            var actualException = new AggregateException(
                message: randomMessage,
                innerException: actualInnerException);

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            // then
            Assert.True(result);
            Assert.True(String.IsNullOrWhiteSpace(actualMessage));
        }

        [Fact(DisplayName = "07.2 - Aggregate - Level 0 - SameExceptionAsShouldFailIfInnerExceptionsTypeDontMatch")]
        public void AggregateSameExceptionAsShouldFailIfInnerExceptionsTypeDontMatch()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerException = new Xeption(message: randomMessage);
            Exception actualInnerException = new Exception(message: randomMessage);

            var expectedException = new AggregateException(
                message: randomMessage,
                innerExceptions: expectedInnerException);

            var actualException = new AggregateException(
                message: randomMessage,
                innerExceptions: actualInnerException);

            var expectedMessage = new StringBuilder();
            expectedMessage.AppendLine($"Aggregate exception differences:");

            expectedMessage.AppendLine($"* Difference in inner exception at index[0] - Expected exception " +
                $"to be \"{expectedInnerException.GetType().FullName}\", " +
                $"but found \"{actualInnerException.GetType().FullName}\"");

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            //then
            Assert.False(result);
            actualMessage.Should().BeEquivalentTo(expectedMessage.ToString().Trim());
        }

        [Fact(DisplayName = "07.3 - Aggregate - Level 0 - SameExceptionAsShouldFailIfInnerExceptionsMessageDontMatch")]
        public void AggregateSameExceptionAsShouldFailIfInnerExceptionsMessageDontMatch()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedInnerException = new Xeption(message: GetRandomString());
            var actualInnerException = new Xeption(message: GetRandomString());

            var expectedException = new AggregateException(
                message: randomMessage,
                innerExceptions: expectedInnerException);

            var actualException = new AggregateException(
                message: randomMessage,
                innerExceptions: actualInnerException);

            var expectedMessage = new StringBuilder();
            expectedMessage.AppendLine($"Aggregate exception differences:");

            expectedMessage.AppendLine(
                $"* Difference in inner exception at index[0] - Expected exception message to be " +
                $"\"{expectedInnerException.Message}\", " +
                $"but found \"{actualInnerException.Message}\"");

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            //then
            Assert.False(result);
            actualMessage.Should().BeEquivalentTo(expectedMessage.ToString().Trim());
        }

        [Fact(DisplayName = "07.4 - Aggregate - Level 0 - SameExceptionAsShouldPassIfInnerExceptionDataMatch")]
        public void AggregateSameExceptionAsShouldPassIfInnerExceptionsDataMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            KeyValuePair<string, List<string>> randomData = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> expectedData = randomData.DeepClone();
            KeyValuePair<string, List<string>> actualData = randomData.DeepClone();

            var expectedInnerException = new Xeption(
                message: exceptionMessage);

            expectedInnerException.AddData(
                key: expectedData.Key,
                values: expectedData.Value.ToArray());

            var actualInnerException = new Xeption(
                message: exceptionMessage);

            actualInnerException.AddData(
                key: actualData.Key,
                values: actualData.Value.ToArray());

            var expectedException = new AggregateException(
                message: exceptionMessage,
                innerExceptions: expectedInnerException);

            var actualException = new AggregateException(
                message: exceptionMessage,
                innerExceptions: actualInnerException);

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            // then
            Assert.True(result);
            Assert.True(String.IsNullOrWhiteSpace(actualMessage));
        }

        [Fact(DisplayName = "07.5 - Aggregate - Level 0 - SameExceptionAsShouldFailIfInnerExceptionDataDontMatch")]
        public void AggregateSameExceptionAsShouldFailIfInnerExceptionsDataDontMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            string mutualKey = GetRandomString();
            KeyValuePair<string, List<string>> expectedDataOne = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> expectedDataTwo = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> actualData = GenerateKeyValuePair(count: 1);
            KeyValuePair<string, List<string>> expectedDataSameKeyName = GenerateKeyValuePair(count: 1, mutualKey);
            KeyValuePair<string, List<string>> actualDataSameKeyName = GenerateKeyValuePair(count: 1, mutualKey);

            var expectedInnerException = new Xeption(
                message: exceptionMessage);

            expectedInnerException.AddData(
                key: expectedDataOne.Key,
                values: expectedDataOne.Value.ToArray());

            expectedInnerException.AddData(
                key: expectedDataTwo.Key,
                values: expectedDataTwo.Value.ToArray());

            expectedInnerException.AddData(
                key: expectedDataSameKeyName.Key,
                values: expectedDataSameKeyName.Value.ToArray());

            var actualInnerException = new Xeption(
                message: exceptionMessage);

            actualInnerException.AddData(
                key: actualDataSameKeyName.Key,
                values: actualDataSameKeyName.Value.ToArray());

            actualInnerException.AddData(
                key: actualData.Key,
                values: actualData.Value.ToArray());

            var expectedMessage = new StringBuilder();
            expectedMessage.AppendLine($"Aggregate exception differences:");
            expectedMessage.AppendLine($"* Difference in inner exception at index[0] - Expected exception to:");

            expectedMessage.AppendLine(
                $"- have a data count of {expectedInnerException.Data.Count}, " +
                $"but found {actualInnerException.Data.Count}");

            expectedMessage.AppendLine(
                $"- NOT contain key \"{actualData.Key}\"");

            expectedMessage.AppendLine(
                $"- contain key \"{expectedDataOne.Key}\" with value(s) ['{expectedDataOne.Value[0]}']");

            expectedMessage.AppendLine(
                $"- contain key \"{expectedDataTwo.Key}\" with value(s) ['{expectedDataTwo.Value[0]}']");

            expectedMessage.AppendLine(
                $"- have key \"{mutualKey}\" with value(s) ['{expectedDataSameKeyName.Value[0]}'], " +
                $"but found value(s) ['{actualDataSameKeyName.Value[0]}']");

            var expectedException = new AggregateException(
                message: exceptionMessage,
                innerExceptions: expectedInnerException);

            var actualException = new AggregateException(
                message: exceptionMessage,
                innerExceptions: actualInnerException);

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            //then
            Assert.False(result);
            actualMessage.Should().BeEquivalentTo(expectedMessage.ToString().Trim());
        }

        [Fact(DisplayName = "08.1 - Aggregate - Level 1 - SameExceptionAsShouldPassIfInnerInnerExceptionsMatch")]
        public void AggregateSameExceptionAsShouldPassIfInnerInnerExceptionsMatch()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerInnerException = new Xeption(message: randomMessage);
            Xeption actualInnerInnerException = new Xeption(message: randomMessage);

            Xeption expectedInnerException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerInnerException);

            Xeption actualInnerException = new Xeption(
                message: randomMessage,
                innerException: actualInnerInnerException);

            var expectedException = new AggregateException(
                message: randomMessage,
                innerExceptions: expectedInnerException);

            var actualException = new AggregateException(
                message: randomMessage,
                innerExceptions: actualInnerException);

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            // then
            Assert.True(result);
            Assert.True(String.IsNullOrWhiteSpace(actualMessage));
        }

        [Fact(DisplayName = "08.2 - Aggregate - Level 1 - SameExceptionAsShouldFailIfInnerInnerExceptionsTypeDontMatch")]
        public void AggregateSameExceptionAsShouldFailIfInnerInnerExceptionsTypeDontMatch()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerInnerException = new Xeption(message: randomMessage);
            Exception actualInnerInnerException = new Exception(message: randomMessage);

            Xeption expectedInnerException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerInnerException);

            Xeption actualInnerException = new Xeption(
                message: randomMessage,
                innerException: actualInnerInnerException);

            var expectedException = new AggregateException(
                message: randomMessage,
                innerExceptions: expectedInnerException);

            var actualException = new AggregateException(
                message: randomMessage,
                innerExceptions: actualInnerException);

            var expectedMessage = new StringBuilder();
            expectedMessage.AppendLine($"Aggregate exception differences:");
            expectedMessage.AppendLine($"* Difference in inner exception at index[0] - " +
                $"Expected inner exception (level 1) to be \"{expectedInnerInnerException.GetType().FullName}\", " +
                $"but found \"{actualInnerInnerException.GetType().FullName}\"");

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            //then
            Assert.False(result);
            actualMessage.Should().BeEquivalentTo(expectedMessage.ToString().Trim());
        }

        [Fact(DisplayName = "08.3 - Aggregate - Level 1 - SameExceptionAsShouldFailIfInnerInnerExceptionMessageDontMatch")]
        public void AggregateSameExceptionAsShouldFailIfInnerInnerExceptionsMessageDontMatch()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedInnerInnerException = new Xeption(message: GetRandomString());
            var actualInnerInnerException = new Xeption(message: GetRandomString());

            Xeption expectedInnerException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerInnerException);

            Xeption actualInnerException = new Xeption(
                message: randomMessage,
                innerException: actualInnerInnerException);

            var expectedException = new AggregateException(
                message: randomMessage,
                innerExceptions: expectedInnerException);

            var actualException = new AggregateException(
                message: randomMessage,
                innerExceptions: actualInnerException);

            var expectedMessage = new StringBuilder();
            expectedMessage.AppendLine($"Aggregate exception differences:");
            expectedMessage.AppendLine($"* Difference in inner exception at index[0] - " +
                $"Expected inner exception (level 1) message to be \"{expectedInnerInnerException.Message}\", " +
                $"but found \"{actualInnerInnerException.Message}\"");

            // when
            bool result = actualException.SameExceptionAs(expectedException, out string actualMessage);

            //then
            Assert.False(result);
            actualMessage.Should().BeEquivalentTo(expectedMessage.ToString().Trim());
        }
    }
}
