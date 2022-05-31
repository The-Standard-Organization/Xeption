// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;

namespace Xeptions
{
    public static class XeptionExtensions
    {
        public static bool IsFrom(this Exception exception, string origin) =>
            exception.TargetSite?.ReflectedType?.Name == origin;

        public static bool IsNotFrom(this Exception exception, string origin) =>
            exception.TargetSite?.ReflectedType?.Name != origin;

        public static bool SameExceptionAs(this Exception exception, Exception otherException)
        {
            return exception.Message == otherException.Message
                && exception.InnerException.Message == otherException.InnerException.Message
                && ((Xeption)exception.InnerException).DataEquals(otherException.InnerException.Data)
                && exception.GetType().FullName == otherException.GetType().FullName;
        }
    }
}
