// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers
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
                (this.Data[key] as List<string>)?.Add(value);
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
                    this.Data.Add(item.Key, item.Value);
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
            bool isEqual = true;
            StringBuilder messageStringBuilder = new StringBuilder();
            isEqual = CompareDataKeys(dictionary, isEqual, messageStringBuilder);

            return (isEqual, messageStringBuilder.ToString());
        }

        private bool CompareDataKeys(IDictionary dictionary, bool isEqual, StringBuilder messageStringBuilder)
        {
            if (this.Data.Count == 0 && dictionary.Count == 0)
            {
                return isEqual;
            }

            if (this.Data.Count != dictionary.Count)
            {
                isEqual = false;

                AppendMessage(
                    messageStringBuilder,
                    $"- Expected data item count to be {dictionary.Count}, but found {this.Data.Count}.");
            }

            (IDictionary additionalItems, IDictionary missingItems, IDictionary sharedItems) =
                GetDataDifferences(dictionary);

            isEqual = EvaluateAdditionalKeys(isEqual, messageStringBuilder, additionalItems);
            isEqual = EvaluateMissingKeys(isEqual, messageStringBuilder, missingItems);
            isEqual = EvaluateSharedKeys(isEqual, messageStringBuilder, sharedItems);

            return isEqual;
        }

        private bool EvaluateAdditionalKeys(
            bool isEqual,
            StringBuilder messageStringBuilder,
            IDictionary additionalItems)
        {
            if (additionalItems?.Count > 0)
            {
                isEqual = false;

                foreach (DictionaryEntry dictionaryEntry in additionalItems)
                {
                    AppendMessage(
                        messageStringBuilder,
                        $"- Did not expect to find key '{dictionaryEntry.Key}'.");
                }
            }

            return isEqual;
        }

        private bool EvaluateMissingKeys(
            bool isEqual,
            StringBuilder messageStringBuilder,
            IDictionary missingItems)
        {
            if (missingItems?.Count > 0)
            {
                isEqual = false;

                foreach (DictionaryEntry dictionaryEntry in missingItems)
                {
                    AppendMessage(
                        messageStringBuilder,
                        $"- Expected to find key '{dictionaryEntry.Key}'.");
                }
            }

            return isEqual;
        }

        private bool EvaluateSharedKeys(
            bool isEqual,
            StringBuilder messageStringBuilder,
            IDictionary sharedItems)
        {
            if (sharedItems?.Count > 0)
            {
                foreach (DictionaryEntry dictionaryEntry in sharedItems)
                {

                    string expectedValues = ((List<string>)dictionaryEntry.Value)
                            .Select(value => value).Aggregate((t1, t2) => t1 + "','" + t2);

                    string actualValues = ((List<string>)this.Data[dictionaryEntry.Key])
                        .Select(value => value).Aggregate((t1, t2) => t1 + "','" + t2);

                    if (actualValues != expectedValues)
                    {
                        isEqual = false;

                        AppendMessage(
                            messageStringBuilder,
                            $"- Expected to find key '{dictionaryEntry.Key}' " +
                            $"with value(s) ['{expectedValues}'], " +
                            $"but found value(s) ['{actualValues}'].");
                    }
                }
            }

            return isEqual;
        }

        private (
            IDictionary AdditionalItems,
            IDictionary MissingItems,
            IDictionary SharedItems)
            GetDataDifferences(IDictionary dictionary)
        {
            IDictionary additionalItems = this.Data.DeepClone();
            IDictionary missingItems = dictionary.DeepClone();
            IDictionary sharedItems = dictionary.DeepClone();

            foreach (DictionaryEntry dictionaryEntry in dictionary)
            {
                additionalItems.Remove(dictionaryEntry.Key);
            }

            foreach (DictionaryEntry dictionaryEntry in this.Data)
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

        private void AppendMessage(StringBuilder builder, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                builder.AppendLine(message);
            }
        }
    }
}