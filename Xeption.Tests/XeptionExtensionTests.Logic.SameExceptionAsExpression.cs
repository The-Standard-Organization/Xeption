// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using FluentAssertions;
using Force.DeepCloner;
using Xunit;

namespace Xeptions.Tests
{
    public partial class XeptionExtensionTests
    {
        [Fact]
        public void ExpressionShouldReturnTrueIfExceptionsMatch()
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
            Func<Exception, bool> expression = XeptionExtensions.SameExceptionAs(expectedException).Compile();

            // when
            bool result = expression(actualException);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public void ExpressionShouldReturnTrueIfBothExceptionsMatchOnNull()
        {
            // given
            Xeption expectedException = null;
            Xeption actualException = null;
            Func<Exception, bool> expression = XeptionExtensions.SameExceptionAs(expectedException).Compile();

            // when
            bool result = expression(actualException);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public void ExpressionShouldReturnFalseIfExceptionsDontMatchOnType()
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

            Func<Exception, bool> expression = XeptionExtensions.SameExceptionAs(expectedException).Compile();

            // when
            bool result = expression(actualException);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public void ExpressionShouldReturnFalseIfInnerExceptionsDontMatchOnType()
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

            Func<Exception, bool> expression = XeptionExtensions.SameExceptionAs(expectedException).Compile();

            // when
            bool result = expression(actualException);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public void ExpressionShouldReturnFalseIfActualInnerExceptionIsNullWhileExpectedInnerExceptionIsPresent()
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

            Func<Exception, bool> expression = XeptionExtensions.SameExceptionAs(expectedException).Compile();

            // when
            bool result = expression(actualException);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public void ExpressionShouldReturnFalseIfExpectedExceptionIsNullWhileActualExceptionIsPresent()
        {
            // given
            string randomMessage = GetRandomString();
            Exception actualInnerException = new Exception(message: randomMessage);

            Xeption expectedException = null;

            var actualException = new Xeption(
                message: randomMessage,
                innerException: actualInnerException);

            Func<Exception, bool> expression = XeptionExtensions.SameExceptionAs(expectedException).Compile();

            // when
            bool result = expression(actualException);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public void ExpressionShouldReturnFalseIfActualExceptionIsNullWhileExpectedExceptionIsPresent()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption expectedInnerException = null;

            var expectedException = new Xeption(
                message: randomMessage,
                innerException: expectedInnerException);

            Xeption actualException = null;
            Func<Exception, bool> expression = XeptionExtensions.SameExceptionAs(expectedException).Compile();

            // when
            bool result = expression(actualException);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public void ExpressionShouldReturnFalseIfExpectedInnerExceptionIsNullWhileActualInnerExceptionIsPresent()
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

            Func<Exception, bool> expression = XeptionExtensions.SameExceptionAs(expectedException).Compile();

            // when
            bool result = expression(actualException);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public void ExpressionShouldReturnTrueIfOuterExceptionsMatchWithNullInnerExceptions()
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

            Func<Exception, bool> expression = XeptionExtensions.SameExceptionAs(expectedException).Compile();

            // when
            bool result = expression(actualException);

            // then
            result.Should().BeTrue();
        }

        [Fact]
        public void ExpressionShouldReturnFalseIfExceptionMessageDontMatch()
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

            Func<Exception, bool> expression = XeptionExtensions.SameExceptionAs(expectedException).Compile();

            // when
            bool result = expression(actualException);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public void ExpressionShouldReturnFalseIfInnerExceptionMessageDontMatch()
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

            Func<Exception, bool> expression = XeptionExtensions.SameExceptionAs(expectedException).Compile();

            // when
            bool result = expression(actualException);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public void ExpressionShouldReturnFalseIfInnerExceptionDataDontMatch()
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

            Func<Exception, bool> expression = XeptionExtensions.SameExceptionAs(expectedException).Compile();

            // when
            bool result = expression(actualException);

            // then
            result.Should().BeFalse();
        }

        [Fact]
        public void ExpressionShouldReturnFalseIfExceptionDataDontMatch()
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

            Func<Exception, bool> expression = XeptionExtensions.SameExceptionAs(expectedException).Compile();

            // when
            bool result = expression(actualException);

            // then
            result.Should().BeFalse();
        }
    }
}
