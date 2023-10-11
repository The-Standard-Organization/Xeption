// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;

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
            return
                (exception is null && otherException is null) ||
                (exception?.GetType()?.FullName == otherException?.GetType()?.FullName
                && exception?.Message == otherException?.Message
                && ((Xeption)exception).DataEquals(otherException?.Data)
                && ((exception?.InnerException is null && otherException?.InnerException is null) ||
                (exception?.InnerException?.GetType()?.FullName == otherException?.InnerException?.GetType()?.FullName
                && exception?.InnerException?.Message == otherException?.InnerException?.Message
                && ((Xeption)(exception?.InnerException)).DataEquals(otherException?.InnerException?.Data))));
        }
    }
}
