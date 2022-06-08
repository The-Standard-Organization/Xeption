// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Xeptions;

namespace FluentAssertions.Exceptions
{
    public class XeptionAssertions<TException> : ReferenceTypeAssertions<Exception, XeptionAssertions<TException>>
    where TException : Exception
    {
        public XeptionAssertions(TException exception) : base(exception)
        {
        }

        public AndConstraint<XeptionAssertions<TException>> BeEquivalentTo(
            Exception expectation,
            string because = "",
            params object[] becauseArgs)
        {
            var actualException = Subject as Xeption ?? new Xeption();
            var expectedException = expectation ?? new Exception();
            var actualInnerException = Subject?.InnerException as Xeption ?? new Xeption();
            var expectedInnerException = expectation?.InnerException ?? new Exception();
            var exceptionDataComparisonResult = actualException.DataEqualsWithDetail(expectedException.Data);
            var innerExceptionDataComparisonResult = actualInnerException.DataEqualsWithDetail(expectedInnerException.Data);

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected the ")
                .Given(() => Subject)
                .ForCondition(subject => subject?.GetType()?.FullName == expectation?.GetType()?.FullName)
                .FailWith(
                    "type to be {0}, but found the type to be {1}.",
                    expectation?.GetType()?.FullName,
                    Subject?.GetType()?.FullName)
                .Then
                .ForCondition(subject => subject?.Message == expectation?.Message)
                .FailWith(
                    "message to be {0}, but found {1}.",
                    expectation?.Message,
                    Subject?.Message)
                .Then
                .ForCondition(subject => ((subject?.InnerException is null && expectation?.InnerException is null)) ||
                    (subject?.InnerException?.GetType()?.FullName == expectation?.InnerException?.GetType()?.FullName))
                .FailWith(
                    "inner exception type to be {0}, but found the inner exception type to be {1}.",
                    expectation?.InnerException?.GetType()?.FullName,
                    Subject?.InnerException?.GetType()?.FullName)
                .Then
                .ForCondition(subject => subject?.InnerException?.Message == expectation?.InnerException?.Message)
                .FailWith(
                    "inner exception message to be {0}, but found {1}.",
                    expectation?.InnerException?.Message,
                    Subject?.InnerException?.Message)
                .Then
                .ForCondition(subject =>
                    (subject?.InnerException?.Data is null && expectation?.InnerException?.Data is null) ||
                        (subject?.InnerException?.Data?.Count == expectation?.InnerException?.Data?.Count))
                .FailWith(
                    "inner exception data to have {0} items, but found {1}.",
                    expectation?.InnerException?.Data?.Count,
                    Subject?.InnerException?.Data?.Count)
                .Then
                .ForCondition(subject => exceptionDataComparisonResult.IsEqual == true)
                .FailWith(
                    "exception data to match but found: "
                    + Environment.NewLine + exceptionDataComparisonResult.Message)
                .Then
                .ForCondition(subject => innerExceptionDataComparisonResult.IsEqual == true)
                .FailWith(
                    "inner exception data to match but found: "
                    + Environment.NewLine + innerExceptionDataComparisonResult.Message)
                .Then
                .ClearExpectation();

            return new AndConstraint<XeptionAssertions<TException>>(this);
        }

        protected override string Identifier => "Exception";
    }
}
