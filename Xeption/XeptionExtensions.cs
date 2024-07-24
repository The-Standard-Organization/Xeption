// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Text;

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
            return IsSameExceptionsAs(exception, otherException, out string message);
        }

        public static bool SameExceptionAs(this Exception exception, Exception otherException, out string message)
        {
            return IsSameExceptionsAs(exception, otherException, out message);
        }

        public static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        internal static bool IsSameExceptionsAs(
            Exception exception,
            Exception otherException,
            out string message,
            int exceptionLevel = 0)
        {
            string exceptionLevelName = exceptionLevel == 0
                ? "exception"
                : $"inner exception (level {exceptionLevel})";

            if (exception is null && otherException is null)
            {
                message = String.Empty;

                return true;
            }

            if (exception is null || otherException is null)
            {
                message = $"Expected {exceptionLevelName} to be \"{otherException?.GetType()?.FullName ?? "null"}\", " +
                    $"but found \"{exception?.GetType()?.FullName ?? "null"}\"";

                return false;
            }

            var errors = new StringBuilder();
            bool invalidException = false;

            if (exception.GetType().FullName != otherException.GetType().FullName)
            {
                invalidException = true;

                errors.AppendLine($"Expected {exceptionLevelName} to be " +
                    $"\"{otherException?.GetType()?.FullName ?? "null"}\", " +
                    $"but found \"{exception?.GetType()?.FullName ?? "null"}\"");
            }

            if ((exception is AggregateException
                && otherException is AggregateException) is false)
            {
                if (exception.Message != otherException.Message)
                {
                    invalidException = true;

                    errors.AppendLine($"Expected {exceptionLevelName} message to be \"{otherException.Message}\", " +
                        $"but found \"{exception.Message}\"");
                }
            }

            (bool isDataEqual, string dataMessage) =
                Xeption.CompareDataKeys(exception.Data, otherException.Data, exceptionLevel);

            if (isDataEqual == false)
            {
                invalidException = true;
                errors.AppendLine(dataMessage);
            }

            if (invalidException == true)
            {
                message = errors.ToString().Trim();
                return false;
            }

            if (exception is AggregateException aggregateException
                && otherException is AggregateException otherAggregateException)
            {
                if (aggregateException.InnerExceptions.Count != otherAggregateException.InnerExceptions.Count)
                {
                    message =
                        $"Expected aggregate exception to contain " +
                        $"{otherAggregateException.InnerExceptions.Count} inner exception(s), " +
                        $"but found {aggregateException.InnerExceptions.Count}";

                    return false;
                }
                else
                {
                    var aggregateErrors = new StringBuilder();
                    aggregateErrors.AppendLine("Aggregate exception differences:");

                    for (int i = 0; i < aggregateException.InnerExceptions.Count; i++)
                    {
                        if (!aggregateException.InnerExceptions[i].SameExceptionAs(
                            otherAggregateException.InnerExceptions[i], out string innerMessage))
                        {
                            invalidException = true;

                            aggregateErrors
                                .AppendLine($"* Difference in inner exception at index[{i}] - {innerMessage}");
                        }
                    }

                    if (invalidException == true)
                    {
                        message = aggregateErrors.ToString().Trim();
                        return false;
                    }
                }
            }

            return IsSameExceptionsAs(
                exception.InnerException,
                otherException.InnerException,
                out message,
                exceptionLevel + 1);
        }
    }
}
