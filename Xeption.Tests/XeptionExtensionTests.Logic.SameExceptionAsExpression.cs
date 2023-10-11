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
    }
}
