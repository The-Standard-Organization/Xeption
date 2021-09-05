// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Xeption
{
    public class Xeption : Exception
    {
        public Xeption() : base() { }

        public Xeption(string message) : base(message) { }

        public Xeption(string message, Exception innerException)
            : base(message, innerException)
        { }

        public void UpsertDataList(string key, object value)
        {
            if (this.Data.Contains(key))
            {
                (this.Data[key] as List<object>)?.Add(value);
            }
            else
            {
                this.Data.Add(key, new List<object> { value });
            }
        }

        public void ThrowIfContainsErrors()
        {
            throw this;
        }
    }
}
