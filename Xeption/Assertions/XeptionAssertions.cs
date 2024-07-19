// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Xeptions;

namespace FluentAssertions.Exceptions
{
    public class XeptionAssertions<TException> : ReferenceTypeAssertions<Exception, XeptionAssertions<TException>>
    where TException : Exception
    {
        public XeptionAssertions(TException exception) : base(exception)
        { }

        public AndConstraint<XeptionAssertions<TException>> BeEquivalentTo(
            Exception expectation,
            string because = "",
            params object[] becauseArgs)
        {
            var actualException = Subject == null
                ? new Xeption()
                : new Xeption(Subject.Message, Subject.InnerException, Subject.Data);

            if (Subject is AggregateException actualAggregate && expectation is AggregateException expectedAggregate)
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .WithExpectation("Expected the ")
                    .Given(() => Subject)

                    .ForCondition(subject =>
                        InnerExceptionsMatch(subject, expectation, "aggregate exception", because, becauseArgs))

                    .FailWith("to be equivalent to {0}{reason}, but it was not.", expectation)
                    .Then
                    .ClearExpectation();
            }
            else
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .WithExpectation("Expected the ")
                    .Given(() => Subject)

                    .ForCondition(subject =>
                        InnerExceptionsMatch(subject, expectation, "exception", because, becauseArgs))

                    .FailWith("to be equivalent to {0}{reason}, but it was not.", expectation)
                    .Then
                    .ClearExpectation();
            }

            return new AndConstraint<XeptionAssertions<TException>>(this);
        }

        private bool InnerExceptionsMatch(
            Exception actual,
            Exception expected,
            string type,
            string because,
            params object[] becauseArgs)
        {
            if (actual is null && expected is null)
                return true;

            if (actual is null || expected is null)
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .FailWith(
                        $"Expected {type} exception to be \"{expected?.GetType().FullName ?? "null"}\", " +
                        $"but found \"{actual?.GetType().FullName ?? "null"}\".");

                return false;
            }

            var typeMatch = actual.GetType().FullName == expected.GetType().FullName;
            var messageMatch = actual.Message == expected.Message;

            if (actual is AggregateException actualAggregate && expected is AggregateException expectedAggregate)
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(typeMatch)

                    .FailWith(
                        $"Expected aggregate {type} type to be \"{expected.GetType().FullName}\", " +
                        $"but found \"{actual.GetType().FullName}\".")

                    .Then
                    .ForCondition(messageMatch)

                    .FailWith(
                        $"Expected aggregate {type} message to be \"{expected.Message}\", " +
                        $"but found \"{actual.Message}\".")

                    .Then

                    .ForCondition(
                        ExceptionDataMatch(actual, expected, "aggregate inner exception", because, becauseArgs))

                    .FailWith("data to match but it does not.")
                    .Then
                    .ClearExpectation();

                var actualInnerExceptions = actualAggregate.InnerExceptions;
                var expectedInnerExceptions = expectedAggregate.InnerExceptions;

                var countMatch = actualInnerExceptions.Count == expectedInnerExceptions.Count;

                if (!countMatch)
                {
                    Execute.Assertion
                        .BecauseOf(because, becauseArgs)
                        .ForCondition(countMatch)
                        .FailWith(
                            "Expected {0} count to be {1}, but found {2}.",
                            type,
                            expectedInnerExceptions.Count,
                            actualInnerExceptions.Count);

                    return false;
                }

                if (countMatch)
                {
                    for (int i = 0; i < actualInnerExceptions.Count; i++)
                    {
                        if (!InnerExceptionsMatch(
                            actual: actualInnerExceptions[i],
                            expected: expectedInnerExceptions[i],
                            type: $"aggregate inner exception [{i}]",
                            because,
                            becauseArgs))
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(typeMatch)

                    .FailWith(
                        $"Expected {type} type to be \"{expected.GetType().FullName}\", " +
                        $"but found \"{actual.GetType().FullName}\".")

                    .Then
                    .ForCondition(messageMatch)
                    .FailWith($"Expected {type} message to be \"{expected.Message}\", but found \"{actual.Message}\".")
                    .Then
                    .ForCondition(ExceptionDataMatch(actual, expected, type, because, becauseArgs))
                    .FailWith("data to match but it does not.")
                    .Then
                    .ClearExpectation();

                return InnerExceptionsMatch(
                    actual: actual.InnerException,
                    expected: expected.InnerException,
                    type: type == "aggregate inner exception" ? "aggregate inner exception" : "inner exception",
                    because,
                    becauseArgs);
            }

            return true;
        }

        private bool ExceptionDataMatch(
            Exception actual,
            Exception expected,
            string type,
            string because,
            params object[] becauseArgs)
        {

            IDictionary actualData = actual.Data;
            IDictionary expectedData = expected.Data;

            if (actualData == null && expectedData == null)
                return true;

            if (actualData == null || expectedData == null)
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .FailWith(
                        $"Expected {type} with type '{expected.GetType().FullName}' " +
                            $"to have data as {0}, but found {1}.",
                        expectedData?.Count == null ? "null" : "not null",
                        actualData?.Count == null ? "null" : "not null");

                return false;
            }

            StringBuilder dataSummary = new StringBuilder();

            if (actualData.Count != expectedData.Count)
            {
                dataSummary.AppendLine(
                    $"- have an expected data item count to be {expectedData.Count}, " +
                    $"but found {actualData.Count}.");
            }

            foreach (var key in expectedData.Keys)
            {
                if (!actualData.Contains(key))
                {
                    var expectedValues = string.Join(", ", expectedData[key] as List<string>);

                    dataSummary.AppendLine(
                        $"- contain key '{key}' with value(s) [{expectedValues}].");
                }
            }

            foreach (var key in actualData.Keys)
            {
                if (!expectedData.Contains(key))
                {
                    dataSummary.AppendLine(
                        $"- NOT contain key '{key}'.");
                }
            }

            foreach (var key in expectedData.Keys)
            {
                if (actualData.Contains(key))
                {
                    var actualValues = string.Join(", ", actualData[key] as List<string>);
                    var expectedValues = string.Join(", ", expectedData[key] as List<string>);

                    if (!Equals(actualValues, expectedValues))
                    {
                        dataSummary.AppendLine(
                            $"- have key '{key}' with value(s) [{expectedValues}], " +
                            $"but found value(s) [{actualValues}].");
                    }
                }
            }

            if (dataSummary.Length > 0)
            {
                string errorMessage = $"Expected {type} to:{Environment.NewLine}" +
                            $"{dataSummary.ToString()}";

                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .FailWith(errorMessage);

                return false;
            }

            return true;
        }


        protected override string Identifier => "Exception";
    }
}
