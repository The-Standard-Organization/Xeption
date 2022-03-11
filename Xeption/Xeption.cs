﻿// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;

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
            if (this.Data.Count != dictionary.Count)
            {
                return false;
            }

            foreach (DictionaryEntry entry in dictionary)
            {
                bool isKeyNotExists = this.Data.Contains(entry.Key) is false;

                if (isKeyNotExists)
                {
                    return false;
                }

                bool isDataNotSame = CompareData(
                    firstObject: this.Data[entry.Key],
                    secondObject: dictionary[entry.Key]);

                if (isDataNotSame)
                {
                    return false;
                }
            }

            foreach (DictionaryEntry entry in this.Data)
            {
                bool isKeyNotExists = dictionary.Contains(entry.Key) is false;
                if (isKeyNotExists)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CompareData(object firstObject, object secondObject)
        {
            var assertionScope = new AssertionScope();
            firstObject.Should().BeEquivalentTo(secondObject);

            return assertionScope.HasFailures();
        }
    }
}