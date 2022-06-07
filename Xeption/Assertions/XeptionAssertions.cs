using System;
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
            throw new NotImplementedException();
        }

        protected override string Identifier => "Exception";
    }
}
