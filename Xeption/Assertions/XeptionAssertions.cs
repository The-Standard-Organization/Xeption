// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Xeptions;

namespace FluentAssertions.Exceptions
{
    public class XeptionAssertions<TException> : ReferenceTypeAssertions<Exception, XeptionAssertions<TException>>
        where TException : Exception
    {
        public XeptionAssertions(TException exception, AssertionChain assertionChain) 
            : base(exception, assertionChain)
        { }

        public AndConstraint<XeptionAssertions<TException>> BeEquivalentTo(
            Exception expectation,
            string because = "",
            params object[] becauseArgs)
        {
            Exception actualException = Subject;
            Exception expectedException = expectation;

            bool isMatch = XeptionExtensions
                .IsSameExceptionsAs(actualException, expectedException, out string message);

            CurrentAssertionChain
                .ForCondition(isMatch)
                .BecauseOf(because, becauseArgs)
                .FailWith(message);

            return new AndConstraint<XeptionAssertions<TException>>(this);
        }

        protected override string Identifier => "Exception";
    }
}
