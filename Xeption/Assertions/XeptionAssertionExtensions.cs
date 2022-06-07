using System;
using FluentAssertions.Exceptions;

namespace FluentAssertions
{
    public static class XeptionAssertionExtensions
    {
        public static XeptionAssertions<TException> Should<TException>(this TException actualValue)
        where TException : Exception
        {
            return new XeptionAssertions<TException>(actualValue);
        }
    }
}
