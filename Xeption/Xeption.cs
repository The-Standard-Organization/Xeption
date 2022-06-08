// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
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
            this.Data.Add(key, values);

        public bool DataEquals(IDictionary dictionary)
        {
            foreach (DictionaryEntry entry in dictionary)
            {
                bool isKeyNotExists = this.Data.Contains(entry.Key) is false;

                bool isDataNotSame = CompareData(
                    firstObject: this.Data[entry.Key],
                    secondObject: dictionary[entry.Key]);

                if (isKeyNotExists || isDataNotSame)
                {
                    return false;
                }
            }

            return true;
        }

        public (bool IsEqual, string Message) DataEqualsWithDetail(IDictionary dictionary)
        {
            bool isEqual = true;

            StringBuilder messageStringBuilder =
                new StringBuilder();

            isEqual = CompareDataKeys(dictionary, isEqual, messageStringBuilder);

            return (isEqual, messageStringBuilder.ToString());
        }

        private bool CompareDataKeys(IDictionary dictionary, bool isEqual, StringBuilder messageStringBuilder)
        {
            if (this.Data.Count != dictionary.Count)
            {
                isEqual = false;

                AppendMessage(
                    messageStringBuilder,
                    $"- Expected data item count to be {dictionary.Count}, but found {this.Data.Count}.");
            }

            (var additionalItems, var missingItems) = GetDataDifferences(dictionary);
            isEqual = EvaluateAdditionalItems(isEqual, messageStringBuilder, additionalItems);
            isEqual = EvaluateMissingItems(isEqual, messageStringBuilder, missingItems);

            return isEqual;
        }

        private bool EvaluateAdditionalItems(bool isEqual, StringBuilder messageStringBuilder, IDictionary? additionalItems)
        {
            if (additionalItems.Count > 0)
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

        private bool EvaluateMissingItems(bool isEqual, StringBuilder messageStringBuilder, IDictionary? missingItems)
        {
            if (missingItems.Count > 0)
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

        private (
            IDictionary AdditionalItems,
            IDictionary MissingItems)
            GetDataDifferences(IDictionary dictionary)
        {
            var additionalItems = this.Data.DeepClone();
            var missingItems = dictionary.DeepClone();

            foreach (DictionaryEntry dictionaryEntry in dictionary)
            {
                additionalItems.Remove(dictionaryEntry.Key);
            }

            foreach (DictionaryEntry dictionaryEntry in this.Data)
            {
                missingItems.Remove(dictionaryEntry.Key);
            }

            return (additionalItems, missingItems);
        }

        private void AppendMessage(StringBuilder builder, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                builder.AppendLine(message);
            }
        }

        private List<DictionaryEntry> ConvertDictionaryToList(IDictionary data)
        {
            List<DictionaryEntry> dataList = new List<DictionaryEntry>();

            foreach (DictionaryEntry dictionaryEntry in data)
            {
                dataList.Add(dictionaryEntry);
            }

            return dataList;
        }

        private bool CompareData(object firstObject, object secondObject)
        {
            var assertionScope = new AssertionScope();
            firstObject.Should().BeEquivalentTo(secondObject);

            return assertionScope.HasFailures();
        }
    }
}