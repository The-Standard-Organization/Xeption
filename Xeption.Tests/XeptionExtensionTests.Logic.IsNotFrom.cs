// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using FluentAssertions;
using Xunit;

namespace Xeptions.Tests
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

        [Fact]
        public void ShouldReturnTrueOnIsNotFromIfTargetNotMatchOrigin()
        {
            // given
            string currentOrigin = nameof(XeptionExtensionTests);

            // when
            bool actualResult = true;

            try
            {
                OtherTarget.ThrowingExceptionMethod();
            }
            catch (Exception exception)
            {
                actualResult = exception.IsNotFrom(currentOrigin);
            }

            // then
            actualResult.Should().BeTrue();
        }
    }
}
