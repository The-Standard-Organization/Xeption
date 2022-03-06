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
    public partial class XeptionExtensionTests
    {
        [Fact]
        public void ShouldReturnTrueOnIsNotFromIfTargetSiteIsNull()
        {
            // given
            string randomOrigin = GetRandomString();
            string inputOrigin = randomOrigin;
            var exception = new Exception();

            // when
            bool actualResult =
                exception.IsNotFrom(inputOrigin);

            // then
            actualResult.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnFalseOnIsNotFromIfTargetMatchesOrigin()
        {
            // given
            string targetOrigin = nameof(XeptionExtensionTests);

            // when
            bool actualResult = true;

            try
            {
                throw new Exception();
            }
            catch (Exception exception)
            {
                actualResult = exception.IsNotFrom(targetOrigin);
            }

            // then
            actualResult.Should().BeFalse();
        }
    }
}
