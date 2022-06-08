// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

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
