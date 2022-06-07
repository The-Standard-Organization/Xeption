// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using FluentAssertions;
using Xunit;

namespace Xeptions.Tests
{
    public partial class XeptionAssertionTests
    {
        [Fact]
        public void BeEquivalentToShouldPassIfExceptionsMatchOnType()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedException = new Xeption(message: randomMessage);
            var actualException = new Xeption(message: randomMessage);

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact]
        public void BeEquivalentToShouldPassIfNullExceptionsMatchOnType()
        {
            // given
            Xeption expectedException = null;
            Xeption actualException = null;

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }

        [Fact]
        public void BeEquivalentToShouldPassIfExceptionMessagesMatch()
        {
            // given
            string randomMessage = GetRandomString();
            var expectedException = new Xeption(message: randomMessage);
            var actualException = new Xeption(message: GetRandomString());

            // when then
            actualException.Should().BeEquivalentTo(expectedException);
        }
    }
}
