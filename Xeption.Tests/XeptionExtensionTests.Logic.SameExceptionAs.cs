// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
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
    }
}
