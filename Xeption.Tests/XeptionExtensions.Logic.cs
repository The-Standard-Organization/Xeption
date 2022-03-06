// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using FluentAssertions;
using Xunit;

namespace Xeption.Tests
{
    public partial class XeptionExtensions
    {
        [Fact]
        public void ShouldReturnFalseIfTargetSiteIsNull()
        {
            // given
            string randomOrigin = GetRandomString();
            string inputOrigin = randomOrigin;
            var exception = new Exception();

            // when
            bool actualResult =
                exception.IsFrom(inputOrigin);

            // then
            actualResult.Should().BeFalse();
        }
    }
}
