// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using CollectionDictionary = System.Collections.IDictionary;

namespace Xeptions
{
    public class Xeption : Exception
    {
        public Xeption() : base() { }

        public Xeption(string message) : base(message) { }

        public Xeption(string message, Exception innerException)
            : base(message, innerException)
        { }

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

        public void AddData(CollectionDictionary dictionary)
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
            return true;
        }
    }
}