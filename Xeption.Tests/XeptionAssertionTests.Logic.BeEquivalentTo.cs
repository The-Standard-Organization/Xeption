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
    public partial class XeptionAssertionTests
    {
        [Fact]
        public void BeEquivalentToShouldPassIfExceptionsMatch()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedException = new Xeption(message: randomMessage);

            var actualException = expectedException.DeepClone();

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }
    }
}
