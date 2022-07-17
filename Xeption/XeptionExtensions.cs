// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Force.DeepCloner;

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

        public static void UpsertDataList(this Exception exception, string key, string value)
        {
            if (exception.Data.Contains(key))
            {
                (exception.Data[key] as List<string>)?.Add(value);
            }
            else
            {
                exception.Data.Add(key, new List<string> { value });
            }
        }

        public static void ThrowIfContainsErrors(this Exception exception)
        {
            if (exception.Data.Count > 0)
            {
                throw exception;
            }
        }

        public static void AddData(this Exception exception, IDictionary dictionary)
        {
            if (dictionary != null)
            {
                foreach (DictionaryEntry item in dictionary)
                {
                    exception.Data.Add(item.Key, item.Value);
                }
            }
        }

        public static void AddData(this Exception exception, string key, params string[] values) =>
            exception.Data.Add(key, values.ToList());

        public static bool DataEquals(this Exception exception, IDictionary dictionary)
        {
            (var isEqual, var message) = exception.DataEqualsWithDetail(dictionary);

            return isEqual;
        }

        public static (bool IsEqual, string Message) DataEqualsWithDetail(this Exception exception, IDictionary dictionary)
        {
            bool isEqual = true;
            StringBuilder messageStringBuilder = new StringBuilder();
            isEqual = exception.CompareDataKeys(dictionary, isEqual, messageStringBuilder);

            return (isEqual, messageStringBuilder.ToString());
        }

        private static bool CompareDataKeys(this Exception exception, IDictionary dictionary, bool isEqual, StringBuilder messageStringBuilder)
        {
            if (exception.Data.Count == 0 && dictionary.Count == 0)
            {
                return isEqual;
            }

            if (exception.Data.Count != dictionary.Count)
            {
                isEqual = false;

                exception.AppendMessage(
                    messageStringBuilder,
                    $"- Expected data item count to be {dictionary.Count}, but found {exception.Data.Count}.");
            }

            (var additionalItems, var missingItems, var sharedItems) = exception.GetDataDifferences(dictionary);
            isEqual = exception.EvaluateAdditionalKeys(isEqual, messageStringBuilder, additionalItems);
            isEqual = exception.EvaluateMissingKeys(isEqual, messageStringBuilder, missingItems);
            isEqual = exception.EvaluateSharedKeys(isEqual, messageStringBuilder, sharedItems);

            return isEqual;
        }

        private static bool EvaluateAdditionalKeys(
            this Exception exception,
            bool isEqual,
            StringBuilder messageStringBuilder,
            IDictionary? additionalItems)
        {
            if (additionalItems?.Count > 0)
            {
                isEqual = false;

                foreach (DictionaryEntry dictionaryEntry in additionalItems)
                {
                    exception.AppendMessage(
                        messageStringBuilder,
                        $"- Did not expect to find key '{dictionaryEntry.Key}'.");
                }
            }

            return isEqual;
        }

        private static bool EvaluateMissingKeys(
            this Exception exception,
            bool isEqual,
            StringBuilder messageStringBuilder,
            IDictionary? missingItems)
        {
            if (missingItems?.Count > 0)
            {
                isEqual = false;

                foreach (DictionaryEntry dictionaryEntry in missingItems)
                {
                    exception.AppendMessage(
                        messageStringBuilder,
                        $"- Expected to find key '{dictionaryEntry.Key}'.");
                }
            }

            return isEqual;
        }

        private static bool EvaluateSharedKeys(
            this Exception exception,
            bool isEqual,
            StringBuilder messageStringBuilder,
            IDictionary? sharedItems)
        {
            if (sharedItems?.Count > 0)
            {
                foreach (DictionaryEntry dictionaryEntry in sharedItems)
                {

                    string expectedValues = ((List<string>)dictionaryEntry.Value)
                            .Select(value => value).Aggregate((t1, t2) => t1 + "','" + t2);

                    string actualValues = ((List<string>)exception.Data[dictionaryEntry.Key])
                        .Select(value => value).Aggregate((t1, t2) => t1 + "','" + t2);

                    if (actualValues != expectedValues)
                    {
                        isEqual = false;

                        exception.AppendMessage(
                            messageStringBuilder,
                            $"- Expected to find key '{dictionaryEntry.Key}' " +
                            $"with value(s) ['{expectedValues}'], " +
                            $"but found value(s) ['{actualValues}'].");
                    }
                }
            }

            return isEqual;
        }

        private static (
            IDictionary AdditionalItems,
            IDictionary MissingItems,
            IDictionary SharedItems)
            GetDataDifferences(this Exception exception, IDictionary dictionary)
        {
            IDictionary additionalItems = exception.Data.DeepClone();
            IDictionary missingItems = dictionary.DeepClone();
            IDictionary sharedItems = dictionary.DeepClone();

            foreach (DictionaryEntry dictionaryEntry in dictionary)
            {
                additionalItems.Remove(dictionaryEntry.Key);
            }

            foreach (DictionaryEntry dictionaryEntry in exception.Data)
            {
                missingItems.Remove(dictionaryEntry.Key);
            }

            foreach (DictionaryEntry dictionaryEntry in additionalItems)
            {
                sharedItems.Remove(dictionaryEntry.Key);
            }

            foreach (DictionaryEntry dictionaryEntry in missingItems)
            {
                sharedItems.Remove(dictionaryEntry.Key);
            }

            return (additionalItems, missingItems, sharedItems);
        }

        private static void AppendMessage(this Exception exception, StringBuilder builder, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                builder.AppendLine(message);
            }
        }
    }
}
