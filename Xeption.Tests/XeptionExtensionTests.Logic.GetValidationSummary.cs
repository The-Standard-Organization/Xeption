// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
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

        [Fact]
        public void ShouldReturnValidationSummaryIfExceptionHasDataItems()
        {
            // given
            string randomMessage = GetRandomString();
            Xeption randomXeption = new Xeption(message: randomMessage);
            randomXeption.Data.Add("Error1", new List<string> { "Error message 1" });
            randomXeption.Data.Add("Error2", new List<string> { "Error message 2", "Error message 3" });

            string expectedResult = $"{randomXeption.GetType().Name} Errors:  " +
                $"Error1 => Error message 1;  Error2 => Error message 2, Error message 3;  \r\n";

            // when
            string actualResult = randomXeption.GetValidationSummary();

            // then
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void ShouldReturnValidationSummaryIfInnerExceptionHasDataItems()
        {
            // given
            string randomMessage = GetRandomString();
            string randomInnerExceptionMessage = GetRandomString();
            Xeption randomInnerXeption = new Xeption(message: randomInnerExceptionMessage);
            randomInnerXeption.Data.Add("Error1", new List<string> { "Error message 1" });
            randomInnerXeption.Data.Add("Error2", new List<string> { "Error message 2", "Error message 3" });
            Xeption randomXeption = new Xeption(randomMessage, randomInnerXeption);

            string expectedResult = $"{randomInnerXeption.GetType().Name} Errors:  " +
                $"Error1 => Error message 1;  Error2 => Error message 2, Error message 3;  \r\n";

            // when
            string actualResult = randomXeption.GetValidationSummary();

            // then
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void ShouldReturnValidationSummaryIfExceptionAndInnerExceptionHasDataItems()
        {
            // given
            string randomMessage = GetRandomString();
            string randomInnerExceptionMessage = GetRandomString();
            Xeption randomInnerXeption =
                new Xeption(message: randomInnerExceptionMessage);

            randomInnerXeption.Data.Add("Error3", new List<string> { "Error message 4" });
            randomInnerXeption.Data.Add("Error4", new List<string> { "Error message 5", "Error message 6" });

            Xeption randomXeption =
                new Xeption(randomMessage, randomInnerXeption);

            randomXeption.Data.Add("Error1", new List<string> { "Error message 1" });
            randomXeption.Data.Add("Error2", new List<string> { "Error message 2", "Error message 3" });

            string expectedResult =
                $"{randomXeption.GetType().Name} Errors:  " +
                $"Error1 => Error message 1;  Error2 => Error message 2, Error message 3;  \r\n" +
                $"{randomInnerXeption.GetType().Name} Errors:  " +
                $"Error3 => Error message 4;  Error4 => Error message 5, Error message 6;  \r\n";

            // when
            string actualResult = randomXeption.GetValidationSummary();

            // then
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}
