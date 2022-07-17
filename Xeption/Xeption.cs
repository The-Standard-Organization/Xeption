// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections;

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
    }
}