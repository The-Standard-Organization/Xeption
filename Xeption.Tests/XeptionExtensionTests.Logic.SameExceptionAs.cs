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
            var innerException = new Xeption(exceptionMessage);

            innerException.AddData(
                key: GetRandomString(),
                values: GetRandomString());

            var exception = new Xeption(exceptionMessage, innerException);
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
            var innerException = new Xeption(innerExceptionMessage);

            var exception = new Xeption(exceptionMessage, innerException);
            var otherException = new Xeption(otherExceptionMessage, innerException);

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
            string otherExceptionMessage = GetRandomString();

            string innerExceptionMessage = GetRandomString();
            var innerException = new Xeption(innerExceptionMessage);

            string otherInnerExceptionMessage = GetRandomString();
            var otherInnerException = new Xeption(otherInnerExceptionMessage);


            var exception = new Xeption(exceptionMessage, innerException);
            var otherException = new Xeption(exceptionMessage, otherInnerException);

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
            var innerException = new Xeption(innerExceptionMessage);
            var otherInnerException = innerException.DeepClone();

            otherInnerException.AddData(
                key: GetRandomString(),
                values: GetRandomString());

            var exception = new Xeption(exceptionMessage, innerException);
            var otherException = new Xeption(exceptionMessage, otherInnerException);

            // when
            bool actualResult =
                exception.SameExceptionAs(otherException);

            // then
            actualResult.Should().BeFalse();
        }
    }
}
