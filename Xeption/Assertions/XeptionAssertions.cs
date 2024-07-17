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

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected the ")
                .Given(() => Subject)
                .ForCondition(subject => InnerExceptionsMatch(subject, expectation, because, becauseArgs))
                .FailWith("to be equivalent to {0}{reason}, but it was not.", expectation)
                .Then
                .ClearExpectation();

            return new AndConstraint<XeptionAssertions<TException>>(this);
        }

        private bool InnerExceptionsMatch(
            Exception actual,
            Exception expected,
            string because,
            params object[] becauseArgs)
        {
            if (actual == null && expected == null)
                return true;

            if (actual == null || expected == null)
            {
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .FailWith(
                        "Expected exception to be {0}, but found {1}.",
                        expected?.GetType().FullName ?? "null",
                        actual?.GetType().FullName ?? "null");

                return false;
            }

            var typeMatch = actual.GetType().FullName == expected.GetType().FullName;
            var messageMatch = actual.Message == expected.Message;

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(typeMatch)

                .FailWith(
                    "Expected exception type to be {0}, but found {1}.",
                    expected.GetType().FullName,
                    actual.GetType().FullName)

                .Then
                .ForCondition(messageMatch)

                .FailWith(
                    "Expected exception message to be {0}, but found {1}.",
                    expected.Message,
                    actual.Message)

                .Then
                .ForCondition(ExceptionDataMatch(actual, expected, because, becauseArgs))
                .FailWith("data to match but it does not.");

            if (actual is AggregateException actualAggregate && expected is AggregateException expectedAggregate)
            {
                var actualInnerExceptions = actualAggregate.InnerExceptions;
                var expectedInnerExceptions = expectedAggregate.InnerExceptions;

                var countMatch = actualInnerExceptions.Count == expectedInnerExceptions.Count;
                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(countMatch)
                    .FailWith(
                        "Expected AggregateException inner exception count to be {0}, but found {1}.",
                        expectedInnerExceptions.Count,
                        actualInnerExceptions.Count);

                if (countMatch)
                {
                    for (int i = 0; i < actualInnerExceptions.Count; i++)
                    {
                        if (!InnerExceptionsMatch(
                            actual: actualInnerExceptions[i],
                            expected: expectedInnerExceptions[i],
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
                return InnerExceptionsMatch(
                    actual: actual.InnerException,
                    expected: expected.InnerException,
                    because, becauseArgs);
            }

            return true;
        }

        private bool ExceptionDataMatch(
            Exception actual,
            Exception expected,
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
                        $"Expected exception with type '{expected.GetType().FullName}' " +
                            $"and message '{expected.Message}' to have data as {0}, but found {1}.",
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
                    dataSummary.AppendLine(
                        $"- contain key '{key}', but it was not found.");
                }
            }

            foreach (var key in actualData.Keys)
            {
                if (!expectedData.Contains(key))
                {
                    dataSummary.AppendLine(
                        $"- not contain key '{key}'.");
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
                            $"- find key '{key}' with value(s) [{expectedValues}], " +
                            $"but found value(s) [{actualValues}].");
                    }
                }
            }

            if (dataSummary.Length > 0)
            {
                string errorMessage = $"Expected exception with type '{expected.GetType().FullName}' " +
                            $"and message '{expected.Message}' to:{Environment.NewLine}" +
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
