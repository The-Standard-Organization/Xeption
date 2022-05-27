// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

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
            string exceptionMessage = GetRandomString();
            var innerException = new Xeption(message: exceptionMessage);

            innerException.AddData(
                key: GetRandomString(),
                values: GetRandomString());

            var exception = new Xeption(
                message: exceptionMessage,
                innerException: innerException);

            var otherException = exception.DeepClone();

            // when
            bool actualResult =
                exception.SameExceptionAs(otherException);

            // then
            actualResult.Should().BeFalse();
        }

        [Fact]
        public void ShouldReturnFalseIfExceptionMessageDontMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            string otherExceptionMessage = GetRandomString();

            string innerExceptionMessage = GetRandomString();
            var innerException = new Xeption(message: innerExceptionMessage);

            var exception = new Xeption(
                message: exceptionMessage, 
                innerException: innerException);

            var otherException = new Xeption(
                message: otherExceptionMessage, 
                innerException: innerException);

            // when
            bool actualResult =
                exception.SameExceptionAs(otherException);

            // then
            actualResult.Should().BeFalse();
        }

        [Fact]
        public void ShouldReturnFalseIfInnerExceptionMessageDontMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            string innerExceptionMessage = GetRandomString();
            var innerException = new Xeption(message: innerExceptionMessage);
            string otherInnerExceptionMessage = GetRandomString();
            var otherInnerException = new Xeption(message: otherInnerExceptionMessage);

            var exception = new Xeption(
                message: exceptionMessage, 
                innerException: innerException);
            
            var otherException = new Xeption(
                message: exceptionMessage, 
                innerException: otherInnerException);

            // when
            bool actualResult =
                exception.SameExceptionAs(otherException);

            // then
            actualResult.Should().BeFalse();
        }

        [Fact]
        public void ShouldReturnFalseIfInnerExceptionDataDontMatch()
        {
            // given
            string exceptionMessage = GetRandomString();
            string otherExceptionMessage = GetRandomString();
            string innerExceptionMessage = GetRandomString();
            var innerException = new Xeption(message: innerExceptionMessage);
            var otherInnerException = innerException.DeepClone();

            otherInnerException.AddData(
                key: GetRandomString(),
                values: GetRandomString());

            var exception = new Xeption(
                message: exceptionMessage, 
                innerException: innerException);

            var otherException = new Xeption(
                message: exceptionMessage, 
                innerException: otherInnerException);

            // when
            bool actualResult =
                exception.SameExceptionAs(otherException);

            // then
            actualResult.Should().BeFalse();
        }
    }
}
