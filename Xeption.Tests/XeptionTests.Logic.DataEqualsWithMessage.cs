// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using FluentAssertions;
using Force.DeepCloner;
using Xunit;
using ICollectionDictionary = System.Collections.IDictionary;

namespace Xeptions.Tests
{
    public partial class XeptionTests
    {
        [Fact]
        public void ShouldReturnTrueAndNullStringIfDataMatchInCount()
        {
            // given
            Xeption randomXeption = new Xeption();

            Dictionary<string, List<string>> randomDictionary = CreateRandomDictionary();
            randomXeption.AddData(randomDictionary);
            Xeption expectedXeption = randomXeption;
            Xeption actualXeption = expectedXeption.DeepClone();

            // when
            var actualComparisonResult = actualXeption.DataEqualsWithDetail(expectedXeption.Data);

            // then
            actualComparisonResult.IsEqual.Should().BeTrue();
            actualComparisonResult.Message.Should().BeNullOrEmpty();
        }
    }
}