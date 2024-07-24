// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Force.DeepCloner;
using Xunit;
using Xunit.Sdk;

namespace Xeptions.Tests
{
    public partial class XeptionAssertionTests
    {
        [Fact(DisplayName = "01.0 - Level 0 - BeEquivalentToShouldPassIfNullExceptionsMatch")]
        public void BeEquivalentToShouldPassIfNullExceptionsMatch()
        {
            // given
            Xeption expectedException = null;
            Xeption actualException = null;

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact(DisplayName = "02.1 - Level 0 - BeEquivalentToShouldPassIfExceptionsMatch")]
        public void BeEquivalentToShouldPassIfExceptionsMatch()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedException = new Xeption(message: randomMessage);
            var actualException = new Xeption(message: randomMessage);

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact(DisplayName = "02.2 - Level 0 - BeEquivalentToShouldFailIfExceptionsDontMatchOnType")]
        public void BeEquivalentToShouldFailIfExceptionsDontMatchOnType()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedException = new Xeption(message: randomMessage);
            var actualException = new Exception(message: randomMessage);

            string expectedMessage =
                $"Expected exception to be \"{expectedException.GetType().FullName}\", " +
                $"but found \"{actualException.GetType().FullName}\"";

            // when
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().Contain(expectedMessage);
        }

        [Fact(DisplayName = "02.3 - Level 0 - BeEquivalentToShouldFailIfExceptionMessagesDontMatch")]
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

        [Fact(DisplayName = "02.4 - Level 0 - BeEquivalentToShouldPassIfExceptionDataMatch")]
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

        [Fact(DisplayName = "02.5 - Level 0 - BeEquivalentToShouldFailIfExceptionDataDontMatch")]
        public void BeEquivalentToShouldFailIfExceptionDataDontMatch()
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

            var expectedError = new StringBuilder();
            expectedError.AppendLine($"Expected exception to:");

            expectedError.AppendLine(
                $"- have a data count of {expectedException.Data.Count}, " +
                $"but found {actualException.Data.Count}");

            expectedError.AppendLine(
                $"- NOT contain key \"{actualData.Key}\"");

            expectedError.AppendLine(
                $"- contain key \"{expectedDataOne.Key}\" with value(s) ['{expectedDataOne.Value[0]}']");

            expectedError.AppendLine(
                $"- contain key \"{expectedDataTwo.Key}\" with value(s) ['{expectedDataTwo.Value[0]}']");

            expectedError.AppendLine(
                $"- have key \"{mutualKey}\" with value(s) ['{expectedDataSameKeyName.Value[0]}'], " +
                $"but found value(s) ['{actualDataSameKeyName.Value[0]}']");

            // when
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().BeEquivalentTo(expectedError.ToString().Trim());
        }

        [Fact(DisplayName = "03.1 - Level 0 - BeEquivalentToShouldPassIfInnerExceptionsMatch")]
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

        [Fact(DisplayName = "03.2 - Level 1 - BeEquivalentToShouldFailIfInnerExceptionsTypeDontMatch")]
        public void BeEquivalentToShouldFailIfInnerExceptionsTypeDontMatch()
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
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().Contain(expectedMessage);
        }

        [Fact(DisplayName = "03.3 - Level 1 - BeEquivalentToShouldFailIfInnerExceptionMessageDontMatch")]
        public void BeEquivalentToShouldFailIfInnerExceptionMessageDontMatch()
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
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().Contain(expectedMessage);
        }

        [Fact(DisplayName = "03.4 - Level 1 - BeEquivalentToShouldPassIfInnerExceptionDataMatch")]
        public void BeEquivalentToShouldPassIfInnerExceptionDataMatch()
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

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact(DisplayName = "03.5 - Level 1 - BeEquivalentToShouldFailIfInnerExceptionDataDontMatch")]
        public void BeEquivalentToShouldFailIfInnerExceptionDataDontMatch()
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

            var expectedError = new StringBuilder();
            expectedError.AppendLine($"Expected inner exception (level 1) to:");

            expectedError.AppendLine(
                $"- have a data count of {expectedInnerException.Data.Count}, " +
                $"but found {actualInnerException.Data.Count}");

            expectedError.AppendLine(
                $"- NOT contain key \"{actualData.Key}\"");

            expectedError.AppendLine(
                $"- contain key \"{expectedDataOne.Key}\" with value(s) ['{expectedDataOne.Value[0]}']");

            expectedError.AppendLine(
                $"- contain key \"{expectedDataTwo.Key}\" with value(s) ['{expectedDataTwo.Value[0]}']");

            expectedError.AppendLine(
                $"- have key \"{mutualKey}\" with value(s) ['{expectedDataSameKeyName.Value[0]}'], " +
                $"but found value(s) ['{actualDataSameKeyName.Value[0]}']");

            var expectedException = new Xeption(
                message: exceptionMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: exceptionMessage,
                innerException: actualInnerException);

            // when
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().BeEquivalentTo(expectedError.ToString().Trim());
        }

        [Fact(DisplayName = "04.1 - Level 2 - BeEquivalentToShouldPassIfInnerInnerExceptionsMatch")]
        public void BeEquivalentToShouldPassIfInnerInnerExceptionsMatch()
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

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact(DisplayName = "04.2 - Level 2 - BeEquivalentToShouldFailIfInnerInnerExceptionsTypeDontMatch")]
        public void BeEquivalentToShouldFailIfInnerInnerExceptionsTypeDontMatch()
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
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().Contain(expectedMessage);
        }

        [Fact(DisplayName = "04.3 - Level 2 - BeEquivalentToShouldFailIfInnerInnerExceptionMessageDontMatch")]
        public void BeEquivalentToShouldFailIfInnerInnerExceptionMessageDontMatch()
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
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().Contain(expectedMessage);
        }

        [Fact(DisplayName = "04.4 - Level 2 - BeEquivalentToShouldPassIfInnerInnerExceptionDataMatch")]
        public void BeEquivalentToShouldPassIfInnerInnerExceptionDataMatch()
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

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact(DisplayName = "04.5 - Level 2 - BeEquivalentToShouldFailIfInnerInnerExceptionDataDontMatch")]
        public void BeEquivalentToShouldFailIfAggregateInnerInnerExceptionDataDontMatch()
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

            var expectedError = new StringBuilder();
            expectedError.AppendLine($"Expected inner exception (level 2) to:");

            expectedError.AppendLine(
                $"- have a data count of {expectedInnerInnerException.Data.Count}, " +
                $"but found {actualInnerInnerException.Data.Count}");

            expectedError.AppendLine(
                $"- NOT contain key \"{actualData.Key}\"");

            expectedError.AppendLine(
                $"- contain key \"{expectedDataOne.Key}\" with value(s) ['{expectedDataOne.Value[0]}']");

            expectedError.AppendLine(
                $"- contain key \"{expectedDataTwo.Key}\" with value(s) ['{expectedDataTwo.Value[0]}']");

            expectedError.AppendLine(
                $"- have key \"{mutualKey}\" with value(s) ['{expectedDataSameKeyName.Value[0]}'], " +
                $"but found value(s) ['{actualDataSameKeyName.Value[0]}']");

            var expectedException = new Xeption(
                message: exceptionMessage,
                innerException: expectedInnerException);

            var actualException = new Xeption(
                message: exceptionMessage,
                innerException: actualInnerException);

            // when
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().BeEquivalentTo(expectedError.ToString().Trim());
        }

        [Fact(DisplayName = "05.0 - Aggregate - BeEquivalentToShouldPassIfNullExceptionsMatch")]
        public void AggregateBeEquivalentToShouldPassIfNullExceptionsMatch()
        {
            // given
            AggregateException expectedException = null;
            AggregateException actualException = null;

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact(DisplayName = "06.1 - Aggregate - BeEquivalentToShouldPassIfExceptionsMatch")]
        public void AggregateBeEquivalentToShouldPassIfExceptionsMatch()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedException = new AggregateException(message: randomMessage);
            var actualException = new AggregateException(message: randomMessage);

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact(DisplayName = "06.2 - Aggregate - BeEquivalentToShouldPassIfExceptionDataMatch")]
        public void AggregateBeEquivalentToShouldPassIfExceptionDataMatch()
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

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact(DisplayName = "06.3 - Aggregate - BeEquivalentToShouldFailIfExceptionDataDontMatch")]
        public void AggregateBeEquivalentToShouldFailIfExceptionDataDontMatch()
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

            var expectedError = new StringBuilder();
            expectedError.AppendLine($"Expected exception to:");

            expectedError.AppendLine(
                $"- have a data count of {expectedException.Data.Count}, " +
                $"but found {actualException.Data.Count}");

            expectedError.AppendLine(
                $"- NOT contain key \"{actualData.Key}\"");

            expectedError.AppendLine(
                $"- contain key \"{expectedDataOne.Key}\" with value(s) ['{expectedDataOne.Value[0]}']");

            expectedError.AppendLine(
                $"- contain key \"{expectedDataTwo.Key}\" with value(s) ['{expectedDataTwo.Value[0]}']");

            expectedError.AppendLine(
                $"- have key \"{mutualKey}\" with value(s) ['{expectedDataSameKeyName.Value[0]}'], " +
                $"but found value(s) ['{actualDataSameKeyName.Value[0]}']");

            // when
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().BeEquivalentTo(expectedError.ToString().Trim());
        }

        [Fact(DisplayName = "07.1 - Aggregate - BeEquivalentToShouldPassIfInnerExceptionMatch")]
        public void AggregateBeEquivalentToShouldPassIfInnerExceptionMatchOnType()
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

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact(DisplayName = "07.2 - Aggregate - Level 0 - BeEquivalentToShouldFailIfInnerExceptionsTypeDontMatch")]
        public void AggregateBeEquivalentToShouldFailIfInnerExceptionsTypeDontMatch()
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

            string expectedMessage =
                $"* Difference in inner exception at index[0] - Expected exception " +
                $"to be \"{expectedInnerException.GetType().FullName}\", " +
                $"but found \"{actualInnerException.GetType().FullName}\"";

            // when
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().Contain(expectedMessage);
        }

        [Fact(DisplayName = "07.3 - Aggregate - Level 0 - BeEquivalentToShouldFailIfInnerExceptionsMessageDontMatch")]
        public void AggregateBeEquivalentToShouldFailIfInnerExceptionsMessageDontMatch()
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

            string expectedMessage =
                $"* Difference in inner exception at index[0] - Expected exception message to be \"{expectedInnerException.Message}\", " +
                $"but found \"{actualInnerException.Message}\"";

            // when
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().Contain(expectedMessage);
        }

        [Fact(DisplayName = "07.4 - Aggregate - Level 0 - BeEquivalentToShouldPassIfInnerExceptionDataMatch")]
        public void AggregateBeEquivalentToShouldPassIfInnerExceptionsDataMatch()
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

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact(DisplayName = "07.5 - Aggregate - Level 0 - BeEquivalentToShouldFailIfInnerExceptionDataDontMatch")]
        public void AggregateBeEquivalentToShouldFailIfInnerExceptionsDataDontMatch()
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

            var expectedError = new StringBuilder();
            expectedError.AppendLine($"Aggregate exception differences:");
            expectedError.AppendLine($"* Difference in inner exception at index[0] - Expected exception to:");

            expectedError.AppendLine(
                $"- have a data count of {expectedInnerException.Data.Count}, " +
                $"but found {actualInnerException.Data.Count}");

            expectedError.AppendLine(
                $"- NOT contain key \"{actualData.Key}\"");

            expectedError.AppendLine(
                $"- contain key \"{expectedDataOne.Key}\" with value(s) ['{expectedDataOne.Value[0]}']");

            expectedError.AppendLine(
                $"- contain key \"{expectedDataTwo.Key}\" with value(s) ['{expectedDataTwo.Value[0]}']");

            expectedError.AppendLine(
                $"- have key \"{mutualKey}\" with value(s) ['{expectedDataSameKeyName.Value[0]}'], " +
                $"but found value(s) ['{actualDataSameKeyName.Value[0]}']");

            var expectedException = new AggregateException(
                message: exceptionMessage,
                innerExceptions: expectedInnerException);

            var actualException = new AggregateException(
                message: exceptionMessage,
                innerExceptions: actualInnerException);

            // when
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().BeEquivalentTo(expectedError.ToString().Trim());
        }

        [Fact(DisplayName = "08.1 - Aggregate - Level 1 - BeEquivalentToShouldPassIfInnerInnerExceptionsMatch")]
        public void AggregateBeEquivalentToShouldPassIfInnerInnerExceptionsMatch()
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

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact(DisplayName = "08.2 - Aggregate - Level 1 - BeEquivalentToShouldFailIfInnerInnerExceptionsTypeDontMatch")]
        public void AggregateBeEquivalentToShouldFailIfInnerInnerExceptionsTypeDontMatch()
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

            var expectedError = new StringBuilder();
            expectedError.AppendLine($"Aggregate exception differences:");
            expectedError.AppendLine($"* Difference in inner exception at index[0] - " +
                $"Expected inner exception (level 1) to be \"{expectedInnerInnerException.GetType().FullName}\", " +
                $"but found \"{actualInnerInnerException.GetType().FullName}\"");

            // when
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().Contain(expectedError.ToString().Trim());
        }

        [Fact(DisplayName = "08.3 - Aggregate - Level 1 - BeEquivalentToShouldFailIfInnerInnerExceptionMessageDontMatch")]
        public void AggregateBeEquivalentToShouldFailIfInnerInnerExceptionsMessageDontMatch()
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

            var expectedError = new StringBuilder();
            expectedError.AppendLine($"Aggregate exception differences:");
            expectedError.AppendLine($"* Difference in inner exception at index[0] - " +
                $"Expected inner exception (level 1) message to be \"{expectedInnerInnerException.Message}\", " +
                $"but found \"{actualInnerInnerException.Message}\"");

            // when
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().Contain(expectedError.ToString().Trim());
        }

        [Fact(DisplayName = "08.4 - Aggregate - Level 1 - BeEquivalentToShouldPassIfInnerInnerExceptionDataMatch")]
        public void AggregateBeEquivalentToShouldPassIfInnerInnerExceptionsDataMatch()
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

            var expectedException = new AggregateException(
                message: exceptionMessage,
                innerExceptions: expectedInnerException);

            var actualException = new AggregateException(
                message: exceptionMessage,
                innerExceptions: actualInnerException);

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact(DisplayName = "08.5 - Aggregate - Level 1 - BeEquivalentToShouldFailIfInnerInnerExceptionDataDontMatch")]
        public void AggregateBeEquivalentToShouldFailIfAggregateInnerInnerExceptionDataDontMatch()
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

            var expectedError = new StringBuilder();
            expectedError.AppendLine($"Aggregate exception differences:");

            expectedError.AppendLine(
                $"* Difference in inner exception at index[0] - " +
                $"Expected inner exception (level 1) to:");

            expectedError.AppendLine(
                $"- have a data count of {expectedInnerInnerException.Data.Count}, " +
                $"but found {actualInnerInnerException.Data.Count}");

            expectedError.AppendLine(
                $"- NOT contain key \"{actualData.Key}\"");

            expectedError.AppendLine(
                $"- contain key \"{expectedDataOne.Key}\" with value(s) ['{expectedDataOne.Value[0]}']");

            expectedError.AppendLine(
                $"- contain key \"{expectedDataTwo.Key}\" with value(s) ['{expectedDataTwo.Value[0]}']");

            expectedError.AppendLine(
                $"- have key \"{mutualKey}\" with value(s) ['{expectedDataSameKeyName.Value[0]}'], " +
                $"but found value(s) ['{actualDataSameKeyName.Value[0]}']");

            var expectedException = new AggregateException(
                message: exceptionMessage,
                innerExceptions: expectedInnerException);

            var actualException = new AggregateException(
                message: exceptionMessage,
                innerExceptions: actualInnerException);

            // when
            Action assertAction = () =>
                actualException.Should().BeEquivalentTo(expectedException);

            XunitException actualError =
                Assert.Throws<XunitException>(assertAction);

            //then
            actualError.Message.Should().BeEquivalentTo(expectedError.ToString().Trim());
        }
    }
}
