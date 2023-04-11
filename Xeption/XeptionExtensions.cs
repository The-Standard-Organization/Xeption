// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

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

        public static string GetValidationSummary(this Exception exception)
        {
            if ((exception == null || exception.Data.Count == 0)
                && (exception?.InnerException == null || exception.InnerException.Data.Count == 0))
            {
                return string.Empty;
            }

            throw new NotImplementedException();
        }
    }
}
