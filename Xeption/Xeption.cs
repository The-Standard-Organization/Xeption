// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
// Licensed under The Standard Software License (TSSL).
// See License.txt in the project root for license information.
// ----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Force.DeepCloner;

namespace Xeptions
{
    public class Xeption : Exception
    {
        public Xeption() : base() { }

        public Xeption(string message) : base(message) { }

        public Xeption(string message, Exception innerException)
            : base(message, innerException)
        { }

        public Xeption(Exception innerException, IDictionary data)
            : base(innerException.Message, innerException)
        {
            this.AddData(data);
        }

        public Xeption(string message, Exception innerException, IDictionary data)
            : base(message, innerException)
        {
            this.AddData(data);
        }

        public void UpsertDataList(string key, string value)
        {
            if (this.Data.Contains(key))
            {
                if (this.Data[key] is List<string> existingList)
                {
                    existingList.Add(value);
                }
                else
                {
                    this.Data[key] = new List<string> { value };
                }
            }
            else
            {
                this.Data.Add(key, new List<string> { value });
            }
        }

        public void ThrowIfContainsErrors()
        {
            if (this.Data.Count > 0)
            {
                throw this;
            }
        }

        public void AddData(IDictionary dictionary)
        {
            if (dictionary != null)
            {
                foreach (DictionaryEntry item in dictionary)
                {
                    if (item.Value is List<string> listValue)
                    {
                        this.Data.Add(item.Key, listValue);
                    }
                    else if (item.Value is string[] arrayValue)
                    {
                        this.Data.Add(item.Key, arrayValue.ToList());
                    }
                    else
                    {
                        this.Data.Add(item.Key, item.Value);
                    }
                }
            }
        }

        public void AddData(string key, params string[] values) =>
            this.Data.Add(key, values.ToList());

        public bool DataEquals(IDictionary dictionary)
        {
            (var isEqual, var message) = this.DataEqualsWithDetail(dictionary);

            return isEqual;
        }

        public (bool IsEqual, string Message) DataEqualsWithDetail(IDictionary dictionary)
        {
            (bool isEqual, string error) = CompareDataKeys(this.Data, dictionary);

            return (isEqual, error);
        }

        internal static (bool IsMatch, string Message) CompareDataKeys(
            IDictionary dictionary,
            IDictionary otherDictionary,
            int exceptionLevel = 0)
        {
            if (dictionary.Count == 0 && otherDictionary.Count == 0)
            {
                return (true, String.Empty);
            }

            string exceptionLevelName = exceptionLevel == 0
                ? "exception"
                : $"inner exception (level {exceptionLevel})";

            var errors = new StringBuilder();
            errors.AppendLine($"Expected {exceptionLevelName} to:");
            bool unmatched = dictionary.Count != otherDictionary.Count;

            if (dictionary.Count != otherDictionary.Count)
            {
                errors.AppendLine($"- have a data count of {otherDictionary.Count}, but found {dictionary.Count}");
            }

            (IDictionary additionalItems, IDictionary missingItems, IDictionary sharedItems) =
                GetDataDifferences(dictionary, otherDictionary);

            (bool hasAdditionalItems, string additionalErrors) = EvaluateAdditionalKeys(additionalItems);
            (bool hasMissingItems, string missingErrors) = EvaluateMissingKeys(missingItems);
            (bool unMatchedItems, string unMatchedItemsErrors) = EvaluateSharedKeys(dictionary, sharedItems);

            if (unmatched || hasAdditionalItems || hasMissingItems || unMatchedItems)
            {
                if (!String.IsNullOrWhiteSpace(additionalErrors))
                {
                    errors.AppendLine(additionalErrors);
                }

                if (!String.IsNullOrWhiteSpace(missingErrors))
                {
                    errors.AppendLine(missingErrors);
                }

                if (!String.IsNullOrWhiteSpace(unMatchedItemsErrors))
                {
                    errors.AppendLine(unMatchedItemsErrors);
                }

                return (false, errors.ToString().Trim());
            }

            return (true, String.Empty);
        }

        private static (bool hasAdditionalItems, string additionalErrors) EvaluateAdditionalKeys(
            IDictionary additionalItems)
        {
            bool hasAdditionalItems = additionalItems?.Count > 0;
            var additionalErrors = new StringBuilder();

            if (additionalItems?.Count > 0)
            {
                foreach (DictionaryEntry dictionaryEntry in additionalItems)
                {
                    additionalErrors.AppendLine($"- NOT contain key \"{dictionaryEntry.Key}\"");
                }
            }

            return (hasAdditionalItems, additionalErrors.ToString().Trim());
        }

        private static (bool hasMissingItems, string missingErrors) EvaluateMissingKeys(IDictionary missingItems)
        {
            bool hasMissingItems = missingItems?.Count > 0;
            var missingErrors = new StringBuilder();

            if (missingItems?.Count > 0)
            {
                foreach (DictionaryEntry dictionaryEntry in missingItems)
                {
                    var values = GetDictionaryValues(dictionaryEntry.Value);
                    missingErrors.AppendLine($"- contain key \"{dictionaryEntry.Key}\" with value(s) ['{values}']");
                }
            }

            return (hasMissingItems, missingErrors.ToString().Trim());
        }

        private static (bool unMatchedItems, string unMatchedItemsErrors) EvaluateSharedKeys(
            IDictionary dictionary,
            IDictionary sharedItems)
        {
            bool unMatchedItems = false;
            var unMatchedItemsErrors = new StringBuilder();

            if (sharedItems?.Count > 0)
            {
                foreach (DictionaryEntry dictionaryEntry in sharedItems)
                {
                    string expectedValues = GetDictionaryValues(dictionaryEntry.Value);
                    string actualValues = GetDictionaryValues(dictionary[dictionaryEntry.Key]);

                    if (actualValues != expectedValues)
                    {
                        unMatchedItems = true;

                        unMatchedItemsErrors.AppendLine(
                            $"- have key \"{dictionaryEntry.Key}\" " +
                            $"with value(s) ['{expectedValues}'], " +
                            $"but found value(s) ['{actualValues}']");
                    }
                }
            }

            return (unMatchedItems, unMatchedItemsErrors.ToString().Trim());
        }

        private static string GetDictionaryValues(object values)
        {
            List<string> valuesList;

            if (values is string[])
            {
                valuesList = ((string[])values).ToList();
            }
            else if (values is List<string>)
            {
                valuesList = (List<string>)values;
            }
            else
            {
                return values?.ToString() ?? String.Empty;
            }

            if (valuesList.Count == 0)
            {
                return String.Empty;
            }

            string stringValues = valuesList.Aggregate((t1, t2) => t1 + "','" + t2);

            return stringValues;
        }

        private static (
            IDictionary AdditionalItems,
            IDictionary MissingItems,
            IDictionary SharedItems)
            GetDataDifferences(IDictionary dictionary, IDictionary otherDictionary)
        {
            IDictionary additionalItems = dictionary.DeepClone();
            IDictionary missingItems = otherDictionary.DeepClone();
            IDictionary sharedItems = otherDictionary.DeepClone();

            foreach (DictionaryEntry dictionaryEntry in otherDictionary)
            {
                additionalItems.Remove(dictionaryEntry.Key);
            }

            foreach (DictionaryEntry dictionaryEntry in dictionary)
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
    }
}
