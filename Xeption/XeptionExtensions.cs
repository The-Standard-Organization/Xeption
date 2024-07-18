// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using FluentAssertions;

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
            try
            {
                exception.Should().BeEquivalentTo(otherException);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool SameExceptionAs(this Exception exception, Exception otherException, out string message)
        {
            try
            {
                exception.Should().BeEquivalentTo(otherException);
                message = string.Empty;
                return true;
            }
            catch (Exception error)
            {
                message = error.Message;
                return false;
            }
        }
    }
}
