using System;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
namespace FluentAssertions.Exceptions
{
    public class XeptionAssertions<TException> : ReferenceTypeAssertions<Exception, XeptionAssertions<TException>>
    where TException : Exception
    {
        public XeptionAssertions(TException exception) : base(exception)
        {
        }

        public AndConstraint<XeptionAssertions<TException>> BeEquivalentTo(Exception expectation, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected the {reason}, ")
                .Given(() => Subject)
                .ForCondition(subject => subject?.GetType()?.FullName == expectation?.GetType()?.FullName)
                .FailWith("type to be {0}, but found the type to be {1}.", expectation?.GetType()?.FullName, Subject?.GetType()?.FullName)
                .Then
                .ForCondition(subject => subject?.Message == expectation?.Message)
                .FailWith("message to be {0}, but found {1}.", expectation?.Message, Subject?.Message)
                .Then
                .ForCondition(subject => ((subject?.InnerException is null && expectation?.InnerException is null)) || (subject?.InnerException?.GetType()?.FullName == expectation?.InnerException?.GetType()?.FullName))
                .FailWith("inner exception type to be {0}, but found the inner exception type to be {1}.", expectation?.InnerException?.GetType()?.FullName, Subject?.InnerException?.GetType()?.FullName)
                .Then
                .ClearExpectation();

            return new AndConstraint<XeptionAssertions<TException>>(this);
        }

        protected override string Identifier => "Exception";
    }
}
