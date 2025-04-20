// // ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// Licensed under The Standard Software License (TSSL).
// See License.txt in the project root for license information.
// ----------------------------------------------------------------------------------

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
