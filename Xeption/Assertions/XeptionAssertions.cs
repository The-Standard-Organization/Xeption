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
                .ClearExpectation();

            return new AndConstraint<XeptionAssertions<TException>>(this);
        }

        protected override string Identifier => "Exception";
    }
}
