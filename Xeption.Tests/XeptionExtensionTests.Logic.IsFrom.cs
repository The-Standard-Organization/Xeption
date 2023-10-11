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

        [Fact]
        public void ShouldReturnTrueIfTargetMatchesOrigin()
        {
            // given
            string targetOrigin = nameof(XeptionExtensionTests);

            // when
            bool actualResult = false;

            try
            {
                throw new Exception();
            }
            catch (Exception exception)
            {
                actualResult = exception.IsFrom(targetOrigin);
            }

            // then
            actualResult.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnFalseIfTargetNotMatchOrigin()
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
                actualResult = exception.IsFrom(currentOrigin);
            }

            // then
            actualResult.Should().BeFalse();
        }
    }
}
