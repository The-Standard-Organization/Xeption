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
    }
}
