// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using Tynamix.ObjectFiller;

namespace Xeptions.Tests
{
    public partial class XeptionTests
    {
        private static string GetRandomMessage() =>
            new MnemonicString().GetValue();

        private static Dictionary<string, List<string>> CreateRandomDictionary()
        {
            var filler = new Filler<Dictionary<string, List<string>>>();

            return filler.Create();
        }

        public class StudentValidationException : Xeption
        {
            public StudentValidationException(string message)
                : base(message)
            { }

            public StudentValidationException(string message, Exception innerException)
                : base(message, innerException)
            { }

            public StudentValidationException(string message, Exception innerException, Dictionary<string,string> data)
                : base(message, innerException, data)
            { }
        }

        public class StudentServiceException : Xeption
        {
            public StudentServiceException(string message)
                : base(message: message)
            { }

            public StudentServiceException(string message, Exception innerException)
                : base(message, innerException)
            { }

            public StudentServiceException(string message, Exception innerException, Dictionary<string, string> data)
                : base(message, innerException, data)
            { }
        }
    }
}