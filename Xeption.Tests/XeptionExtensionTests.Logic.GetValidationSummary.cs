// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using FluentAssertions;
using Xunit;

namespace Xeptions.Tests
{
    public partial class XeptionExtensionTests
    {
        [Fact]
        public void ShouldReturnEmptyValidationSummaryIfNoDataItemsFoundOnExceptionOrInnerException()
        {
            // given
            string randomMessage = GetRandomString();
            Exception randomException = new Exception(message: randomMessage);
            string expectedResult = string.Empty;

            // when
            string actualResult = randomException.GetValidationSummary();

            // then
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}
